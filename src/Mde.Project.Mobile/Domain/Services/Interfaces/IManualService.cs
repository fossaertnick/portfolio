using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IManualService
    {
        // methoden
        Task<ResultModel<string>> HelpTheUserAsync();
    }
}
