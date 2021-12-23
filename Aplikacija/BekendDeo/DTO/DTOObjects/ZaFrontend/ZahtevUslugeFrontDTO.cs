using System;

namespace BekendDeo.DTO
{
    public class ZahtevUslugeFrontDTO
    {
        public int ZahtevID { get; set; }
        public int UslugaID { get; set;}
        public string Username { get; set; }
        public string NazivUsluge { get; set; }
        public string ImeLjubimca { get; set; }
        public string TipZivotinje { get; set; }
        public string TipUsluge { get; set;}
        public DateTime? Termin { get; set;}
        public DateTime? DatumPocetka { get; set;}
        public DateTime? DatumZavrsetka { get; set;}
        public string Obradjen { get; set; }
        public int RadnikID { get; set; }

    }
    
}