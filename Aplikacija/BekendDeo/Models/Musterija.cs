using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("MUSTERIJA")]
    public class Musterija
    {
       

        [Key()]
        //[JsonIgnore] 
        /*ne moze json ignore jer hocu kada se vrati 
        musterija objekat, da se na frontu zapamti koja je to musterija
        i kad onda ona izvrsavsa neku funkciju da se samo prosledi njen id 
        i ja taj id odmah primenim u funkciji na nacin koji mi treba */
        [Column("MusterijaID")]
        public int ID { get; set; }

        [Column("Ime")]
        [MaxLength(50)]
        [Required()]
        public string  Ime { get; set; }

        [Column("Prezime")]
        [MaxLength(50)]
        [Required()]
        public string  Prezime { get; set; }

        [Column("Username")]
        [MaxLength(20)]
        [Required()]
        public string  Username { get; set; }

        [Column("Sifra")]
        [MaxLength(150)]
        [Required()]
        public string  Sifra { get; set; }
        /*Slika se pamti kao string
        Kada slika stigne na server stranu, prvo se sacuva, i pamti se 
        string putanje,  username + "profilepic.png" i ta putanja 
        navodi i u bazi kao atribut.
        konvertovanje slike u base64 format za slanje od klijenta ka serveru
        */ 
        
        [Column("SlikaPath")]
        [MaxLength(255)]
        [DefaultValue(null)]
        public string Slika { get; set; }

         //ako se ne radi o nekoj tabeli vec o pokazivacu na drugu tabelu 
         //onda se Property oznacava kao virtual   
         #region ReferenceNaDrugeKlase
        [JsonIgnore]
        public virtual List<Pitanje> Pitanja {get;set;} 

        [JsonIgnore]
        public virtual List<Recenzija> Recenzije {get;set;}

        [JsonIgnore]
        public virtual List<Zahtev> Zahtevi {get;set;}
         #endregion ReferenceNaDrugeKlase
    }
}