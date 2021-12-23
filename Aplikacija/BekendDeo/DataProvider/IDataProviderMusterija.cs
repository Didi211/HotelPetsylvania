using System.Collections.Generic;
using System.Threading.Tasks;
using BekendDeo.Models;

namespace BekendDeo.APILogika
{
    public interface IDataProviderMusterija
    {   
        Task<string> GetKapacitetiOkej(List<ZahtevUsluga> listaIzabranihUsluga);
        Task<List<Musterija>> GetMusterije();
        Task<List<Zahtev>> GetZahteviAsync(int musterijaID);
        Task<List<Usluga>> GetUslugeAsync();
        Task<int> GetIDAsync(string username);
        Task<Musterija> GetMusterijaAsync(int idMusterije);
        Task<Zahtev> GetOneZahtev(int rezBroj);
        Task<bool> PostPostaviPitanjeAsync(Pitanje p);
        Task<bool> PostKreirajRecenzijuAsync(Recenzija r);
        Task<int> PostKreirajZahtevAsync(Zahtev z);
        Task<bool> DeleteObrisiZahtevAsync(int  zahtevID);
        Task<bool> UpdateAzurirajProfilAsync(Musterija m,int ID);
        Task<bool> UpdateAzurirajZahtevAsync(Zahtev z);
        Task<bool> UpdateProcitanoObavestenje(int idObavestenja);
        Task<bool> UpdateMusteriju(Musterija m);
        Task<bool> UpdatePromeniSifru(Musterija musterija, string staraSifra, string novaSifra);
        

    }
}