using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Pages;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class ListViewModel : ObservableObject
    {
        private readonly IMemoriaService _memorialService;

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
                if(value != null)
                {
                    FilterMemoriaCommand.Execute(value?.Trim() ?? string.Empty);
                }
                SetProperty(ref searchTerm, value);
            }
        }
        public ICommand InitializeMemoriaCommand { get; }
        public ICommand DetailsMemoriaCommand => new Command<Memoria>(async (memoriaDetails) =>
        {
            ExecuteDetailsMemoriaCommand(memoriaDetails);
            await CanExecuteDetailsMemoriaCommand(memoriaDetails);
        });
        public ICommand UpdateMemoriaCommand => new Command<Memoria>(async (memoriaDetails) =>
        {
            if(memoriaDetails != null && memoriaDetails is Memoria)
            {
                Dictionary<string, object> sendInfo = new Dictionary<string, object>
                {
                    {nameof(CreateOrUpdateViewModel.SelectedMemoria), memoriaDetails }
                };
                await Shell.Current.GoToAsync($"{nameof(CreateOrUpdatePage)}?mode=update" , sendInfo);
            }
        });
        public ICommand NavigationCommand => new Command<string>(async (destination) =>
        {
            if( destination == "add")
            {
                await Shell.Current.GoToAsync($"{nameof(CreateOrUpdatePage)}?mode=create");
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
        public ICommand FilterMemoriaCommand => new Command<string>(async (name) =>
        {
            ExecuteVisuallyFilterLocationsCommand(name);
        });
        public ICommand DeleteMemoriaCommand => new Command<Memoria>(async (specificMemoria) =>
        {
            ExecuteDeleteMemoriaCommand(specificMemoria);
        });

        // constructor
        public ListViewModel(IMemoriaService memorialService)
        {
            _memorialService = memorialService;
            InitializeMemoriaCommand = new Command(ExecuteInitializeMemoriaCommand);
        }

        // methodes
        private async void ExecuteInitializeMemoriaCommand()
        {
            var memorias = await _memorialService.GetAllMemoriaAsync();
            Locations.Clear();
            foreach(var memoria in memorias)
            {
                Locations.Add(memoria);
            }
        }
        private async void ExecuteDetailsMemoriaCommand(Memoria memoriaDetails)
        {
            Dictionary<string, object> sendInfo;

            if (memoriaDetails != null)
            {
                sendInfo = new Dictionary<string, object>
                {
                    {"specifics", memoriaDetails }
                };
                await Shell.Current.GoToAsync(nameof(DetailsPage), sendInfo);
            }
        }
        private async Task<bool> CanExecuteDetailsMemoriaCommand(Memoria memoriaDetails)
        {
            return memoriaDetails is Memoria && memoriaDetails is not null;
        }
        private async void ExecuteVisuallyFilterLocationsCommand(string name)
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                Locations = new ObservableCollection<Memoria>(Locations);
            }
            else
            {
                var filtered = await _memorialService.GetMemoriaByFilterAsync(name);
                Locations = new ObservableCollection<Memoria>(filtered);
            }
        }
        private async void ExecuteDeleteMemoriaCommand(Memoria specificMemoria)
        {
            if (specificMemoria is not null)
            {
                await _memorialService.DeleteMemoriaAsync(specificMemoria);
                Locations.Remove(specificMemoria);                
            }
        }
    }
}
