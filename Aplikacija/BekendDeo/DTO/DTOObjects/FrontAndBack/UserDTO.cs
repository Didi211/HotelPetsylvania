namespace BekendDeo.DTO
{
    public class UserDTO
    {
        //ovo je ID iz tabele Korisnik
        public int UserID { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
        public string Tip { get; set; }
        
        //ovo je ID iz tabela Admin/Musterija/Radnik
        public int OsobaID { get; set; }
    }
}