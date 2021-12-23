using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("USLUGA")]
    public class Usluga
    {
        [Key()]
        [Column("UslugaID")]
        public int ID { get; set; }

        [Column("Dostupnost")]
        public bool Dostupnost {get;set;}

        [Required()]
        [Column("CenaUsluge")]
        public double CenaUsluge { get; set; }

        [Required()]
        [Column("Kapacitet")]
        public int Kapacitet { get; set; }

        [Required()]
        [Column("Tip")]
        public string Tip { get; set; } //Usluga Jednokratna i Boravak

        [Required()]
        [Column("Naziv")]
        [MaxLength(50)]
        public string Naziv { get; set; }

        [Required()]
        [Column("DatumDodavanja")]
        [DataType(DataType.DateTime)]
        public DateTime DatumDodavanja { get; set; } //ako ja ovako radim nema nacina da se na frontu 
                                                     //prikaze adminu kada je dodao uslugu, mora da ide u bazu

        [Column("DatumIzmeneCene")]
        [DataType(DataType.DateTime)]
        [DefaultValue(null)]
        public DateTime? DatumIzmeneCene { get; set; }

        [Column("DatumIzmeneKapaciteta")]
        [DataType(DataType.DateTime)]
        [DefaultValue(null)]
        public DateTime? DatumIzmeneKapaciteta { get; set; }

        [JsonIgnore]
        public virtual List<ZahtevUsluga> PripadaZahtevima { get; set; }
        //jedna usluga moze da bude povezana sa vise zahteva



        #region AdminStraniKljucevi


        [JsonIgnore]
        public Administrator AdministratorAdd { get; set; }

        [Required()]
        [JsonIgnore]
        public int AdministratorAddID { get; set; }

        [JsonIgnore]
        public Administrator AdministratorIzmenaCene { get; set; }

        [DefaultValue(null)]
        [JsonIgnore]
        public int? AdministratorIzmenaCeneID { get; set; }

        [JsonIgnore]
        public Administrator AdministratorIzmenaKapaciteta { get; set; }

        [DefaultValue(null)]
        [JsonIgnore]
        public int? AdministratorIzmenaKapacitetaID { get; set; }
        #endregion AdminStraniKljucevi
    }
}