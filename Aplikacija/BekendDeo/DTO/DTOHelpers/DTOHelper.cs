using System.Collections.Generic;
using BekendDeo.Models;
using BekendDeo.AuthentificationService;
using Microsoft.Extensions.Options;
using BekendDeo.Helpers;
using System.Linq;

namespace BekendDeo.DTO
{
    public class DTOHelper
    {
        
        private  IOptions<AppSettings> _options;

        private  ITokenManager TokenMan { get; set;}
        
        private  HotelContext Context { get; set; }

        public DTOHelper(IOptions<AppSettings> options,ITokenManager token)
        {
            _options = options;
            TokenMan = token;
        }
        public  UserDTO MakeUser(Korisnik k,int personID)
        {
            UserDTO user = new UserDTO();
            user.UserID = k.ID;
            user.Username = k.Username;
            user.Tip = k.Tip; 

            user.OsobaID = personID;
            
            ///user.Token = TokenMan.GenerateToken(k.ID);
            return user;
        }
        
        public  List<UslugaFrontDTO> MakeCenovnik(List<Usluga> usluge)
        {
            List<UslugaFrontDTO>  menu = new List<UslugaFrontDTO>();
            
            foreach(Usluga u in usluge)
            {
                UslugaFrontDTO ponuda = new UslugaFrontDTO();
                ponuda.ID = u.ID;
                ponuda.Naziv = u.Naziv;
                ponuda.Cena = u.CenaUsluge;
                ponuda.Tip = u.Tip;
                menu.Add(ponuda);
            }   
            return menu;         
        }

        public  List<PitanjeFrontDTO> MakeForum(List<Pitanje> pitanja)
        {
            List<PitanjeFrontDTO> forum = new List<PitanjeFrontDTO>();
            
            foreach(Pitanje p in pitanja)
            {
                PitanjeFrontDTO pitanjedto = new PitanjeFrontDTO();

                pitanjedto.ID = p.ID;

                pitanjedto.TekstPitanja = p.TekstPitanja;
                pitanjedto.TekstOdgovora = p.TekstOdgovora;

                pitanjedto.DatumPitanja = p.DatumPitanja;
                pitanjedto.DatumOdgovora = p.DatumOdgovora;
                
                pitanjedto.Odgovoreno = p.Odgovoreno;

                pitanjedto.MusterijaIme = p.Musterija.Ime;
                pitanjedto.MusterijaPrezime = p.Musterija.Prezime;
                
                if (p.Odgovoreno)
                {
                    pitanjedto.RadnikIme = p.Radnik.Ime;
                    pitanjedto.RadnikPrezime = p.Radnik.Prezime;
                }
                
                forum.Add(pitanjedto);
            }
            return forum;
        } 

        public  List<ObavestenjeDTO> MakeObavestenja(List<Obavestenje> obavestenja)
        {
            List<ObavestenjeDTO> obList = new List<ObavestenjeDTO>();

            foreach(Obavestenje o in obavestenja)
            {
                ObavestenjeDTO ob =  new ObavestenjeDTO();
                ob.Sadrzaj = o.Sadrzaj;
                ob.ID = o.ID;
                ob.RadnikID = null;
                    
                

                obList.Add(ob);
            }
            return obList;
        }

        public Musterija MakeMusteriju(MusterijaBackDTO customer)
        {
            Musterija  musterija = new Musterija();
            //preuzimam podatke iz DTO objekta
            
            musterija.Ime = customer.Ime;
            musterija.Prezime = customer.Prezime;
            musterija.Username = customer.Username;
            musterija.Sifra = customer.Sifra;
            
            musterija.Slika = customer.Slika;
            return musterija;
        }
        
        public List<RecenzijaFrontDTO> MakeRecenzije(List<Recenzija> recenzije)
        {
            List<RecenzijaFrontDTO> listaRec = new List<RecenzijaFrontDTO>();
            foreach(Recenzija r in recenzije)
            {
                RecenzijaFrontDTO recFront = new RecenzijaFrontDTO();
                recFront.MusterijaIme = r.Musterija.Ime;
                recFront.MusterijaPrezime = r.Musterija.Prezime;
                recFront.Tekst = r.Tekst;
                recFront.Ocena = r.Ocena;
                listaRec.Add(recFront);
            }
            return listaRec;
        }
        public List<ZahtevMusterijaFrontDTO> MakeZahteveDTO(List<Zahtev> zahtevi)
        {
            List<ZahtevMusterijaFrontDTO> zahteviPrikaz = new List<ZahtevMusterijaFrontDTO>();
            foreach(Zahtev z in zahtevi)
            {
                ZahtevMusterijaFrontDTO request = new ZahtevMusterijaFrontDTO();
                request.RezervacioniBroj = z.RezervacioniBroj;
                request.Cena = z.Cena;
                request.ImeLjubimca = z.ImeZivotinje;
                request.TipLjubimca = z.Zivotinja;
                request.DatumPocetka = z.DatumPocetka;
                request.DatumZavrsetka = z.DatumZavrsetka;
                request.IzabraneUsluge = MakeUslugeZahtevaDTO(z.UslugeZahteva);
                request.Obradjen = z.Obradjen;

                zahteviPrikaz.Add(request);
            }
            return zahteviPrikaz;
            
            
        }
         public List<UslugaZahtevaMusterijaFrontDTO> MakeUslugeZahtevaDTO(List<ZahtevUsluga> zahusl)
        {
            List<UslugaZahtevaMusterijaFrontDTO> lista = new List<UslugaZahtevaMusterijaFrontDTO>();
            foreach(ZahtevUsluga z in zahusl)
            {
                UslugaZahtevaMusterijaFrontDTO usluga = new UslugaZahtevaMusterijaFrontDTO();
                usluga.Naziv = z.Usluga.Naziv;
                usluga.TipUsluge = z.TipUsluge;
                usluga.Obradjen = z.Obradjen;
                usluga.Termin = z.Termin;
                usluga.DatumPocetka = z.DatumPocetka;
                usluga.DatumZavrsetka = z.DatumZavrsetka;
                lista.Add(usluga);
            }
            return lista;
        }
    }
}