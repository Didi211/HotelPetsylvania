using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Linq;
using System.Threading.Tasks;
using BekendDeo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Web;
using System.Net;
using System.Net.Http;
using BekendDeo.AuthentificationService;
using System.Text;

namespace BekendDeo.APILogika
{
    public class DataProvider : IDataProvider
    {
        #region  Attributes
        private readonly ITokenManager _token;

        #endregion Attributes
     
        #region Konstruktor
        public DataProvider(HotelContext context,ITokenManager token)
        {
            Context = context;
            _token = token;

        }

        #endregion 

        #region Property
        public HotelContext Context {get; set;}

        #endregion


        #region PublicFunctions

        #region Get
        public async Task<List<Korisnik>> GetSveKorisnike()
        {
            var lista = await Context.Korisnici.ToListAsync();
            return lista;
        }
        public async Task<Korisnik> NadjiKorisnika(int userid)
        {
            return await Context.Korisnici.FindAsync(userid);
        }
        
        public  async Task<List<Usluga>> GetCeneAsync()
        {
            var usluge = await Context.Usluge.OrderBy(x => x.Tip).ToListAsync();
            return  usluge.FindAll(IfUslugaDostupna);
           
        }
    
        public async Task<List<Pitanje>> GetPitanjaAsync()
        {
            var pitanja =  await Context.Pitanja.Include(m => m.Musterija)
                    .Include(r => r.Radnik).ToListAsync();
           
            return pitanja;
        }

        public async Task<List<Obavestenje>> GetObavestenjaAsync(bool braodcast,int idMusterije)
        {
            if(braodcast)
            //broadcast true onda prikupljam sva obavestenja
                return await Context.Obavestenja.Where(o => o.BroadCastFlag == true).ToListAsync();
            //false onda uzimam za neku musteriju 
            if(idMusterije < 1)
                throw new Exception("ID musterije invalidan.");
            return await Context.Obavestenja.Where( o => o.KomeNamenjeno == idMusterije).ToListAsync();
        }

        public async Task<List<Recenzija>> GetRecenzijeAsync()
        {
            return await Context.Recenzije.Include(m => m.Musterija).ToListAsync();
        }
        #endregion Get

        #region Post
        public async Task<Korisnik> PostLogin(string username, string password)
        {
            var sha1nc = new SHA1CryptoServiceProvider();
            password = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(password)));
            //vraca token 
            var korisnik = await Context.Korisnici.
                Where(usr => usr.Username == username).FirstOrDefaultAsync();
            if(korisnik == null) 
                return null; //nema ga takav korisnik
             
            if(korisnik.Sifra != password) 
                return null; //pogresna sifra   
            // if(korisnik.Zabrana)
            //     return null; //ima zabranu 
            //neka vrati korisnika a onda na u kontroler proverim da li ima zabrana 
            return korisnik;
           
           
        } 

        public async Task<bool> PostObavestiAsync(int rezervacionBroj, Obavestenje ob)
        {
            //rezervacionBroj = -1 onda je broadcast 
            //rezervacionBroj > 0 onda je nekoj musteriji namenjeno
           
            if(rezervacionBroj > 0)
            {
                if(ob.RadnikID < 0) 
                    return false;
                var zahtev = await Context.Zahtevi.Where(x => x.RezervacioniBroj == rezervacionBroj).SingleOrDefaultAsync();
                var musterijaID = zahtev.MusterijaID;
                ob.KomeNamenjeno = musterijaID;
                ob.BroadCastFlag = false;
                ob.AdministratorID = null;
                ob.Procitano = false;
                
            }
            else if(rezervacionBroj == -1)
            {
                ob.KomeNamenjeno = null;
                ob.BroadCastFlag = true;
                ob.RadnikID = null;
                ob.AdministratorID = 1;
            }
            else 
            {
                return false;
            }
            Context.Obavestenja.Add(ob);
            await Context.SaveChangesAsync();
            return true;
        }
 

        public async Task<Korisnik> PostRegistrujMusteiriju(Musterija m)
        {
            var sha1nc = new SHA1CryptoServiceProvider();
            m.Sifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(m.Sifra)));
            
            if(! await ValidacijaKorisnikaAsync(m.Username))
               return null;
            Korisnik noviKorisnik = new Korisnik(m.Username);
            noviKorisnik.Sifra = m.Sifra;
            noviKorisnik.Tip = "M";
            noviKorisnik.Zabrana = false;
            KreirajKorisnika(noviKorisnik);
            Context.Musterije.Add(m);
            await Context.SaveChangesAsync();
            return noviKorisnik;
        }
        #endregion Post

        #region Delete
       
        
       

        #endregion Delete
        #region Update
        public async Task<bool> UpdateKorisnika(Korisnik k )
        {
            Context.Korisnici.Update(k);
            await Context.SaveChangesAsync();
            return true;
        }
        #endregion Update

        #endregion PublicFunctions
        
        #region HelperFunctions
        public async Task<bool> ValidacijaKorisnikaAsync(string username)
        {
            var noviKorisnik = await Context.Korisnici
                .Where(user => user.Username == username)
                .FirstOrDefaultAsync();

            if (noviKorisnik != null) return false;
            return true;
        }

         public  void KreirajKorisnika(Korisnik user)
        {
            Context.Korisnici.Add(user);
        }

        public async  Task<bool> ObrisiKorisnikaAsync(string username,int delFlag) 
        //delFlag = 1, brisem iz baze
        //delFlag = 0, ne brisem iz baze samo stavljam zabranu
        {
            var korisnik = await Context.Korisnici
                .Where(user => user.Username == username).FirstOrDefaultAsync();
            if(korisnik == null)
            {
                return false;
            }
            else
            {
                int userid = korisnik.ID;
                if(delFlag == 1)
                {
                    Context.Korisnici.Remove( korisnik);
                }
                else //delFlag == 0
                {
                   korisnik.Zabrana = true;
                }
               
                await Context.SaveChangesAsync();
                return true;
            }
        }
        
        #endregion HelperFunctions

        #region  PrivateFunctions

       
        private static bool IfUslugaDostupna(Usluga u)
        {
            if (u.Dostupnost == true) return true;
            return false;
        }

        

        #endregion PrivateFunctions
    }
}