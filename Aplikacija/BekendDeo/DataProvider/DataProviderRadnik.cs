using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BekendDeo.AuthentificationService;
using BekendDeo.Models;
using Microsoft.EntityFrameworkCore;

namespace BekendDeo.APILogika
{
    public class DataProviderRadnik : IDataProviderRadnik
    {
        
        #region  Attributes
        private readonly ITokenManager _token;

        #endregion Attributes
        
        #region Properties 
        public HotelContext Context { get; set; }
        public IDataProvider Provider { get; set; }

        #endregion Properties

        #region Constructor
        public  DataProviderRadnik(HotelContext context,ITokenManager token)
        {
            Context = context;
            _token = token;
            Provider = new DataProvider(context,token);
        }
        #endregion Constructor

        #region PublicFunctions

        #region Get
        public async Task<List<Radnik>> GetRadnike()
        {
            return await Context.Radnici.ToListAsync();
        }
        public async Task<Radnik> GetRadnikAsync(int idRadnika)
        {
            return await Context.Radnici.FindAsync(idRadnika);
        }
        public async Task<List<Zahtev>> GetRezervacijuAsync(int rezBroj)
        {
            if(rezBroj < 1)
                return null;
            return await Context.Zahtevi
                    .Where( z => z.RezervacioniBroj == rezBroj && z.Obradjen == Obrada.Neobradjen)
                    .Include(u => u.UslugeZahteva)
                    .ThenInclude(us => us.Usluga).ToListAsync();
        }
        public async Task<List<ZahtevUsluga>> GetZahtevi_Usluge_BoravciAsync()
        {
            return await Context.Sastojci
            .Where(x =>
                 x.TipUsluge == "BORAVAK"
                 && x.Obradjen != Obrada.Obradjen
                  )
                .Include(x => x.Usluga).Include(x => x.Zahtev).ThenInclude(z => z.Musterija)
                .ToListAsync();
        }

        public async Task<List<ZahtevUsluga>> GetZahtevi_Usluge_JednokratneAsync(int radnikID)
        {
            var lista = await Context.Sastojci.
                Where(x => 
                        x.TipUsluge == "JEDNOKRATNA_USLUGA"
                        && x.Obradjen == Obrada.Neobradjen
                        && x.Termin.Value.Day == DateTime.Now.Day
                     ).Include(x => x.Usluga).Include(x => x.Zahtev)
                .ThenInclude(z => z.Musterija)
                .ToListAsync();
            var listaZaRadnika = await Context.Sastojci.
                Where(x => 
                        x.TipUsluge == "JEDNOKRATNA_USLUGA"
                        && x.Obradjen == Obrada.U_obradi
                        && x.RadnikID == radnikID
                        && x.Termin.Value.Day == DateTime.Now.Day
                     ).Include(x => x.Usluga).Include(x => x.Zahtev)
                .ThenInclude(z => z.Musterija)
                .ToListAsync();
            lista.AddRange(listaZaRadnika);
            return lista;
        }

        public async Task<int> GetIDAsync(string username)
        {
            var radnik = await Context.Radnici.Where(r => r.Username == username).FirstAsync();
            return radnik.ID;
        }
        public async Task<ZahtevUsluga> GetOneZahtevUsluga(int rezBroj, int uslugaID)
        {
            var zahtevUsluga = await Context.Sastojci
                .Where(x => x.ZahtevID == rezBroj && x.UslugaID == uslugaID).SingleOrDefaultAsync();
            return zahtevUsluga;
        }
      
        #endregion Get

