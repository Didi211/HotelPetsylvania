using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BekendDeo.Models;

namespace BekendDeo.APILogika
{
    public interface IDataProviderRadnik
    {
    Task<List<Radnik>> GetRadnike();
        Task<List<ZahtevUsluga>> GetZahtevi_Usluge_BoravciAsync();
        Task<List<ZahtevUsluga>> GetZahtevi_Usluge_JednokratneAsync(int radnikID);
        Task<ZahtevUsluga> GetOneZahtevUsluga(int rezBroj, int uslugaID);
        Task<int> GetIDAsync(string username);
        Task<List<Zahtev>> GetRezervacijuAsync(int rezBroj);
        Task<Radnik> GetRadnikAsync(int idRadnika);
        Task<bool> UpdateAzurirajProfilAsync(Radnik r);
        Task<bool> UpdateOdgovoriNaPitanjeAsync(Pitanje p);
        Task<bool> UpdateKreniSaObradom(ZahtevUsluga zu, int radnikID);
        Task<bool> UpdateZavrsiObradu(ZahtevUsluga zu);
        Task<bool> UpdatePromeniSifru(Radnik radnik, string staraSifra, string novaSifra);
        Task<bool> UpdateRadnik(Radnik r);
        Task UpdateBoravci_Obradjeno();
        Task UpdateBoravci_KreniSaObradom();
        
        
    }
}