using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("RECENZIJA")]
    public class Recenzija
    {
        [Key()]
        [JsonIgnore]
        [Column("RecenzijaID")]
        public int ID { get; set; }

        [Column("Tekst")]
        [DefaultValue(null)]
        public string  Tekst { get; set; }  

        [Column("Ocena")]
        [Range(1,5)]
        [Required()]
        public int Ocena { get; set; }  

        [Column("DatumPostavljanja")]
        [DataType(DataType.DateTime)]
        [Required()]
        public DateTime DatumPostavljanja { get; set; } 
 
        [JsonIgnore]
        public  Musterija Musterija {get;set;}

        [Required]
        public int MusterijaID { get; set; }
    }
}