using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IRawToUserService
    {
        // methoden
        Task<ResultModel<string>> HelpTheUserAsync();
        Task InitializeNotifications();
        Task StopNotifications();
    }
}
