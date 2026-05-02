using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class ListViewModel : ObservableObject
    {
        private readonly IMemoriaService _memoriaService;

        // fields
        private ObservableCollection<Memoria> locations = new ObservableCollection<Memoria>();
        private string searchTerm;

        // properties
        public ObservableCollection<Memoria> Locations
        {
            get { return locations; }
            set => SetProperty(ref locations, value);
        }
        public string SearchTerm
        {
            get { return searchTerm; }
            set
            {
                if(SetProperty(ref searchTerm, value))
                {
                    ExecuteVisuallyFilterLocationsCommand(searchTerm?.Trim() ?? string.Empty);
                }
            }
        }

        // Commands
        public ICommand InitializeMemoriaCommand { get; }
        public ICommand DetailsMemoriaCommand => new Command<Guid>(async (memoriaDetailsId) =>
        {
            ExecuteDetailsMemoriaCommand(memoriaDetailsId);
        });
        public ICommand UpdateMemoriaCommand => new Command<Guid>(async (memoriaDetailsId) =>
        {
            if(memoriaDetailsId != Guid.Empty)
            {
                await Shell.Current.GoToAsync($"{nameof(CreateOrUpdatePage)}?id={memoriaDetailsId}");
            }
        });
        public ICommand NavigationCommand => new Command<string>(async (destination) =>
        {
            if( destination == "add")
            {
                await Shell.Current.GoToAsync(nameof(CreateOrUpdatePage));
            }
            else if( destination == "map")
            {
                await Shell.Current.GoToAsync(nameof(MapPage));
            }
            else
            {
                await Shell.Current.GoToAsync("//settingsPage");
            }
        });
        public ICommand DeleteMemoriaCommand => new Command<Guid>(async (specificMemoriaId) =>
        {
            ExecuteDeleteMemoriaCommand(specificMemoriaId);
        });

        // constructor
        public ListViewModel(IMemoriaService memorialService)
        {
            _memoriaService = memorialService;
            InitializeMemoriaCommand = new Command(ExecuteInitializeMemoriaCommand);
        }

        // methodes
        private async Task RefreshMemoriaList()
        {
            var memorias = await _memoriaService.GetAllMemoriaAsync();
            Locations.Clear();
            foreach (var memoria in memorias)
            {
                Locations.Add(memoria);
            }
        }
        private async void ExecuteInitializeMemoriaCommand()
        {
            await RefreshMemoriaList();
        }
        private async void ExecuteDetailsMemoriaCommand(Guid memoriaDetailsId)
        {
            if(memoriaDetailsId != Guid.Empty)
            {
                await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={memoriaDetailsId}");
            }
        }
        private async void ExecuteVisuallyFilterLocationsCommand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                var all = await _memoriaService.GetAllMemoriaAsync();
                Locations = new ObservableCollection<Memoria>(all);
            }
            else
            {
                var filtered = await _memoriaService.GetMemoriaByFilterAsync(name);
                Locations = new ObservableCollection<Memoria>(filtered);
            }
        }
        private async void ExecuteDeleteMemoriaCommand(Guid specificMemoriaId)
        {
            if (specificMemoriaId != Guid.Empty)
            {
                await _memoriaService.DeleteMemoriaAsync(specificMemoriaId);
                await RefreshMemoriaList();
            }
        }
    }
}
