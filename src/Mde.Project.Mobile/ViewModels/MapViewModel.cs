using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Pages;
using Microsoft.Maui.Controls.Maps;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class MapViewModel : ObservableObject
    {
        private readonly IMemoriaService _memoriaService;

        // properties
        public ObservableCollection<Memoria> Locations { get; set; } = new();
        public ICommand NavigationCommand => new Command<string>(async (destination) =>
        {
            if (destination == "add")
            {
                await Shell.Current.GoToAsync($"{nameof(CreateOrUpdatePage)}?mode=create");
            }
            else if (destination == "list")
            {
                await Shell.Current.GoToAsync(nameof(ListPage));
            }
            else
            {
                await Shell.Current.GoToAsync("//settingsPage");
            }
        });

        // constructor
        public MapViewModel(IMemoriaService memoriaService)
        {
            _memoriaService = memoriaService;
        }

        // methoden
        public async Task<Location?> ReadyMapService()
        {
            var hasPermission = await _memoriaService.EnsureLocationPermission();
            if (!hasPermission) throw new ArgumentException("no permission received");

            var location = await _memoriaService.GetCurrentCoordinatesAsync();
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
    }
}
