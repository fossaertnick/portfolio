using Mde.Project.Mobile.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Text;

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
