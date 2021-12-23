using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("KORISNIK")]
    public class Korisnik
    {
        public Korisnik(string korisnickoIme) {Username = korisnickoIme;}
        public Korisnik() { }

        [Key()]
        [JsonIgnore]
        [Column("KorisnikID")]
        public int ID { get; set; }

        [Column("Username")]
        [MaxLength(20)]
        [Required()]
        public string  Username { get; set; }

        [Column("Sifra")]
        [Required()]
        [MaxLength(150)]
        public string Sifra { get; set; }  


        [Column("Tip")]
        [Required()]
        public string Tip { get; set; }   //tip Admin Radnik i Musterija

        [Column("Zabrana")]
        [Required()]
        [DefaultValue(false)]
        public bool Zabrana { get; set; }     

    }
}