using System.Collections.Generic;
using BekendDeo.Models;
using BekendDeo.AuthentificationService;
using Microsoft.Extensions.Options;
using BekendDeo.Helpers;
using System.Linq;

namespace BekendDeo.DTO
{
    public class DTOHelperRadnik
    {
        
        private  IOptions<AppSettings> _options;

        private  ITokenManager TokenMan { get; set;}
        
        private  HotelContext Context { get; set; }

        public DTOHelperRadnik(IOptions<AppSettings> options,ITokenManager token)
        {
            _options = options;
            TokenMan = token;
        }
        public Obavestenje MakeObavestenjeFromDTO(ObavestenjeDTO ob)
        {
            Obavestenje obavestenje = new Obavestenje();
            obavestenje.Sadrzaj = ob.Sadrzaj;
            obavestenje.ID = ob.ID;
            obavestenje.RadnikID = ob.RadnikID;
            return obavestenje;
        }
        public ProfilRadnikaFrontDTO MakeProfil(Radnik r)
        {
            ProfilRadnikaFrontDTO profil =  new ProfilRadnikaFrontDTO();
            profil.Ime = r.Ime;
            profil.Prezime = r.Prezime;
            profil.Username = r.Username;
            profil.Sifra = r.Sifra;
            profil.JMBG = r.JMBG;
            profil.DatumPocetkaRada = r.DatumPocetkaRada;
            return profil; 
        }
        public IList<ZahtevUslugeFrontDTO> MakeListuZahtevaUsluga(IList<ZahtevUsluga> listaUsluga)
        {
            IList<ZahtevUslugeFrontDTO> lista = new List<ZahtevUslugeFrontDTO>();
            foreach(ZahtevUsluga zu in listaUsluga)
            {
                ZahtevUslugeFrontDTO item = new ZahtevUslugeFrontDTO();
                item.ZahtevID = zu.ZahtevID;
                item.UslugaID = zu.UslugaID;
                item.Username = zu.Zahtev.Musterija.Username;
                item.NazivUsluge = zu.Usluga.Naziv;
                item.ImeLjubimca = zu.Zahtev.ImeZivotinje;
                item.TipZivotinje = zu.Zahtev.Zivotinja;
                item.TipUsluge = zu.TipUsluge;
                item.Termin = zu.Termin;
                item.DatumPocetka = zu.DatumPocetka;
                item.DatumZavrsetka = zu.DatumZavrsetka;
                item.Obradjen = zu.Obradjen;
                item.RadnikID = zu.RadnikID;

                lista.Add(item);
            }
            return lista;
        }
        public Pitanje MakePitanjeFromDTO(PitanjeRadnikBackDTO p)
        {
            Pitanje pitanje = new Pitanje();
            pitanje.ID = p.PitanjeID;
            pitanje.RadnikID = p.RadnikID;
            pitanje.TekstOdgovora = p.TekstOdgovora;
            return pitanje;
        }
    }
    
}
