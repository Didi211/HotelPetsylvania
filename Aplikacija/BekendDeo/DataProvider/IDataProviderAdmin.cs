using System.Collections.Generic;
using System.Threading.Tasks;
using BekendDeo.DTO;
using BekendDeo.Models;
using Microsoft.AspNetCore.Mvc;

namespace BekendDeo.APILogika
{
    public interface IDataProviderAdmin
    {
        Task<Administrator> GetAdministrator();
        Task<List<Usluga>> GetUslugeAsync();
        Task<List<Radnik>> GetRadnikeAsync();
        Task<List<Obavestenje>> GetObavestenjaAsync();

        Task<int> GetIDAsync(string username);
        Task<bool> PostDodajRadnikaAsync(Radnik r);
        Task<string> PostDodajUsluguAsync(Usluga u);
        Task<string> DeleteObrisiRadnikaAsync(int radnikID);
        Task<string> DeleteObrisiUsluguAsync(int uslugaID);
        Task<string> DeleteObrisiObavestenjeAsync(int obavestenjeID);
        Task<string> UpdateIzmeniCenuAsync(int idUsluge, double cena);
        Task<int> UpdateIzmeniKapacitetAsync(int idUsluge, int kapacite);
        Task<bool> UpdateDostupnostUslugeAsync(int idUsluge,bool value);
        Task<bool> UpdateAdmin(Administrator admin);

        Task SetFlag();
        

        
    }
}