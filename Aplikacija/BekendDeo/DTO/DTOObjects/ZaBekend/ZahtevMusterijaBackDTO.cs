using System;
using System.Collections.Generic;

namespace BekendDeo.DTO
{
    public class ZahtevMusterijaBackDTO
    {
        /*Klasa koja se koristi pri kreiranju zahteva na front strani*/
        public int MusterijaID { get; set; }
        public string ImeLjubimca { get; set; }
        public string TipZivotinje { get; set; }
        public List<ZahtevUslugaBackDTO> Usluge { get; set;}
    }
}