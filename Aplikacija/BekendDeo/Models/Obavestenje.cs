using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace BekendDeo.Models
{
    [Table("OBAVESTENJE")]
    public class Obavestenje
    {
        [Key()]
        [JsonIgnore]
        [Column("ObavestenjeID")]
        public int ID { get; set; }

        [Column("Sadrzaj")]
        [Required()]
        [MaxLength(255)]
        public string Sadrzaj { get; set; }

        [Column("BroadCastFlag")]
        [Required()]
        //[JsonIgnore] ovo mora da se vidi da se zna ko salje obavestenje
        public bool BroadCastFlag { get; set; }

        [Column("Procitano")]
        [DefaultValue(false)]
        public bool? Procitano { get; set; }

        [Column("KomeNamenjeno")]
        [DefaultValue(null)]
        public int? KomeNamenjeno { get; set; } //ovde se pamti MUsterijaID
    
        [JsonIgnore]
        public  Radnik Radnik { get; set; }

        [DefaultValue(null)]
        public int? RadnikID { get; set; }

        [JsonIgnore]
        public  Administrator Administrator { get; set; }

        [DefaultValue(null)]
        [JsonIgnore]
        public int? AdministratorID { get; set; }

       
       
        
    }
}