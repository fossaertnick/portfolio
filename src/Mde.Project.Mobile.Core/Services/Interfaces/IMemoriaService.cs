using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Core.Services.Interfaces
{
    public interface IMemoriaService
    {
        // methodes
        Task<ResultModel<IEnumerable<Memoria>>> GetAllMemoriaAsync();
        Task<ResultModel<Memoria>> GetMemoriaByIdAsync(Guid id);
        Task<ResultModel<IEnumerable<Memoria>>> GetMemoriaByFilterAsync(string searchTerm);
        Task<ResultModel<bool>> DeleteMemoriaAsync(Guid memoriaId);
        Task<ResultModel<bool>> SaveMemoriaAsync(Memoria saveMemoria);
    }
}
