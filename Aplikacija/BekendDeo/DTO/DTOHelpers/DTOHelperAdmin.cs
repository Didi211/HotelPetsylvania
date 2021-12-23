using System.Collections.Generic;
using BekendDeo.Models;
using BekendDeo.AuthentificationService;
using Microsoft.Extensions.Options;
using BekendDeo.Helpers;
using System.Linq;
using System;

namespace BekendDeo.DTO
{
    public class DTOHelperAdmin
    {
        
        private  IOptions<AppSettings> _options;

        private  ITokenManager TokenMan { get; set;}
        
        private  HotelContext Context { get; set; }

        public DTOHelperAdmin(IOptions<AppSettings> options,ITokenManager token)
        {
            _options = options;
            TokenMan = token;
        }
        public Radnik MakeRadnikFromDTO(RadnikBackDTO r)
        {
            Radnik radnik = new Radnik();
            radnik.Ime = r.Ime;
            radnik.Prezime = r.Prezime;
            radnik.Username = r.Username;
            radnik.Sifra = r.Sifra;
            radnik.JMBG = r.JMBG;
            
            return radnik;
        }
        public Usluga MakeUslugaFromDTO(UslugaBackDTO usluga)
        {
            Usluga u = new Usluga();
            //prikupljam podate iz DTO objekta
            u.CenaUsluge = usluga.Cena;
            u.Kapacitet = usluga.Kapacitet;
            u.Naziv = usluga.Naziv;
            u.Tip = usluga.Tip;
            //popunjavam podatke sam 
            u.Dostupnost = true;
            u.DatumDodavanja = DateTime.Now;
            u.DatumIzmeneCene = null;
            u.DatumIzmeneKapaciteta = null;
            u.AdministratorAddID = 1;
            u.AdministratorIzmenaCeneID = null;
            u.AdministratorIzmenaKapacitetaID = null;

            return u; 


        }
        public IList<RadnikFrontDTO> MakeRadnikeDTO(IList<Radnik> listaRadnika)
        {
            IList<RadnikFrontDTO> radiniciDTO = new List<RadnikFrontDTO>();
            foreach(Radnik r in listaRadnika)
            {
                RadnikFrontDTO radnikDTO = new RadnikFrontDTO();
                radnikDTO.ID = r.ID;
                radnikDTO.Username = r.Username;
                radnikDTO.JMBG = r.JMBG;
                radnikDTO.Ime = r.Ime;
                radnikDTO.Prezime = r.Prezime;
                radnikDTO.DatumPocetkaRada = r.DatumPocetkaRada;
                radiniciDTO.Add(radnikDTO);
            }
            return radiniciDTO;
        }

        


    }
}