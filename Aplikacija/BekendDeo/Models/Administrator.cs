using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("ADMINISTRATOR")]
    public class Administrator 
    {
        [Key()]
        [JsonIgnore]
        [Column("AdminID")]
        public int ID { get; set; }

        [Column("Username")]
        [MaxLength(20)]
        [Required()]
        public string Username {get; set;}

        [Column("Sifra")]
        [MaxLength(150)]
        [Required()]
        public string Sifra { get; set; }

        #region ReferenceNaDrugeKlase
        [JsonIgnore]
        public virtual List<Obavestenje> Obavestenja {get;set;} 
        [JsonIgnore]
        public virtual List<Radnik> DodatiRadnici {get;set;}  
        
        [InverseProperty("AdministratorAdd")]
        [JsonIgnore]
        public virtual List<Usluga> DodateUsluge {get;set;} 

        [InverseProperty("AdministratorIzmenaCene")]
        [JsonIgnore]
        public virtual List<Usluga> IzmenjeneCene {get;set;} 

        [InverseProperty("AdministratorIzmenaKapaciteta")] 
        [JsonIgnore]
        public virtual List<Usluga> IzmenjeniKapaciteti {get;set;} 

        #endregion ReferenceNaDrugeKlase
   }
}