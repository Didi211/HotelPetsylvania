using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("PITANJE")]
    public class Pitanje
    {
        [Key()]
        //[JsonIgnore]
        /* pretpostavka da mi treba id da lakse gadjam 
        pitanje kada treba da odgovori radnik*/
        [Column("PitanjeID")]
        public int ID { get; set; }

        [Column("TekstPitanja")]
        [MaxLength(255)]
        [Required()]
        public  string TekstPitanja { get; set; }

        [Column("TekstOdgovora")]
        [MaxLength(255)]
        [DefaultValue(null)]
        public string  TekstOdgovora { get; set; }

        [Column("DatumPitanja")]
        [DataType(DataType.DateTime)]
        [Required()]
        public DateTime DatumPitanja { get; set; } 

        [Column("DatumOdgovora")]
        [DataType(DataType.DateTime)]        
        [DefaultValue(null)]
        public DateTime?  DatumOdgovora { get; set; } 

        [Column("Odgovoreno")]
        [Required()]
        [DefaultValue(false)]
        //[JsonIgnore] 
        /*da se zna za prikaz koja pitanja su odgovorena koja nisu*/
        public bool Odgovoreno { get; set; } //za ovaj jos ne znam 

        /*ne znam kako da izvucem ime i prezime musterije
        i radnika a da ne skidam json ignore 
        jer onda se svi podaci prikazuju za musteriju i radnika
        pada mi na pamet da napravim fju NameSurnameOnly
        koja ce da nulluje sve ostale podatke.
        Drugi problem je sto bih onda morao da popunjavam podatke za radnika i musteriju.
        Za sada ostaje da se sve salje pa cu da nadjem resenje....*/
        
        [JsonIgnore]
        public  Musterija Musterija { get; set; }

        [Required()]
        public int MusterijaID { get; set; }

        [JsonIgnore]
         public  Radnik Radnik { get; set; }
        
        [DefaultValue(null)]
         public int? RadnikID { get; set; }
    }
}