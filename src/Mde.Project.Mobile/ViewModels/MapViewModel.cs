using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class MapViewModel : ObservableObject
    {
        private readonly IMemoriaService _memoriaService;
        private readonly ILocationService _locationService;

        // properties
        public Location? CurrentLocation { get; private set;  }
        public ObservableCollection<Memoria> Locations { get; set; } = new();

        // Commands
        public ICommand PinClickedCommand => new Command<Guid>(async (memoriaDetailsId) =>
        {
                await ExecuteDetailsMemoriaCommand(memoriaDetailsId);
        });

        // constructor
        public MapViewModel(IMemoriaService memoriaService, ILocationService locationService)
        {
            _memoriaService = memoriaService;
            _locationService = locationService;
        }

        // methoden
        public async Task<Location?> ReadyMapService()
        {
            var hasPermission = await _locationService.EnsureLocationPermission();
            if (!hasPermission) throw new ArgumentException("no permission received");

            var location = await _locationService.GetCurrentLocationAsync();
            if(location == null) throw new ArgumentException("no coordinates found");

            var position = new Location(location.Latitude, location.Longitude);
            return position;
        }
        public async Task LoadExistingMemoriaAsync()
        {
            Locations.Clear();  
            var locaties = await _memoriaService.GetAllMemoriaAsync();

            if(locaties != null)
            {
                foreach(var locatie in locaties)
                {
                    Locations.Add(locatie);
                }
            }
            else
            {
                return;
            }
        }
        private async Task ExecuteDetailsMemoriaCommand(Guid memoriaDetailsId)
        {
            if (memoriaDetailsId != Guid.Empty)
            {
                await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={memoriaDetailsId}");
            }
        }
        public async Task LoadMapAsync()
        {
            CurrentLocation = await ReadyMapService();
            await LoadExistingMemoriaAsync();
        }
    }
}
