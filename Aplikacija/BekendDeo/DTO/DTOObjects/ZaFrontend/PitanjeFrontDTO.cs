using System;

namespace BekendDeo.DTO
{
    public class PitanjeFrontDTO
    {
        public int ID { get; set; }
        public string TekstPitanja { get; set; }
        public string TekstOdgovora { get; set; }
        public DateTime DatumPitanja { get; set; }
        public DateTime? DatumOdgovora { get; set; }
        public bool Odgovoreno { get; set; }

        public string MusterijaIme { get; set; }
        public string MusterijaPrezime { get; set; }
        public string RadnikIme { get; set; }
        public string RadnikPrezime { get; set; }
    }
}