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
    public class DataProviderMusterija : IDataProviderMusterija
    {
        #region  Attributes
        private readonly ITokenManager _token;

        #endregion Attributes
        
        #region Properties 
        public HotelContext Context { get; set; }
        public IDataProvider Provider { get; set; }

        #endregion Properties

        #region Constructor
        public  DataProviderMusterija(HotelContext context,ITokenManager token)
        {
            Context = context;
            _token = token;
            Provider = new DataProvider(context,token);
        }
        #endregion Constructor

        #region PublicFunctions

        #region Get
        public async Task<List<Musterija>> GetMusterije()
        {
            return await Context.Musterije.ToListAsync();
        }
        public async Task<List<Usluga>> GetUslugeAsync()
        {
            var usluge = await Context.Usluge.Where(x => x.Dostupnost == true).ToListAsync();
            return usluge;
        }
        public async Task<Zahtev> GetOneZahtev(int rezBroj)
        {
            var zahtev = await Context.Zahtevi
                .Where(z => z.RezervacioniBroj == rezBroj)
                .Include(u => u.UslugeZahteva).FirstAsync();
            return zahtev;
        }
        public async Task<int> GetIDAsync(string username)
        {
            var musterija = await Context.Musterije.Where(m => m.Username == username).FirstAsync();
            return musterija.ID;
        }
        public async Task<List<Zahtev>> GetZahteviAsync(int musterijaID)
        {
             return await Context.Zahtevi.Where(m => m.MusterijaID == musterijaID)
                            .Include(z => z.UslugeZahteva)
                            .ThenInclude(u => u.Usluga).ToListAsync();
        }
        public async Task<Musterija> GetMusterijaAsync(int idMusterije)
        {
            return await Context.Musterije.FindAsync(idMusterije); 
        }
        public async Task<string> GetKapacitetiOkej(List<ZahtevUsluga> listaIzabranihUsluga)
        {
            
            int maxKapacitet,trenutnoZauzeto;
            foreach(ZahtevUsluga zusl in listaIzabranihUsluga)
            {
                var usluga =  await Context.Usluge.FindAsync(zusl.UslugaID);
                maxKapacitet = usluga.Kapacitet;
                trenutnoZauzeto = Context.Sastojci
                        .Where(x => x.UslugaID == zusl.UslugaID
                               && x.Obradjen != Obrada.Obradjen).Count();
               
                if(trenutnoZauzeto + 1 > maxKapacitet)
                    return "Nema dovoljno mesta da rezervisete uslugu " + usluga.Naziv;
                
            }
            return "OK";
        }
        #endregion Get

        #region  Post
        public async Task<bool> PostKreirajRecenzijuAsync(Recenzija r)
        {
            if(r.MusterijaID < 1)
                return false;
            r.DatumPostavljanja = DateTime.Now;
            Context.Recenzije.Add(r);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<int> PostKreirajZahtevAsync(Zahtev z)
        {
            

           if (z.MusterijaID < 0 )
                return -1;
            //kreira se prazan zahtev u bazu 
            Zahtev noviZahtev = new Zahtev();
            noviZahtev.MusterijaID = z.MusterijaID;
            noviZahtev.ImeZivotinje = z.ImeZivotinje;
            noviZahtev.Zivotinja = z.Zivotinja;
            Context.Zahtevi.Add(noviZahtev);
            await Context.SaveChangesAsync();

            //sada prikljupamo jedan po jedan id i pravimo novi obj ZahtevUsluga
            foreach (ZahtevUsluga u in z.UslugeZahteva)
            {
                if (u.DatumZavrsetka < u.DatumPocetka)
                    return -1;
                u.ZahtevID = noviZahtev.RezervacioniBroj;
                u.Obradjen = Obrada.Neobradjen;
        
                noviZahtev.UslugeZahteva.Add(u); //dodavanje u listi da bi mogao kasnije da pristupam tim objektima 

                Context.Sastojci.Add(u);
            }
            noviZahtev.Cena = SracunajCenuZahteva(noviZahtev);
            noviZahtev.Obradjen = Obrada.Neobradjen; //NEOBRADJEN, U_OBRADI, OBRADJEN
            noviZahtev.DatumPocetka = null;
            noviZahtev.DatumZavrsetka = null;
            noviZahtev.RadnikID = null;
            await Context.SaveChangesAsync();
            return noviZahtev.RezervacioniBroj;
        }

        public async Task<bool> PostPostaviPitanjeAsync(Pitanje p)
        {
            if(p.MusterijaID < 1)
                return false;
            
            p.TekstOdgovora = null;
            p.DatumOdgovora = null;
            p.DatumPitanja = DateTime.Now;
            p.Odgovoreno = false;
            p.RadnikID = null;
            //p.Musterija = null; //why this??? 
            Context.Pitanja.Add(p);
            await Context.SaveChangesAsync();
            return true;
        }
        
        #endregion Post

        #region Delete
        public async Task<bool> DeleteObrisiZahtevAsync(int zahtevID)
        {
            var zahtev = await Context.Zahtevi.FindAsync(zahtevID);
            if (zahtev == null)
                return false;
            if( zahtev.Obradjen == Obrada.Neobradjen)
            {
                foreach (ZahtevUsluga z in zahtev.UslugeZahteva)
                {
                    Context.Sastojci.Remove(z);
                }
                Context.Zahtevi.Remove(zahtev);
                await Context.SaveChangesAsync();
                return true;
            }
            //ako je zahtev obradjen ili u obradi , ne sme se obrisati, cuva se u arhivi
            return true;
        }

       #endregion Delete

        #region Update
        public async Task<bool> UpdateMusteriju(Musterija m)
        {
            Context.Musterije.Update(m);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateProcitanoObavestenje(int idObavestenja)
        {
            var obavestenje = await Context.Obavestenja.FindAsync(idObavestenja);
            if(obavestenje == null)
                return false;
            obavestenje.Procitano = true;
            Context.Obavestenja.Update(obavestenje);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdateAzurirajProfilAsync(Musterija m,int ID)
        {
            if(ID < 0)
                return false;
            Musterija musterija =await Context.Musterije.FindAsync(ID);
            musterija.Ime = m.Ime;
            musterija.Prezime = m.Prezime;
      
            
            
            Context.Musterije.Update(musterija);
            await Context.SaveChangesAsync();
            
            return true;
        }
        public async Task<bool> UpdateAzurirajZahtevAsync(Zahtev z)
        {
            
            if(z.Obradjen != Obrada.Neobradjen) return false;
            if(z.MusterijaID < 0) return false;
            if(z.RezervacioniBroj < 0)  return false; 
            if(z.UslugeZahteva == null) return false;
            foreach(ZahtevUsluga zusl in z.UslugeZahteva)
            {
                if(zusl.TipUsluge == "BORAVAK")
                {
                    if(zusl.DatumPocetka > zusl.DatumZavrsetka)
                        return false;
                }
                else //tip usluge jednokratna
                {
                    if(zusl.Termin < DateTime.Today)
                        return false;
                }
            }
            z.Cena = SracunajCenuZahteva(z);
            
            Context.Zahtevi.Update(z);
            
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> UpdatePromeniSifru(Musterija musterija, string staraSifra, string novaSifra)
        {
            var sha1nc = new SHA1CryptoServiceProvider();
            staraSifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(staraSifra)));
            
            novaSifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(novaSifra)));


            if(musterija.Sifra != staraSifra)
                return false;
            musterija.Sifra = novaSifra;
            var korisnik = await Context.Korisnici.Where(x => x.Username == musterija.Username).SingleOrDefaultAsync();
            korisnik.Sifra = novaSifra;
            Context.Update(musterija);
            Context.Update(korisnik);

            await Context.SaveChangesAsync();
            return true;
        }
        #endregion Update

        #endregion PublicFunctions       

        #region PrivateFunctions
        private double SracunajCenuZahteva(Zahtev request) //async? podesi je da radi i za boravak
        {
            double cena = 0;
            int zahtevid = request.RezervacioniBroj;
            foreach (ZahtevUsluga u in request.UslugeZahteva)
            {
                int uslugaid = u.UslugaID;
                TimeSpan? daniBoravka;

                IEnumerable<double> cenaUsluge =
                  from usl in Context.Usluge
                  where usl.ID == uslugaid
                  select usl.CenaUsluge;

                if (u.TipUsluge == "BORAVAK") //znaci druga dva polja su puna 
                {
                    daniBoravka = u.DatumZavrsetka - u.DatumPocetka;
                    if (daniBoravka.GetValueOrDefault().Days == 0) //ako nema razlike u danima znaci boravak je jednodnevni
                        cenaUsluge.First();
                    else
                        cena += (daniBoravka.GetValueOrDefault().Days + 1) * cenaUsluge.First();

                }
                else //Onda je tip "JEDNOKRATNA_USLUGA
                {
                    cena += cenaUsluge.First();
                }

            }
            return cena;
        }

        
        #endregion PrivateFunctions

    }
}