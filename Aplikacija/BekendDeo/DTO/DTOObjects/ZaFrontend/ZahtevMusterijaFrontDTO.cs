using System;
using System.Collections.Generic;

namespace BekendDeo.DTO
{
    public class ZahtevMusterijaFrontDTO
    {
        public int RezervacioniBroj { get; set;}
        public double? Cena { get; set; }
        public string Obradjen { get; set; }
        public string ImeLjubimca { get; set; }
        public string TipLjubimca { get; set; }
        public DateTime? DatumPocetka { get; set; }
        public DateTime? DatumZavrsetka { get; set; }
        public List<UslugaZahtevaMusterijaFrontDTO> IzabraneUsluge { get; set; }

    }
}