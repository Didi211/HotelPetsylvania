namespace BekendDeo.DTO
{
    public class ObavestenjeDTO
    {
        public string Sadrzaj { get; set; }
        public int ID { get; set; }

        public int? RadnikID { get; set; }
        //ne znam da li ce mi trebati Id obavestenja kako bi front mogao da ga targetuje
    }
}