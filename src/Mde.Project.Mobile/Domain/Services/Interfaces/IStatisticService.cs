using Mde.Project.Mobile.Core.Entities.Enums;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IStatisticService
    {
        // methoden
        Task<int> GetTotalMemorias();
        Task<int> GetMemoriasByOccasionAsync(OccationType type);
        Task<int> GetPhotoCountAsync();
        Task<int> GetVideoCountAsync();
        Task<string> GetFavoriteCountryAsync();
    }
}