        #region Update
        public async Task UpdateBoravci_KreniSaObradom()
        {
            var lista = await Context.Sastojci
            .Where(x => x.TipUsluge == "BORAVAK"
                   && x.Obradjen == Obrada.Neobradjen ).ToListAsync();
            foreach(ZahtevUsluga zusl in lista)
            {
                

                    if(zusl.DatumPocetka.Value.ToString("dd/MM/yyyy") == DateTime.Today.ToString("dd/MM/yyyy"))
                    {
                        var zahtev = await Context.Zahtevi.Where(x => x.RezervacioniBroj == zusl.ZahtevID).SingleOrDefaultAsync();
                        var result = zahtev.UslugeZahteva.All(x => x.Obradjen == Obrada.Neobradjen);

                        zusl.Obradjen = Obrada.U_obradi;
                        if(result)
                        {
                            zahtev.Obradjen = Obrada.U_obradi;
                            zahtev.DatumPocetka = zusl.DatumPocetka;
                        }
                        Context.Sastojci.Update(zusl);
                        Context.Zahtevi.Update(zahtev);
                        
                    }
                
            }
            await Context.SaveChangesAsync();
        }
        public async Task UpdateBoravci_Obradjeno()
        {
            var lista = await Context.Sastojci.Where(x => x.TipUsluge == "BORAVAK").ToListAsync();
            foreach(ZahtevUsluga zusl in lista)
            {
                if(zusl.DatumZavrsetka < DateTime.Today)
                {
                    zusl.Obradjen = Obrada.Obradjen;
                    var zahtev = await Context.Zahtevi.Where(x => x.RezervacioniBroj == zusl.ZahtevID).SingleOrDefaultAsync();
                    var result = zahtev.UslugeZahteva.All(x => x.Obradjen == Obrada.Obradjen);
                    if(result)
                    {
                        zahtev.Obradjen = Obrada.Obradjen;
                    }
                }
            }
            await Context.SaveChangesAsync();
            
        }
        public async Task<bool> UpdateRadnik(Radnik r) 
        {
            Context.Radnici.Update(r);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateZavrsiObradu(ZahtevUsluga zu)
        {
            if(zu.Obradjen != Obrada.U_obradi)
                return false;
            var zahtev = await Context.Zahtevi
                    .Where(x => x.RezervacioniBroj == zu.ZahtevID)
                    .Include(x => x.UslugeZahteva)
                    .SingleOrDefaultAsync();
               
            zu.Obradjen = Obrada.Obradjen;
            var result = zahtev.UslugeZahteva.All(x=> x.Obradjen == Obrada.Obradjen);
            if(result)
            {
                zahtev.DatumZavrsetka = DateTime.Now;
                zahtev.Obradjen = Obrada.Obradjen;
            }
            Context.Sastojci.Update(zu);
            Context.Zahtevi.Update(zahtev);

            await Context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateKreniSaObradom(ZahtevUsluga zu, int radnikID)
        {
            var zahtev = await Context.Zahtevi.FindAsync(zu.ZahtevID);

            zahtev.DatumPocetka = DateTime.Now;
            zahtev.RadnikID = radnikID;
            zu.RadnikID = radnikID;

            zu.Obradjen = Obrada.U_obradi;
            zahtev.Obradjen = Obrada.U_obradi;

            Context.Sastojci.Update(zu);
            Context.Zahtevi.Update(zahtev);
            await Context.SaveChangesAsync();

            return true;
        }
        public async Task<bool> UpdateAzurirajProfilAsync(Radnik r)
        {
            if(r.ID < 0) 
                return false;
            var radnik = await Context.Radnici.FindAsync(r.ID);
            if(radnik == null)
                return false;
           

            radnik.Ime = r.Ime;
            radnik.Prezime = r.Prezime;


            Context.Radnici.Update(radnik);

            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<bool> UpdateOdgovoriNaPitanjeAsync(Pitanje p)
        {
            
            if(p.RadnikID < 0) return false;
            var pitanje = await Context.Pitanja.FindAsync(p.ID);
            
            if(pitanje == null) return false;
            pitanje.RadnikID = p.RadnikID;
            pitanje.TekstOdgovora = p.TekstOdgovora;
            pitanje.DatumOdgovora = DateTime.Now;
            pitanje.Odgovoreno = true;
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdatePromeniSifru(Radnik radnik, string staraSifra, string novaSifra)
        {
            var sha1nc = new SHA1CryptoServiceProvider();
            staraSifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(staraSifra)));
            
            novaSifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(novaSifra)));

            if(radnik.Sifra != staraSifra)
                return false;
            radnik.Sifra = novaSifra;
            var korisnik = await Context.Korisnici.Where(x => x.Username == radnik.Username).SingleOrDefaultAsync();
            korisnik.Sifra = novaSifra;
            Context.Update(radnik);
            Context.Update(korisnik);

            await Context.SaveChangesAsync();
            return true;
        }
        #endregion Update

        #endregion PublicFunctions
    }
}