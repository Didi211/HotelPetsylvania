using System;

namespace BekendDeo.DTO
{
    public class RadnikFrontDTO
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string JMBG { get; set; } 
        public string Username { get; set; }
        public DateTime DatumPocetkaRada { get; set; }
        
    }
}