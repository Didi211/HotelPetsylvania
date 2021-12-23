using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("ZAHTEV")]
    public class Zahtev
    {

        public Zahtev()
        {
            UslugeZahteva = new List<ZahtevUsluga>();
            
        }
      
      

        [Key()]
        [Column("RezervacioniBroj")]
        public int RezervacioniBroj { get; set; }
        
        [Column("ImeLjubimca")]
        public string ImeZivotinje { get; set;}

        [Column("Zivotinja")]
        public string Zivotinja { get; set;}
        

        [Column("Cena")]
        [DefaultValue(null)]
        public double? Cena { get; set; }

        
        [Column("Obradjen")]
        [JsonIgnore]
        public string Obradjen { get; set; }

        [Column("DatumPocetka")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        [DefaultValue(null)]
        public  DateTime? DatumPocetka { get; set; }

        [Column("DatumZavrsetka")]
        [DataType(DataType.DateTime)]
        [JsonIgnore]
        [DefaultValue(null)]
        public DateTime? DatumZavrsetka { get; set; }
        
        [JsonIgnore]
        public Radnik Radnik { get; set; }

        [DefaultValue(null)]
        public int? RadnikID { get; set; }

        [JsonIgnore]
        public virtual Musterija Musterija { get; set; }

        
        public int? MusterijaID { get; set; }

        
        public List<ZahtevUsluga> UslugeZahteva { get; set; }
        //jedan zahtev moze da ima vise usluga
        
    }
}