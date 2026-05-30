using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        // methoden
        Task<ResultModel<bool>> EnsureLocationPermission();
        Task<ResultModel<Location>> GetCurrentLocationAsync();
        Task<ResultModel<bool>> ForwardGeoCodeAsync(Memoria saveMemoria);
        Task<ResultModel<Address>> ReverseGeoCodingAsync(Location coordinates);
    }
}
