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
using Microsoft.Extensions.Options;
using BekendDeo.Helpers;
using BekendDeo.AuthentificationService;


namespace BekendDeo.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class MusterijaController : ControllerBase
    {
        #region Atributi

        
        public HotelContext Context { get; set; }
        public IDataProvider Provider { get; set; }
        public IDataProviderMusterija ProviderMusterija { get; set; }
        public DTOHelperMusterija DtoObjMusterija { get; set; }
        public DTOHelper DtoObj { get; set; }

        #endregion Atributi

        #region Konstruktor
        public MusterijaController(IDataProvider provider,IDataProviderMusterija providerMusterija,
                            IOptions<AppSettings> options, ITokenManager token)
        {
            Provider = provider;
            ProviderMusterija = providerMusterija;
            DtoObjMusterija = new DTOHelperMusterija(options,token,providerMusterija);
            DtoObj = new DTOHelper(options,token);
        }

        #endregion Konstruktor

        #region HttpGetFunctions
        [Route("PrikaziUsluge")]
        [HttpGet]
        public async Task<IActionResult> PrikaziUsluge()
        {
            try
            {
                var usluge = await  ProviderMusterija.GetUslugeAsync();
                if(!usluge.Any())
                    return StatusCode(204,"Lista usluga je prazna.");
                var uslugeDTO = DtoObj.MakeCenovnik(usluge);
                return Ok(uslugeDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        
        [Route("Profil/{idMusterije}")]
        [HttpGet]
        public async Task<IActionResult> GetProfil([FromRoute] int idMusterije)
        {
            try
            {
                var musterija = await ProviderMusterija.GetMusterijaAsync(idMusterije);
                if(musterija == null)
                    return StatusCode(400);
                var profil = DtoObjMusterija.MakeProfil(musterija);
                return Ok(profil);

            }
            catch(Exception ex)
            {
                if(ex.InnerException != null)
                    return StatusCode(500,ex.InnerException.Message);
                return StatusCode(500,ex.Message);
            }
        }
        [Route("Zahtevi/{musterijaID}")]
        [HttpGet]
        public async Task<IActionResult> GetZahtevi([FromRoute] int musterijaID)
        {
            try
            {
                var zahtevi =  await ProviderMusterija.GetZahteviAsync(musterijaID);
                if (!zahtevi.Any())
                {
                    return StatusCode(204);
                }
                var zahteviPrikaz = DtoObj.MakeZahteveDTO(zahtevi);
                return Ok(zahteviPrikaz);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
            
        }

        [Route("Obavestenja/{musterijaID}")]
        [HttpGet]
        public async Task<IActionResult> GetObavestenja([FromRoute] int musterijaID)
        {
            try
            {
                var obavestenja = await Provider.GetObavestenjaAsync(false,musterijaID);
                if (!obavestenja.Any()) 
                {
                    return StatusCode(204);
                }
                var obavestenjaMusteriji = DtoObj.MakeObavestenja(obavestenja);
                return  Ok(obavestenjaMusteriji);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }

        }
        #endregion HttpGetFunctions

        #region HttpPostFunctions
        
        [Route("PostaviPitanje")]
        [HttpPost]
        public async Task<IActionResult> PostaviPitanje([FromBody] PitanjeMusterijaBackDTO question)
        {
            try
            {
               
               var pitanje = DtoObjMusterija.MakePitanjeFromDTO(question);
                if(await ProviderMusterija.PostPostaviPitanjeAsync(pitanje))
                    return StatusCode(204);
                return StatusCode(400);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [Route("KreirajRecenziju")]
        [HttpPost]
        public async Task<IActionResult> KreirajRecenziju([FromBody] RecenzijaBackDTO review)
        {
            try
            {
                Recenzija r = DtoObjMusterija.MakeRecenzijaFromDTO(review);
                if(await ProviderMusterija.PostKreirajRecenzijuAsync(r))
                    return StatusCode(204);
                return StatusCode(400);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        [Route("KreirajZahtev")]
        [HttpPost]
        public async Task<IActionResult> KreirajZahtev([FromBody] ZahtevMusterijaBackDTO  request)
        {
            try
            {
                //parametar prebaciti u Klasu Zahtev
                Zahtev zahtev = await DtoObjMusterija.MakeZahtevBackendFromDTO(request,-1);
                //provera kapaciteta za usluge
                string validateString = await ProviderMusterija.GetKapacitetiOkej(zahtev.UslugeZahteva);
                if(validateString != "OK")
                    return StatusCode(400,validateString);
                int rezBroj = await ProviderMusterija.PostKreirajZahtevAsync(zahtev);
                if(rezBroj >  0)
                    return Ok(rezBroj);
                return StatusCode(400, "Datum pocetka ne moze biti posle datuma zavrsetka");
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }  
        }
        #endregion HttpPostFunctions

        #region HttpPutFunctions
        [Route("PromeniSifru/{idMusterije}")]
        [HttpPut]
        public async Task<IActionResult> PromeniSifru([FromRoute] int idMusterije, [FromBody] SifraBackDTO sifraObj)
        {
             try
            {
                if(idMusterije < 0)
                    return StatusCode(400, "ID musterije nije validan");
                var musterija = await ProviderMusterija.GetMusterijaAsync(idMusterije);
                if(musterija == null)
                    return StatusCode(400, "Radnik ne postoji.");
                if(!await ProviderMusterija.UpdatePromeniSifru(musterija,sifraObj.StaraSifra,sifraObj.NovaSifra))
                {
                    return StatusCode(400,"Nije verifikovana stara sifra.");
                }
                return StatusCode(204);
            }
            catch (Exception ex)
            {
                 if(ex.InnerException != null)
                    return StatusCode(500,ex.InnerException.Message);    
                return StatusCode(500,ex.Message);   
            }
        }
        [Route("ProcitanoObavestenje/{idObavestenja}")]
        [HttpPut]
        public async Task<IActionResult> AzurirajObavestenjeProcitano([FromRoute] int idObavestenja)
        {
            try
            {
                var result = await ProviderMusterija.UpdateProcitanoObavestenje(idObavestenja);
                if(!result)
                    return StatusCode(400,"ID Nije validan.");
                return StatusCode(204);
            }
            catch (Exception ex)
            {
               return StatusCode(500,ex.Message); 
            }
        }

        [Route("AzurirajZahtev/{rezBroj}")]
        [HttpPut]
        public async Task<IActionResult> AzurirajZahtev([FromBody] ZahtevMusterijaBackDTO z, [FromRoute] int rezBroj)
        {
            try
            {
                Zahtev zah = await  DtoObjMusterija.MakeZahtevBackendFromDTO(z,rezBroj);
                string validateString = await ProviderMusterija.GetKapacitetiOkej(zah.UslugeZahteva);
                if(validateString != "OK")
                    return StatusCode(400,validateString);
                if (await ProviderMusterija.UpdateAzurirajZahtevAsync(zah))
                    return StatusCode(204);
                return StatusCode(400,"Datum zavrsetka ne moze biti pre pocetka.");
                //greske, los id
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null)
                    return StatusCode(500, ex.InnerException.Message);
                return StatusCode(500, ex.Message);
            }
        }
        
        [Route("AzurirajProfil/{idMusterije}")]
        [HttpPut]
        public async Task<IActionResult> AzurirajProfil([FromRoute] int idMusterije,[FromBody] MusterijaBackDTO musterija)
        {
            try
            {
                Musterija m = DtoObj.MakeMusteriju(musterija);
               
               if(await ProviderMusterija.UpdateAzurirajProfilAsync(m,idMusterije))
                    return StatusCode(204);
                return StatusCode(400);
                /*razlog za error:
                id < 0, username promenjen*/
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }
        #endregion HttpPutFunctions

        #region HttpDeleteFunctions
        
        [Route("ObrisiZahtev/{zahtevID}")]
        [HttpDelete]
        public async Task<IActionResult> ObrisiZahtev([FromRoute] int zahtevID)
        {
             try
            {
                if(await ProviderMusterija.DeleteObrisiZahtevAsync(zahtevID))
                    return StatusCode(204);
                return StatusCode(400);
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }

        }
        #endregion HttpDeleteFunctions

    }
}