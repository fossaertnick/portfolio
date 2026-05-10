using Mde.Project.Mobile.Core.Entities;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IGeoCodingService
    {
        // map logica
        Task ForwardGeoCodeAsync(Memoria saveMemoria);
        Task<Address?> ReverseGeoCodingAsync(Location coordinates);
    }
}
