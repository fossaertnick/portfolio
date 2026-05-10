using Mde.Project.Mobile.Core.Entities;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IMapService
    {
        public event Action<Guid> OnPinClicked;

        // methoden
        void Initialize(object mapControl);
        void MoveTo(Location location);
        void SetPins(IEnumerable<Memoria> items);
        void ClearPins();
    }
}
