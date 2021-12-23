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
    public class RadnikController : ControllerBase
    {
        #region Atributi

        
        public HotelContext Context { get; set; }
        public IDataProvider Provider { get; set; }
        public IDataProviderRadnik ProviderRadnik { get; set; }
        public DTOHelperRadnik DTOObjRadnik { get; set; }
        public DTOHelperAdmin DTOObjAdmin { get; set; }
        public DTOHelper DTOObj { get; set; }

        #endregion Atributi

        #region Konstruktor
        public RadnikController(IDataProvider provider,IDataProviderRadnik providerRadnik,
                        IOptions<AppSettings> options, ITokenManager token)
        {
            Provider = provider;
            ProviderRadnik = providerRadnik;
            DTOObjRadnik = new DTOHelperRadnik(options,token);
            DTOObj = new DTOHelper(options,token);
            DTOObjAdmin = new DTOHelperAdmin(options,token);
        }

        #endregion Konstruktor

        #region HttpGetFunctions
        
       

        [Route("Profil/{idRadnika}")]
        [HttpGet]
        public async Task<IActionResult> Getprofil([FromRoute] int idRadnika)
        {
            try
            {
                var radnik = await ProviderRadnik.GetRadnikAsync(idRadnika);
                if(radnik == null)
                    return StatusCode(400);
                var profil = DTOObjRadnik.MakeProfil(radnik);
                return Ok(profil);
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null)
                    return StatusCode(500,ex.InnerException.Message);
                return StatusCode(500,ex.Message);
            }
        }
        [Route("ZahteviUsluge/{radnikID}")] 
        [HttpGet]
        public async Task<IActionResult> GetZahtevi_Usluge([FromRoute] int radnikID) 
        {
            try
            {
                await ProviderRadnik.UpdateBoravci_KreniSaObradom();
                await ProviderRadnik.UpdateBoravci_Obradjeno();
                if(radnikID < 0)
                    return StatusCode(400, "Invalidan id radnika.");
                var radnik = await ProviderRadnik.GetRadnikAsync(radnikID);
                if(radnik == null)
                    return StatusCode(400,"Radnik sa ID: " + radnikID + " ne postoji u bazi.");
                var  boravakLista = await ProviderRadnik.GetZahtevi_Usluge_BoravciAsync();
                var jednokratneLista = await ProviderRadnik.GetZahtevi_Usluge_JednokratneAsync(radnikID);

                IList<IList<ZahtevUslugeFrontDTO>> konacnaLista = new List<IList<ZahtevUslugeFrontDTO>>();
                var boravakListaDTO = DTOObjRadnik.MakeListuZahtevaUsluga(boravakLista);
                konacnaLista.Add(boravakListaDTO);
                var jednokratneListaDTO = DTOObjRadnik.MakeListuZahtevaUsluga(jednokratneLista);
                konacnaLista.Add(jednokratneListaDTO);

                return Ok(konacnaLista);
            }
            catch(Exception ex)
            {
               return StatusCode(500,ex.Message);
            }
        }

    
        [Route("Pretraga/{rezBroj}")]
        [HttpGet]
        public async Task<IActionResult> GetTrazeniZahtev([FromRoute] int rezBroj)
        { 
            try
            {   
                var rezervacija = await ProviderRadnik.GetRezervacijuAsync(rezBroj);
                if (rezervacija == null) 
                {
                    return StatusCode(400);
                }
                return Ok(DTOObj.MakeZahteveDTO(rezervacija));

            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.Message);
            }
        }

        #endregion HttpGetFunctions

        #region HttpPostFunctions
        
        [Route("ObavestiMusteriju/{rezervacionBroj}")]
        [HttpPost]
        public async Task<IActionResult> ObavestiMusteriju([FromRoute] int rezervacionBroj, [FromBody] ObavestenjeDTO notification)
        {
            try
            {
                //idMusterije = -1 onda je broadcast 
                //idMusterije > 0 onda je nekoj musteriji namenjeno
                Obavestenje ob = DTOObjRadnik.MakeObavestenjeFromDTO(notification);
                if(await Provider.PostObavestiAsync(rezervacionBroj,ob))
                    return StatusCode(204);
                else 
                    return StatusCode(400,"Id radnika ili rezBroj zahteva nisu validni.");
            }
            catch(Exception ex)
            {
                return StatusCode(500,ex.InnerException.Message);
            }
        }
        #endregion HttpPostFunctions

        #region HttpPutFunctions
        
        [Route("IzaberiUsluguZaObradu")]
        [HttpPut]
        public async Task<IActionResult> IzaberiUsluguZaObradu([FromBody] ZahtevUslugaRadnikBackDTO usluga)
        {
            try
            {
                return await UslugaObrada(usluga,true);
            }
            catch (Exception ex)
            {
                
               return StatusCode(500,ex.Message);
            }
        }

        [Route("UslugaZavrsena")]
        [HttpPut]
        public async Task<IActionResult> UslugaZavrsena([FromBody] ZahtevUslugaRadnikBackDTO usluga)
        {
            try
            {
                return await UslugaObrada(usluga,false);
            }
            catch (Exception ex)
            {
                
               return StatusCode(500,ex.Message);
            }
        }
        
    
        [Route("OdgovoriNaPitanje")]
        [HttpPut]
        public async Task <IActionResult> OdgovoriNaPitanje([FromBody] PitanjeRadnikBackDTO p)
        {
            try
            {
                Pitanje pitanje = DTOObjRadnik.MakePitanjeFromDTO(p);

                if(await ProviderRadnik.UpdateOdgovoriNaPitanjeAsync(pitanje))
                    return StatusCode(204);
                return StatusCode(400);
                //razlog za 400 je los ID radnika ili los id pitanja 
               
            }
            catch(Exception ex)
            {
                if(ex.InnerException != null)
                    return StatusCode(500,ex.InnerException.Message);    
                return StatusCode(500,ex.Message);
            }
        }

        [Route("PromeniSifru/{idRadnika}")]
        [HttpPut]
        public async Task<IActionResult> PromeniSifru([FromRoute] int idRadnika, [FromBody] SifraBackDTO sifraObj)
        {
            try
            {
                if(idRadnika < 0)
                    return StatusCode(400, "ID Radnika nije validan");
                var radnik = await ProviderRadnik.GetRadnikAsync(idRadnika);
                if(radnik == null)
                    return StatusCode(400, "Radnik ne postoji.");
                if(!await ProviderRadnik.UpdatePromeniSifru(radnik,sifraObj.StaraSifra,sifraObj.NovaSifra))
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

        [Route("AzurirajProfil/{idRadnika}")]
        [HttpPut]
        public async Task<IActionResult> AzurirajProfil([FromRoute] int idRadnika, RadnikBackDTO r)
        {
            
            try
            {

                Radnik radnik = DTOObjAdmin.MakeRadnikFromDTO(r);
                radnik.ID = idRadnika;
               if(await ProviderRadnik.UpdateAzurirajProfilAsync(radnik))
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
        #endregion HttpDeleteFunctions

        #region PrivateFunctions
        private async Task<IActionResult> UslugaObrada(ZahtevUslugaRadnikBackDTO usluga, bool obradiUslugu)
        {
            if(usluga.RadnikID < 1)
                    return StatusCode(400,"ID radnika nije validan.");
                if(usluga.RezervacioniBroj < 1)
                    return StatusCode(400,"Rezervacioni broj nije validan.");
                if(usluga.UslugaID < 1)
                    return StatusCode(400,"ID usluge nije validan.");
                var zahtevUsluga = await ProviderRadnik.GetOneZahtevUsluga(usluga.RezervacioniBroj,usluga.UslugaID);
                if(zahtevUsluga == null)
                    return StatusCode(400,"ZahtevUsluga sa rezervacionim brojem:" + usluga.RezervacioniBroj
                        + "i ID usluge:" + usluga.UslugaID + " ne postoji u bazi.");
                
                if(obradiUslugu)
                    await ProviderRadnik.UpdateKreniSaObradom(zahtevUsluga,usluga.RadnikID);
                else
                {
                    var result = await ProviderRadnik.UpdateZavrsiObradu(zahtevUsluga);
                    if(!result)
                        return StatusCode(400,"Izabrana usluga nije otpocela svoju obradu. Nemoguce je zavrsiti je.");
                }
                return StatusCode(204); 
        }
        #endregion PrivateFunctions
    }

}