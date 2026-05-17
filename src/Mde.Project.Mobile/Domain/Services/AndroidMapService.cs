using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
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
        public async Task<ResultModel<bool>> ClearPins()
        {
            try
            {
                if (_map == null) return ResultModel<bool>.Failure("Map was null", "Map was not initialized.");
                _map.Pins.Clear();

                return ResultModel<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while deleting the pins.");
            }
        }
        public async Task<ResultModel<bool>> Initialize(object mapControl)
        {
            try
            {
                if (mapControl is not Map map) return ResultModel<bool>.Failure("Map control was not of type Map", "The map could not be initialized.");
                _map = map;

                return ResultModel<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while initializing the map.");
            }
        }
        public async Task<ResultModel<bool>> MoveTo(Location location)
        {
            try
            {
                if (_map == null) return ResultModel<bool>.Failure("Map was null.", "Map was not initialized.");
                _map.MoveToRegion(MapSpan.FromCenterAndRadius(new Location(location.Latitude, location.Longitude), Distance.FromKilometers(1)));

                return ResultModel<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while moving the map.");
            }
        }
        public async Task<ResultModel<bool>> SetPins(IEnumerable<Memoria> items)
        {
            try
            {
                if (_map == null) return ResultModel<bool>.Failure("Map was null.", "Map was not initialized.");
                if (items == null) return ResultModel<bool>.Failure("Items collection was null", "No locations received.");
                _map.Pins.Clear();
                foreach(var memoria in items)
                {
                    if (memoria.MemoriaAddress == null) { Console.WriteLine($"Memoria {memoria.Id} had no address."); continue; }

                    var pin = new Pin
                    {
                        Label = memoria.Name,
                        Location = new Location(memoria.MemoriaAddress.Latitude, memoria.MemoriaAddress.Longitude),
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

                return ResultModel<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong with the placing of the pins.");
            }
        }
    }
}
