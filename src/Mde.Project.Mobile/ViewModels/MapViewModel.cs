using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class MapViewModel : BaseViewModel
    {
        private readonly ILocationService _locationService;
        private readonly IMemoriaService _memoriaService;
        private readonly IGeoCodingService _geoCoding;

        // fields
        private Location? currentLocation;

        // properties
        public Location? CurrentLocation
        {
            get { return currentLocation; }
            set
            {
                SetProperty(ref currentLocation, value);
            }
        }
        public ObservableCollection<MemoriaList> Locations { get; set; } = new();

        // Commands
        public ICommand PinClickedCommand => new Command<Guid>(async (memoriaDetailsId) =>
        {
            await ExecuteDetailsMemoriaCommand(memoriaDetailsId);
        });

        // constructor
        public MapViewModel(ILocationService locationService, IMemoriaService memoriaService, IGeoCodingService geoCoding)
        {
            _locationService = locationService;
            _memoriaService = memoriaService;
            _geoCoding = geoCoding;
        }

        // methoden
        private async Task ReadyMapService()
        {
            var permissionResult = await _locationService.EnsureLocationPermission();
            var hasPermission = await HandleResult(permissionResult);
            if (hasPermission != true) return;

            var locationResult = await _locationService.GetCurrentLocationAsync();
            var location = await HandleResult(locationResult);
            if (location == null) return;

            CurrentLocation = new Location(location.Latitude, location.Longitude);
        }
        private async Task LoadExistingMemoriaAsync()
        {
            Locations.Clear();
            var result = await _memoriaService.GetAllMemoriaAsync();
            var memorias = await HandleResult(result);
            if(memorias == null) return;

            foreach(var memoria in memorias)
            {
                Locations.Add(memoria);
            }
        }
        private async Task ExecuteDetailsMemoriaCommand(Guid memoriaDetailsId)
        {
            if (memoriaDetailsId == Guid.Empty)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    "No valid memoria selected.",
                    "OK");

                return;
            }
            
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={memoriaDetailsId}");
        }
        public async Task LoadMapAsync()
        {
            try
            {
                IsBusy = true;
                await ReadyMapService();
                await LoadExistingMemoriaAsync();
            }
            finally
            {
                IsBusy = false;
            }
        }
        public async Task MapClickedCreateMemoria(Location coordinates)
        {
            try
            {
                var result = await _geoCoding.ReverseGeoCodingAsync(coordinates);
                if (!result.IsSucces) return;

                await Shell.Current.GoToAsync(nameof(CreateOrUpdatePage), new Dictionary<string, object>
                {
                    ["Country"] = result.Data.Country,
                    ["City"] = result.Data.City,
                    ["Street"] = result.Data.Street,
                    ["HouseNumber"] = result.Data.HouseNumber ?? string.Empty,
                    ["Latitude"] = coordinates.Latitude,
                    ["Longitude"] = coordinates.Longitude
                });
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Error during map click handling: {ex.Message}");
            }
        }
    }
}
