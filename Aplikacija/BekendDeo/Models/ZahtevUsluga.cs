using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("ZAHTEV_USLUGA")]
    public class ZahtevUsluga
    {

        [JsonIgnore]
        public virtual Zahtev Zahtev { get; set; }


        //[JsonIgnore]
        //rezBroj je ovo
        public int ZahtevID { get; set; }

        [JsonIgnore]
        public virtual Usluga Usluga { get; set; }

        public int UslugaID { get; set; }

        [Required()]
        [Column("TipUsluge")]
        public string TipUsluge { get; set; }

        [Column("Termin")]
        [DataType(DataType.DateTime)]
        [DefaultValue(null)]
        public DateTime? Termin { get; set; }

        [Column("DatumPocetka")]
        [DataType(DataType.DateTime)]
        [DefaultValue(null)]
        public DateTime? DatumPocetka { get; set; }

        [Column("DatumZavrsetka")]
        [DataType(DataType.DateTime)]
        [DefaultValue(null)]
        public DateTime? DatumZavrsetka { get; set; }

        [Column("Obradjen")]
        public string Obradjen { get; set; }
        /*NEOBRADJEN
          U_OBRADI
        OBRADJEN*/
        public int RadnikID { get; set; }
    }
}