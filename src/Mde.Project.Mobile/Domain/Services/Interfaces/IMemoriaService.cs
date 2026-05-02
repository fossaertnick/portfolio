using Mde.Project.Mobile.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IMemoriaService
    {
        // methodes
        Task<IEnumerable<Memoria>> GetAllMemoriaAsync();
        Task<Memoria> GetMemoriaByIdAsync(Guid id);
        Task<IEnumerable<Memoria>> GetMemoriaByFilterAsync(string searchTerm);
        Task DeleteMemoriaAsync(Guid memoriaId);
        Task SaveChangesAsync(Memoria saveMemoria);
    }
}
