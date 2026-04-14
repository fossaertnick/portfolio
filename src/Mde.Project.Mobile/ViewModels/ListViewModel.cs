using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class ListViewModel : ObservableObject
    {
        private readonly IKnownLocationService _locationService;

        // fields
        private ObservableCollection<KnownLocation> locations = new ObservableCollection<KnownLocation>();

        // properties
        public ObservableCollection<KnownLocation> Locations
        {
            get { return locations; }
            set => SetProperty(ref locations, value);
        }
        public ICommand InitializeLocationCommand { get; }
        public ICommand DetailsSendLocationCommand => new Command<KnownLocation>(async (location) =>
        {
            ExecuteDetailsSendLocationCommand(location);
            CanExecuteDetailsSendLocationCommand(location);
        });
        public ICommand NavigationCommand => new Command<string>(async (destination) =>
        {
            if( destination == "add")
            {
                await Shell.Current.GoToAsync("add");
            }
            else if( destination == "map")
            {
                await Shell.Current.GoToAsync("map");
            }
            else
            {
                await Shell.Current.GoToAsync("//settingsPage");
            }
        });

        // constructor
        public ListViewModel(IKnownLocationService locationService)
        {
            _locationService = locationService;
            InitializeLocationCommand = new Command(ExecuteInitializeLocationCommand);
        }

        // methodes
        private async void ExecuteInitializeLocationCommand()
        {
            var knownLocations = await _locationService.GetAllKnownLocationsAsync();
            Locations = new ObservableCollection<KnownLocation>(knownLocations);
        }
        private async void ExecuteDetailsSendLocationCommand(KnownLocation location)
        {
            var sendInfo = new Dictionary<string, object>
            {
                {"specifics", location }
            };
            await Shell.Current.GoToAsync($"details", sendInfo);
        }
        private bool CanExecuteDetailsSendLocationCommand(KnownLocation location)
        {
            return location is KnownLocation && location is not null;
        }
    }
}
