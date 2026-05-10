namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface ILocationService
    {
        // methoden
        Task<bool> EnsureLocationPermission();
        Task<Location?> GetCurrentLocationAsync();
    }
}
