using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        // methoden
        Task<ResultModel<bool>> EnsureLocationPermission();
        Task<ResultModel<Location>> GetCurrentLocationAsync();
    }
}
