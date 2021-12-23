using System.Collections.Generic;
using System.Threading.Tasks;
using BekendDeo.Models;

namespace BekendDeo.APILogika
{
    public interface IDataProvider
    {
         
        Task<List<Usluga>> GetCeneAsync();
        Task<List<Korisnik>> GetSveKorisnike();
        Task<List<Pitanje>> GetPitanjaAsync();
        Task<List<Obavestenje>> GetObavestenjaAsync(bool braodcast,int idMusterije);
        Task<List<Recenzija>> GetRecenzijeAsync();
        Task<Korisnik> PostLogin(string username, string password);
        Task<bool> PostObavestiAsync(int idMusterije, Obavestenje ob);
        Task<Korisnik> PostRegistrujMusteiriju(Musterija m);
        Task<Korisnik> NadjiKorisnika(int userid); 
        Task<bool> ValidacijaKorisnikaAsync(string username);
        void KreirajKorisnika(Korisnik user);
        Task<bool> ObrisiKorisnikaAsync(string username,int delFlag);
        Task<bool> UpdateKorisnika(Korisnik k);

    
        
    }
}