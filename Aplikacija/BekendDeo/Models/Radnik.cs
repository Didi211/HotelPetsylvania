using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("RADNIK")]
    public class Radnik
    {

        [Key()]
        [Column("RadnikID")]
        public int ID { get; set; }

        [Column("Ime")]
        [MaxLength(50)]
        [Required()]
        public string Ime { get; set; }

        [Column("Prezime")]
        [MaxLength(50)]
        [Required()]
        public string Prezime { get; set; }

        [Column("Username")]
        [MaxLength(20)]
        [Required()]
        public string Username { get; set; }

        [Column("Sifra")]
        [MaxLength(150)]
        [Required()]
        public string Sifra { get; set; }

        [Column("JMBG")]
        [StringLength(13, MinimumLength = 13)]
        [Required()]
        public string JMBG { get; set; }

        [Column("DatumPocetkaRada")]
        [DataType(DataType.DateTime)]
        [Required()]
        public DateTime DatumPocetkaRada { get; set; } //ne znam da li treba jsonignore

        [Column("DatumPrekidaRada")]
        [DataType(DataType.DateTime)]
        [DefaultValue(null)]
        public DateTime? DatumPrekidaRada { get; set; } ////ne znam da li treba jsonignore

        [Column("SlikaPath")]
        [MaxLength(255)]
        [DefaultValue(null)]
        public string Slika { get; set; }


        [JsonIgnore]
        public virtual Administrator Administrator { get; set; }

        [JsonIgnore]

        public int? AdministratorID { get; set; }

        #region ReferenceNaDrugeKlase
        [JsonIgnore]
        public virtual List<Pitanje> Odgovori { get; set; }

        [JsonIgnore]
        public virtual List<Obavestenje> Obavestenja { get; set; }
        #endregion ReferenceNaDrugeKlase


    }
}