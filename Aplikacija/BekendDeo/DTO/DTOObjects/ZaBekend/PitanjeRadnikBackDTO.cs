
/*OVA KLASA SE KORISTI PRI AZURIRANJU PITAJNA 
TJ KADA PRIHVATA PODATKE SA FRONTA KADA RADNIK ODGOVARA NA PITANJE
*/
namespace BekendDeo.DTO
{
    public class PitanjeRadnikBackDTO
    {
        public int PitanjeID { get; set; }
        public int RadnikID { get; set; }
        public string TekstOdgovora { get; set; }
    }
}