using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BekendDeo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System.Web;
using System.Net;
using System.Net.Http;
using BekendDeo.APILogika;
using BekendDeo.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Options;
using BekendDeo.Helpers;
using BekendDeo.AuthentificationService;
using System.Security.Cryptography;
using System.Text;

namespace BekendDeo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HotelController : ControllerBase
    {

        #region Properties

        //public HotelContext Context { get; set; }
        public IDataProvider Provider { get; set; }
        public IDataProviderAdmin ProviderAdmin { get; set; }
        public IDataProviderRadnik ProviderRadnik { get; set; }
        public IDataProviderMusterija ProviderMusterija { get; set; }
        public DTOHelper DTOobj { get; set; }
        public ITokenManager TokenMan { get; set; }

        #endregion Properties

        #region Konstruktor
        
        public HotelController(IDataProvider provider,IDataProviderAdmin providerAdmin,
                    IDataProviderMusterija providerMusterija, IDataProviderRadnik providerRadnik
                    ,IOptions<AppSettings> options, ITokenManager token)
        {
            Provider = provider;
            ProviderAdmin = providerAdmin;
            ProviderMusterija = providerMusterija;
            ProviderRadnik = providerRadnik;
            DTOobj = new DTOHelper(options,token);
            TokenMan = token;
        }

        #endregion Konstruktor

        #region HttpGetFunctions  
        

        [Route("GetSpecificID/{userID}")]
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> GetSpecificID([FromRoute] int userID)
        {
            try
            {
                var user = await Provider.NadjiKorisnika(userID);
                if(user == null)
                    return StatusCode(400,"Invalidan ID korisnika");
                int osobaID = await GetSpecificID(user);
                
                
                return Ok(osobaID);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [Route("GetUserByID/{userID}")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetUserByID([FromRoute] int userID)
        {
            try
            {
                var user = await Provider.NadjiKorisnika(userID);
                if(user == null)
                    return StatusCode(400,"Invalidan ID korisnika");
                int osobaID = await GetSpecificID(user);
                var usrDTO = DTOobj.MakeUser(user,osobaID);
                
                return Ok(usrDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }


        [Route("CeneUsluga")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetCeneUsluga() 
        {
            try
            {
                var usluge =  await Provider.GetCeneAsync();
                var menu = DTOobj.MakeCenovnik(usluge);
                if(!usluge.Any())
                    return StatusCode(204);
                return Ok(menu);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }

        }

        [Route("Pitanja")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetPitanja()
        {
            //ovo moze da se iskoristi i da se radniku prikazu pitanja
            //a onda on moze da bira na koje zeli da odgovori
            //ili 
            //da radniku vratim samo neodgovorena pitanja 
            try
            {
                var pitanja = await Provider.GetPitanjaAsync();
                if (!pitanja.Any()) 
                {
                   
                    return StatusCode(204);
                }
                var forum = DTOobj.MakeForum(pitanja);
                return Ok(forum); 
               //forum je lista pitanja gde sam izabrao samo neke podatke
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
         
        }

        [Route("Obavestenja")]
        [AllowAnonymous]
        [HttpGet]        
        public async Task<IActionResult> GetObavestenja()
        {
            try
            {
                //prvo proverava broadcast tako da nece da pukne ako je 0 za ID musterije
                var obavestenja = await Provider.GetObavestenjaAsync(true,0);
                if(!obavestenja.Any())
                    return StatusCode(204);
                var obSvima = DTOobj.MakeObavestenja(obavestenja);
                return Ok(obSvima);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [Route("Recenzije")]
        [AllowAnonymous]
        [HttpGet]
        public async Task<IActionResult> GetRecenzije()
        {
            try
            {
                var recenzije = await Provider.GetRecenzijeAsync();
                if(!recenzije.Any())
                    return StatusCode(204);
                var recPrikaz = DTOobj.MakeRecenzije(recenzije);
                return Ok(recPrikaz);
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null)
                    return StatusCode(500,ex.InnerException.Message);
                return StatusCode(500,ex.Message);
            }
        }
    
        #endregion HttpGetFunctions

        #region HttpPostFunctions

        [Route("Login")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login([FromBody] KorisnikLoginBackDTO korisnikLogin) 
        {
            
            try
            {
                
                string username = korisnikLogin.Username;
                string sifra = korisnikLogin.Sifra;

                var korisnik = await Provider.PostLogin(username,sifra); 
                if(korisnik == null)
                {
                    return StatusCode(400,"Pogresan username ili sifra");
                }
                if(korisnik.Zabrana)
                    return StatusCode(403); //403 forbidden to znaci da mu ne dozvolim pristup
                int osobaID = await GetSpecificID(korisnik);

                
                UserDTO user = DTOobj.MakeUser(korisnik,osobaID);
                user.Token = TokenMan.GenerateToken(user.UserID);
                
                return Ok(user);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.InnerException.Message);
            }
           
        }

        [Route("RegistrujSe")]
        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> RegistrujSe([FromBody] MusterijaBackDTO customer)
        {
            try
            {
                Musterija musterija = DTOobj.MakeMusteriju(customer);
                Korisnik k = await Provider.PostRegistrujMusteiriju(musterija);
                if(k == null)
                    return StatusCode(400,"Korisnik sa username-om: " +  customer.Username + " vec postoji u bazi.");
                UserDTO korisnikWithToken = DTOobj.MakeUser(k,musterija.ID);
                korisnikWithToken.Token = TokenMan.GenerateToken(korisnikWithToken.UserID);
                return Ok(korisnikWithToken);
            }
            catch(Exception ex)
            {
                if (ex.InnerException != null)
                    return StatusCode(500,ex.InnerException.Message);
                return StatusCode(500,ex.Message);
            }
        }
     
        #endregion HttpPostFunctions

        #region HttpDeleteFunctions

  
        #endregion HttpDeleteFunctions

        #region  PrivateFunctions 
        private async Task<int> GetSpecificID(Korisnik user) 
        {
            int osobaID;
            if(user.Tip == "A")
                osobaID = await ProviderAdmin.GetIDAsync(user.Username);
            else if(user.Tip == "R")
                osobaID = await ProviderRadnik.GetIDAsync(user.Username);
            else
                osobaID = await ProviderMusterija.GetIDAsync(user.Username);
            return osobaID;
        }
        private async Task EnkriptujSifre()
        {
            var sha1nc = new SHA1CryptoServiceProvider();
            var listaKorisnika = await Provider.GetSveKorisnike();
            foreach(Korisnik user in listaKorisnika)
            {
                var sifra = user.Sifra;
                sifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(sifra)));
                user.Sifra = sifra;
                
                await Provider.UpdateKorisnika(user);
            }


            var admin = await ProviderAdmin.GetAdministrator();
            admin.Sifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(admin.Sifra)));
            await ProviderAdmin.UpdateAdmin(admin);

            var listaRadnika = await ProviderRadnik.GetRadnike();
            foreach(Radnik radnik in listaRadnika)
            {
                
                radnik.Sifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(radnik.Sifra)));
                
                
                await ProviderRadnik.UpdateRadnik(radnik);
            }


            var listaMusterija = await ProviderMusterija.GetMusterije();
            foreach(Musterija musterija in listaMusterija)
            {
                
                musterija.Sifra = Convert.ToBase64String(sha1nc.ComputeHash(Encoding.ASCII.GetBytes(musterija.Sifra)));
                
                
                await ProviderMusterija.UpdateMusteriju(musterija);
            }

        }
        #endregion PrivateFunctions


    }
}
