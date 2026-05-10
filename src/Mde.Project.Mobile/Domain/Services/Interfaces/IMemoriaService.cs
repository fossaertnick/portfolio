using Mde.Project.Mobile.Core.Entities;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IMemoriaService
    {
        // methodes
        Task<IEnumerable<Memoria>> GetAllMemoriaAsync();
        Task<Memoria> GetMemoriaByIdAsync(Guid id);
        Task<IEnumerable<Memoria>> GetMemoriaByFilterAsync(string searchTerm);
        Task DeleteMemoriaAsync(Guid memoriaId);
        Task SaveMemoriaAsync(Memoria saveMemoria);
    }
}
