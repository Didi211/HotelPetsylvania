using System;

namespace BekendDeo.DTO
{
    public class ZahtevUslugaBackDTO
    {
        /*Klasa koja predstavlja ZahtevUslugu pri slanju od fronta na backend*/
        public int UslugaID { get; set; }
        public string TipUsluge { get; set; }
        public string  Termin { get; set; } //dd/mm/yyyy/hh/mm
        public string DatumPocetka { get; set; }
        public string DatumZavrsetka { get; set; }
        
    }
}