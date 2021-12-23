using System.Collections.Generic;
using BekendDeo.Models;
using BekendDeo.AuthentificationService;
using Microsoft.Extensions.Options;
using BekendDeo.Helpers;
using System.Linq;
using System;
using BekendDeo.APILogika;
using System.Threading.Tasks;

namespace BekendDeo.DTO
{
    public class DTOHelperMusterija
    {
        
        private  IOptions<AppSettings> _options;

        private  ITokenManager TokenMan { get; set;}
        
        private  HotelContext Context { get; set; }
        private IDataProviderMusterija Provider { get; set; }
        public DTOHelperMusterija(IOptions<AppSettings> options,ITokenManager token,IDataProviderMusterija provider)
        {
            _options = options;
            TokenMan = token;
            Provider =  provider;

        }
        
       public ProfilMusterijeFrontDTO MakeProfil(Musterija m)
       {
           ProfilMusterijeFrontDTO profil = new ProfilMusterijeFrontDTO();
           profil.ID = m.ID;
           profil.Ime = m.Ime;
           profil.Prezime = m.Prezime;
           profil.Sifra=m.Sifra;
           profil.Username = m.Username;
           profil.Slika = m.Slika;
           return profil;
         
       }
        public Pitanje MakePitanjeFromDTO(PitanjeMusterijaBackDTO pitanje)
        {
            Pitanje pit = new Pitanje();
            //citam iz dto objekta 
            pit.TekstPitanja = pitanje.TekstPitanja;
            pit.MusterijaID = pitanje.MusterijaID;
            return pit;
        }
        public Recenzija MakeRecenzijaFromDTO(RecenzijaBackDTO rec)
        {
            Recenzija recenzija = new Recenzija();
            recenzija.Tekst = rec.Tekst;
            recenzija.Ocena = rec.Ocena;
            recenzija.MusterijaID = rec.MusterijaID;
            return recenzija;
        }

        public async Task<Zahtev>   MakeZahtevBackendFromDTO(ZahtevMusterijaBackDTO zahtev, int rezBroj)
        {
            Zahtev z;
            if(rezBroj > 0)
                z =  await Provider.GetOneZahtev(rezBroj);
            else if(rezBroj < 0)
               {
                   z = new Zahtev();
                  
               } 
            else 
                return null; //nevalidan rez broj 

           

            //sada treba da napravimo uslugeZahtev listu 
            z.UslugeZahteva = MakeZahtevUslugaBackendFromDTO(zahtev.Usluge,rezBroj);
            z.MusterijaID = zahtev.MusterijaID;
            z.ImeZivotinje = zahtev.ImeLjubimca;
            z.Zivotinja = zahtev.TipZivotinje;
         

            return z;
        }
        private List<ZahtevUsluga> MakeZahtevUslugaBackendFromDTO(List<ZahtevUslugaBackDTO> listaDTO, int rezBroj)
        {
            List<ZahtevUsluga> zahtevUslugaLista = new List<ZahtevUsluga>();
            foreach(ZahtevUslugaBackDTO zuDTO in listaDTO)
            {
                ZahtevUsluga objliste = new ZahtevUsluga();
                objliste.UslugaID = zuDTO.UslugaID;
                objliste.TipUsluge = zuDTO.TipUsluge;
                if(zuDTO.Termin != null && zuDTO.Termin != "")
                    objliste.Termin = StringToDateTime(zuDTO.Termin);
                if(zuDTO.DatumPocetka != null && zuDTO.DatumPocetka != "")
                    objliste.DatumPocetka = StringToDate(zuDTO.DatumPocetka,true);
                if(zuDTO.DatumPocetka != null && zuDTO.DatumPocetka != "")
                    objliste.DatumZavrsetka = StringToDate(zuDTO.DatumZavrsetka,false); 
                objliste.ZahtevID = rezBroj;
                objliste.Obradjen = Obrada.Neobradjen;
                zahtevUslugaLista.Add(objliste);
            }
            return zahtevUslugaLista;
        }
        private DateTime StringToDateTime(string datum)
        {
            
            var podaci = datum.Split('/');
            var dan = int.Parse(podaci[0]); 
            var mesec = int.Parse(podaci[1]);
            var godina = int.Parse(podaci[2]);
            var sati = int.Parse(podaci[3]);
            var minuti = int.Parse(podaci[4]);
            DateTime noviDatum = new DateTime(godina,mesec,dan,sati,minuti,1);
            // noviDatum = DateTime.ParseExact(datum, "dd/MM/YYYY/HH/mm", null);
            return noviDatum;
            
        }
        private DateTime StringToDate(string datum, bool pocetak)
        {
            
            var podaci = datum.Split('/');
            var dan = int.Parse(podaci[0]); 
            var mesec = int.Parse(podaci[1]);
            var godina = int.Parse(podaci[2]);
            DateTime noviDatum;
            if(pocetak)
                noviDatum = new DateTime(godina,mesec,dan,9,0,0);
            else
                noviDatum = new DateTime(godina,mesec,dan,18,0,0);
           
            return noviDatum;
        }
      
    }
}