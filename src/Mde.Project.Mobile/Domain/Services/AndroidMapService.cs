using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Microsoft.Maui.Controls.Maps;
using Microsoft.Maui.Maps;
using Map = Microsoft.Maui.Controls.Maps.Map;

namespace Mde.Project.Mobile.Domain.Services
{
    public class AndroidMapService : IMapService
    {
        private Map _map;
        public event Action<Guid> OnPinClicked;

        // methoden
        public void ClearPins()
        {
            _map.Pins.Clear();
        }
        public void Initialize(object mapControl)
        {
            _map = (Map)mapControl;
        }
        public void MoveTo(Location location)
        {
            _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Microsoft.Maui.Devices.Sensors.Location(location.Latitude, location.Longitude), Distance.FromKilometers(1)));
        }
        public void SetPins(IEnumerable<Memoria> items)
        {
            _map.Pins.Clear();
            foreach(var memoria in items)
            {
                var pin = new Pin
                {
                    Label = memoria.Name,
                    Location = new Microsoft.Maui.Devices.Sensors.Location(memoria.Latitude, memoria.Longitude),
                    BindingContext = memoria.Id,
                };

                pin.MarkerClicked += (s, e) =>
                {
                    if(pin.BindingContext is Guid id)
                    {
                        OnPinClicked?.Invoke(memoria.Id);                       
                    }
                };

                _map.Pins.Add(pin);
            }
        }
    }
}
