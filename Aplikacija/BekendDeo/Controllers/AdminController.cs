#region  Libraries
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
using Microsoft.AspNetCore.Authorization;
using BekendDeo.DTO;
using Microsoft.Extensions.Options;
using BekendDeo.Helpers;
using BekendDeo.AuthentificationService;
#endregion Libraries
namespace BekendDeo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdminController : ControllerBase
    {
        #region Atributi

        public HotelContext Context { get; set; }
        public IDataProvider Provider { get; set; }
        public IDataProviderAdmin ProviderAdmin { get; set; }
        public DTOHelperAdmin DTOobjAdmin { get; set; }
        public DTOHelper DTOobj { get; set; }

        #endregion Atributi

        #region Konstruktor
        public AdminController(IDataProvider provider,IDataProviderAdmin providerAdmin,
                                IOptions<AppSettings> options, ITokenManager token)
        {
            Provider = provider;
            ProviderAdmin = providerAdmin;
            DTOobjAdmin = new DTOHelperAdmin(options,token);
            DTOobj = new DTOHelper(options,token);
        }

        #endregion Konstruktor

        #region HttpGetFunctions

        [Route("PrikaziUsluge")]
        [HttpGet]
        public async Task<IActionResult> GetUsluge()
        {
            try
            {
                var usluge = await ProviderAdmin.GetUslugeAsync();
                if (!usluge.Any()) return StatusCode(204);
                return Ok(usluge);
            }
            catch (Exception ex)
            {
                return ErrorMessage(ex);
            }
        }

        [Route("PrikaziObavestenja")]
        [HttpGet]
        public async Task<IActionResult> GetObavestenja()
        {
            try
            {
                List<Obavestenje> lista = await ProviderAdmin.GetObavestenjaAsync();
                if(!lista.Any())
                    return StatusCode(204,"Nema obavestenja");
                return Ok(DTOobj.MakeObavestenja(lista));
            }   
            catch(Exception ex)
            {
                return ErrorMessage(ex);
            }
        }
        [Route("PrikaziRadnike")]
        [HttpGet]
        public async Task<IActionResult> GetRadnike()
        {
            try
            {
                var radnici = await ProviderAdmin.GetRadnikeAsync();
                if (!radnici.Any())
                {
                    return StatusCode(204);
                }
                var radniciDTO = DTOobjAdmin.MakeRadnikeDTO(radnici);
                return Ok(radniciDTO);
            }
            catch (Exception ex)
            {
                return ErrorMessage(ex);
            }
        }

     

        #endregion HttpGetFunctions

        #region HttpPostFunctions

        [Route("PostaviObavestenje")]
        [HttpPost]
        public async Task<IActionResult> AdminSetObavestenje([FromBody] ObavestenjeDTO notification)
        {
            try
            {

                if(notification.Sadrzaj == null) 
                    return StatusCode(400,"Polje za tekst obavestenja ne sme da bude prazno.");
                Obavestenje ob = new Obavestenje(); 
                ob.Sadrzaj = notification.Sadrzaj;
                //-1 jer ne saljemo musteriji 
                await Provider.PostObavestiAsync(-1, ob);
               
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                return ErrorMessage(ex);
            }

        }

        [Route("DodajRadnika")]
        [HttpPost]
        public async Task<IActionResult> DodajRadnika([FromBody] RadnikBackDTO worker)
        {
            try
            {
                Radnik radnik = DTOobjAdmin.MakeRadnikFromDTO(worker);
                if (await ProviderAdmin.PostDodajRadnikaAsync(radnik))
                    return StatusCode(204);
                return StatusCode(400,"Korisnik sa tim username-om vec postoji.");
            }
            catch (Exception ex)
            {
                return ErrorMessage(ex);
            }
        }

        [Route("DodajUslugu")]
        [HttpPost]
        public async Task<IActionResult> DodajUslugu([FromBody] UslugaBackDTO service)
        {
            try
            {
                Usluga usluga = DTOobjAdmin.MakeUslugaFromDTO(service);
                string validateString = await ProviderAdmin.PostDodajUsluguAsync(usluga);
                if (validateString == "OK")
                    return StatusCode(204);
                return StatusCode(400, validateString);
            }
            catch (Exception ex)
            {
                return ErrorMessage(ex);
            }
        }
        #endregion HttpPostFunctions

        #region HttpPutFunctions


        

        [Route("IzmeniCenu/{idUsluge}")]
        [HttpPut]
        public async Task<IActionResult> IzmeniCenuUsluge([FromRoute] int idUsluge, [FromBody]double cena)
        {
            try
            {
                string validateString = await ProviderAdmin.UpdateIzmeniCenuAsync(idUsluge,cena);
                if(validateString == "OK")
                    return StatusCode(204);
                return StatusCode(400,validateString);
               
            }
            catch(Exception ex)
            {
                return ErrorMessage(ex);
            }
        }
        
        [Route("AktivirajUslugu")]
        [HttpPut]
        public async Task<IActionResult> AktivirajUslugu([FromBody] int uslugaID)
        {
           try
            {
                if( await ProviderAdmin.UpdateDostupnostUslugeAsync(uslugaID,true))
                    return StatusCode(204);
                return StatusCode(400,"Invalidan ID usluge");
            } 
            catch(Exception ex)
            {
               return ErrorMessage(ex);
            } 
        }

        [Route("DeaktivirajUslugu")]
        [HttpPut]
        
        public async Task<IActionResult> DeaktivirajUslugu([FromBody] int uslugaID)
        {
            try
            {
                if( await ProviderAdmin.UpdateDostupnostUslugeAsync(uslugaID,false))
                    return StatusCode(204);
                return StatusCode(400,"Invalidan ID usluge");
            }
            catch(Exception ex)
            {
               return ErrorMessage(ex);
            }
        }

        [Route("IzmeniKapacitet/{idUsluge}")]
        [HttpPut]
        
        public async Task<IActionResult> IzmeniKapacitetUsluge([FromRoute] int idUsluge, [FromBody] int kapacitet)
        {
             try
            {
                int tretnutnoZauzeto = await ProviderAdmin.UpdateIzmeniKapacitetAsync(idUsluge,kapacitet); 
                // == 0 sve okej 
                if(tretnutnoZauzeto == 0)
                    return StatusCode(204);
                // > 0 error i vraca koliki  je trenutni kapacitet
                if(tretnutnoZauzeto > 0)
                    return StatusCode(400,"Vrednost kapaciteta nije validna. Trenutno zauzetih mesta: " + tretnutnoZauzeto);
                // == -1 error da id ne valja 
                return StatusCode(400,"ID usluge nije validan.");
                /*razlozi za 400 code:  idUsluge je losa vrednost*/
                                     
            }
            catch(Exception ex)
            {
                return ErrorMessage(ex);
            }
        }

       

        #endregion HttpPutFunctions

        #region HttpDeleteFunctions

        [Route("OtpustiRadnika/{radnikID}")]
        [HttpDelete]
        //da probamo preko delete samo da izmenimo podatke
        public async Task<IActionResult> OtpustiRadnika([FromRoute] int radnikID)
        {
            try
            {
                string validateString = await ProviderAdmin.DeleteObrisiRadnikaAsync(radnikID); 
                if( validateString == "DELETED" || validateString == "FIRED")
                    return StatusCode(200, validateString);
                return StatusCode(400,validateString);
            }
            catch (Exception ex)
            {
               return ErrorMessage(ex);
            }
        }
        
        [Route("ObrisiUslugu/{uslugaID}")]
        [HttpDelete]
        public async Task<IActionResult> ObrisiUslugu([FromRoute] int uslugaID)
        {
            try
            {
                string validateString = await ProviderAdmin.DeleteObrisiUsluguAsync(uslugaID);
                if (validateString == "OK")
                    return StatusCode(204);
                return StatusCode(400,validateString);
            }
            catch (Exception ex)
            {
                return ErrorMessage(ex);

            }
        }

        [Route("ObrisiObavestenje/{obavestenjeID}")]
        [HttpDelete]
        public async Task<IActionResult> ObrisiObavestenje([FromRoute] int obavestenjeID)
        {
            try
            {
                string validateString = await ProviderAdmin.DeleteObrisiObavestenjeAsync(obavestenjeID);
                if(validateString == "OK")
                    return StatusCode(204);
                return StatusCode(400,validateString);
            }
            catch(Exception ex)
            {
                return ErrorMessage(ex);
            }
        }
        #endregion HttpDeleteFunctions

        #region PrivatneFunckije
       
        
        private ObjectResult ErrorMessage(Exception ex)
        {
           
            return StatusCode(500,ex.Message);  
        }
        #endregion PrivatneFunckije
        

    }
    

}