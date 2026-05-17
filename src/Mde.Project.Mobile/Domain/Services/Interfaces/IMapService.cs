using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IMapService
    {
        public event Action<Guid> OnPinClicked;

        // methoden
        Task<ResultModel<bool>> Initialize(object mapControl);
        Task<ResultModel<bool>> MoveTo(Location location);
        Task<ResultModel<bool>> SetPins(IEnumerable<Memoria> items);
        Task<ResultModel<bool>> ClearPins();
    }
}
