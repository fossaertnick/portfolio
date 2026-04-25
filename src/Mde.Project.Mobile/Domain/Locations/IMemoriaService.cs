using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations
{
    public interface IMemoriaService
    {
        // methodes
        Task<Memoria> GetMemoriaById(Guid id);
        Task<IEnumerable<Memoria>> GetMemoriaByFilterAsync(string searchTerm);
        Task<IEnumerable<Memoria>> GetAllMemoriaAsync();
        Task DeleteMemoriaAsync(Memoria location);
        Task<Memoria> CreateMemoriaAsync(Memoria newMemoria);
        Task<Memoria> UpdateMemoriaAsync(Memoria updateMemoria);
        Task SaveChangesAsync(Memoria saveMemoria);
        
        // map logica
        Task<Location?> GetCurrentCoordinatesAsync();
        Task<(string country, string city, string street, string number)> GetAddressConnectedToCoordinates(Location coordinates);
        Task<bool> EnsureLocationPermission();
    }
}
