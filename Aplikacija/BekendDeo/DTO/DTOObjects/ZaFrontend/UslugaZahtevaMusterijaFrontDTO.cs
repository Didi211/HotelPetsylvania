using System;

namespace BekendDeo.DTO
{
    public class UslugaZahtevaMusterijaFrontDTO
    {
        public string Naziv { get; set; }
        public string Obradjen { get; set; }
        public string TipUsluge { get; set; }
        public DateTime? Termin { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }

    }
}