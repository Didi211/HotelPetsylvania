using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using BekendDeo.AuthentificationService;
using BekendDeo.DTO;
using BekendDeo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BekendDeo.APILogika
{
    
    public class DataProviderAdmin : IDataProviderAdmin
    {
        #region  Attributes
        private readonly ITokenManager _token;

        #endregion Attributes
        
        #region Properties 
        public HotelContext Context { get; set; }
        public IDataProvider Provider { get; set; }

        #endregion Properties

        #region Constructor
        public  DataProviderAdmin(HotelContext context,ITokenManager token)
        {
            Context = context;
            _token = token;
            Provider = new DataProvider(context,token);
        }
        #endregion Constructor

        #region PublicFunctions


        public async Task SetFlag()
        {
            var obavestenjaLista = await Context.Obavestenja.ToListAsync();
            foreach(Obavestenje o in obavestenjaLista)
            {
                if(o.KomeNamenjeno != null)
                {
                    o.Procitano = false;
                    Context.Obavestenja.Update(o);    

                }
            }
            await Context.SaveChangesAsync();
        }



        #region Get
        public async Task<Administrator> GetAdministrator()
        {
            return await Context.Admin.FirstAsync();
        }
        public async Task<int> GetIDAsync(string username)
        {
            var admin = await Context.Admin.Where(a => a.Username == username).FirstAsync(); 
            return admin.ID;
        }
        public async Task<List<Usluga>> GetUslugeAsync()
        {
            return await Context.Usluge.ToListAsync();
        }

        public async Task<List<Radnik>> GetRadnikeAsync()
        {
           return await Context.Radnici.Where(x => x.DatumPrekidaRada == null).ToListAsync();
        }

       
        public async Task<List<Obavestenje>> GetObavestenjaAsync()
        {
            List<Obavestenje> lista = await Context.Obavestenja
            .Where(o => o.BroadCastFlag == true).ToListAsync();
            return lista;
        }
        #endregion Get

        #region Post
        public async Task<bool> PostDodajRadnikaAsync(Radnik r)
        {
            var sha1nc = new SHA1CryptoServiceProvider();
            r.Sifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(r.Sifra)));
            
            


            if(! await Provider.ValidacijaKorisnikaAsync(r.Username))
            {
                return false;
            }

            r.DatumPocetkaRada = DateTime.Now;
            r.DatumPrekidaRada = null;
            r.AdministratorID = 1;
            Korisnik user = new Korisnik();
            user.Username = r.Username;
            user.Sifra = r.Sifra;
            user.Tip = "R";
            user.Zabrana = false;
            Provider.KreirajKorisnika(user);
            Context.Radnici.Add(r);
            await Context.SaveChangesAsync();
            return true;
        }

        public async Task<string> PostDodajUsluguAsync(Usluga u)
        {
            string validateString = ValidacijaUsluge(u);
              if(validateString != "OK")
                return validateString;
            u.DatumIzmeneCene = null;
            u.DatumIzmeneKapaciteta = null;
            u.DatumDodavanja = DateTime.Now;
            u.Dostupnost = true;
            u.AdministratorAddID = 1;
            u.AdministratorIzmenaCeneID = null;
            u.AdministratorIzmenaKapacitetaID = null;
            Context.Usluge.Add(u);
            await Context.SaveChangesAsync();
            return "OK";
        }

        #endregion Post

        #region Delete
        public async Task<string> DeleteObrisiRadnikaAsync(int radnikID)
        {
           var radnik = await Context.Radnici.FindAsync(radnikID);
            if (radnik == null)
                return "ID radnika nije validan.";
            //provera da li je radnik nesto radio 
            //proverava se da li se pojavljuje u tabelama pitanje,obavestenje i zahtev
            //ako ga nema brisemo ga, ako ga ima, podesvavamo datum i zabranu
            var obavestenja = await Context.Obavestenja
                .Where(o => o.RadnikID == radnikID).ToListAsync();
            var pitanja = await Context.Pitanja
                .Where(p => p.RadnikID == radnikID).ToListAsync();
            var zahtevi = await Context.Zahtevi
                .Where(z => z.RadnikID== radnikID).ToListAsync();
            if(!obavestenja.Any() && !pitanja.Any() && !zahtevi.Any()) 
            {
                await Provider.ObrisiKorisnikaAsync(radnik.Username,1);
                //drugi parametar je delFlag
                //delFlag = 1, brisem iz baze
                //delFlag = 0, ne brisem iz baze samo stavljam zabranu
                Context.Radnici.Remove(radnik);
                await Context.SaveChangesAsync();
                return "DELETED";
            }
            else
            {
                radnik.DatumPrekidaRada = DateTime.Now;
                await Provider.ObrisiKorisnikaAsync(radnik.Username,0);
                await Context.SaveChangesAsync();
                return "FIRED";
            }
        }

        public async Task<string> DeleteObrisiUsluguAsync(int uslugaID)
        {
           //proverava se da li je usluga bila koriscena 
            //to se radi pretragom u klasi ZahtevUsluga 
            var zahtevi = await Context.Sastojci
                .Where(u => u.UslugaID == uslugaID).ToListAsync();
            var usluga = await Context.Usluge.FindAsync(uslugaID);
            if(usluga == null)
                return "ID usluge nije validna vrednost.";
            if(zahtevi.Any()) //usluga je koriscena nekada
            {  
                // usluga.Dostupnost = false;
                // await Context.SaveChangesAsync();
                // return true;
                return "Usluga koriscena. Nije moguce obrisati je. Da li zelite da deaktivirate uslugu?";
            }
            else
            {
                Context.Usluge.Remove(usluga);
                await Context.SaveChangesAsync();
                return "OK";
            }
        }

        public async Task<string> DeleteObrisiObavestenjeAsync(int obavestenjeID)
        {
            var ob = await Context.Obavestenja.FindAsync(obavestenjeID);
            if(ob == null)
                return "ID obavestenja nije validan.";
            Context.Obavestenja.Remove(ob);
            await Context.SaveChangesAsync();
            return "OK";
            
        }
        #endregion Delete

        #region Update
      
        public async Task<bool> UpdateDostupnostUslugeAsync(int idUsluge, bool value)
        {
            var usluga = await Context.Usluge.FindAsync(idUsluge);
            if(usluga == null)
                return false;
            usluga.Dostupnost = value;
            Context.Usluge.Update(usluga);
            await Context.SaveChangesAsync();
            return true;
        }
        public async Task<string> UpdateIzmeniCenuAsync(int idUsluge, double cena)
        {
            /*Nekako mora da naznacim da se promenila cena
            ako samo izmenim cenu onda nece nikakva promena da se desi
            na vec kreirane zahteve. Njihova cena je sracunata i to je to*/
            if (cena <= 0)
                return "Cena <= 0.";
            var usluga = await Context.Usluge.FindAsync(idUsluge);
            if(usluga == null)
                return "ID usluge nije validan.";
            usluga.CenaUsluge = cena;
            usluga.DatumIzmeneCene = DateTime.Now;
            usluga.AdministratorIzmenaCeneID = 1;
            //Context.Usluge.Update(usluga);
            /*Ova funckija ce u kontekstu da zapamti taj objekat
            Jer sam ga promenio lokalno*/
            await Context.SaveChangesAsync();
            //saveChanges svakako povlaci sve lokalne objekte i upisuje ih u bazu
            //ali ako mi negde drugde treba ovaj obj onda Update treba
            return "OK";
            
        } 
        public async Task<int> UpdateIzmeniKapacitetAsync(int idUsluge, int kapacitet)
        {
            /*kapacitet ne sme da bude manji od broj trenutno neobradjenih zahteva + 5 
            cisto radi reda neka bude koje mesto visak slobodno uvek
            treba da se uzme u obzir i broj popunjenih mesta trenutno 
            za to mi treba izmena Obradjen da ima tri vrednosti : Neobradjen, U_obradi, Obradjen*/
            var usluga = await Context.Usluge.FindAsync(idUsluge);
            if(usluga == null)
                return -1;

            int minBroj = await TrenutniKapacitetUslugeAsync(idUsluge);
            //poziva se funkcija koja broji koliko ima zauzetih mesta trenutno
            //rezervisana + u obradi koja su
            if(kapacitet < minBroj )
                return minBroj;
    
            usluga.Kapacitet = kapacitet;
            usluga.DatumIzmeneKapaciteta = DateTime.Now;
            usluga.AdministratorIzmenaKapacitetaID = 1;
            await Context.SaveChangesAsync();
            return 0;
        }  
        public async Task<bool> UpdateAdmin(Administrator admin)
        {
            Context.Admin.Update(admin);
            await Context.SaveChangesAsync();
            return true;
        }
        #endregion Update

        #endregion PublicFunctions


        #region  PrivateFunctions
        private async Task<int> TrenutniKapacitetUslugeAsync(int idUsluge)
        {
            int kapacitet = 0;
            kapacitet = await Context.Sastojci.Where(u => u.UslugaID == idUsluge 
                            && u.Obradjen != Obrada.Obradjen).CountAsync();
            return kapacitet;
        }
        private string ValidacijaUsluge(Usluga service)
        {
            //proveravam samo podatke koje se unose na frontu 
            if (service.CenaUsluge <= 0) return "Cena usluge < 0"; //mora da bude pozitivna vrednost 

            if (service.Kapacitet <= 0) return "Kapacite < 0"; //tkdj

            if (service.Tip != "BORAVAK" && service.Tip != "JEDNOKRATNA_USLUGA") return "Tip usluge nevalidan"; //imitacija enum tipa 
            //naziv proverava db context pa ne moram ovde 
            

            return "OK";
        }
        #endregion PrivateFunctions

    }
}