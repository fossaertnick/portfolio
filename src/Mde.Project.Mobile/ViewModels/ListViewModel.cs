using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public partial class ListViewModel : BaseViewModel
    {
        private readonly IMemoriaService _memoriaService;

        // fields
        private ObservableCollection<Memoria> locations;
        private string searchTerm;
        private CancellationTokenSource? _searchWay;

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
                    _ = BounceSearchAsync(searchTerm?.Trim() ?? string.Empty);
                }
            }
        }

        // Commands
        public ICommand InitializeMemoriaCommand { get; }
        public ICommand DetailsMemoriaCommand => new Command<Guid>(async (memoriaId) =>
        {
            await ExecuteDetailsMemoriaCommand(memoriaId);
        });
        public ICommand UpdateMemoriaCommand => new Command<Guid>(async (memoriaId) =>
        {
            await ExecuteUpdateMemoriaCommand(memoriaId);
        });
        public ICommand NavigationCommand => new Command<string>(async (destination) =>
        {
            await ExecuteNavigationCommand(destination);
        });
        public ICommand DeleteMemoriaCommand => new Command<Guid>(async (memoriaId) =>
        {
            await ExecuteDeleteMemoriaCommand(memoriaId);
        });
        

        // constructor
        public ListViewModel(IMemoriaService memoriaService)
        {
            _memoriaService = memoriaService;
            Locations = new ObservableCollection<Memoria>();
            InitializeMemoriaCommand = new Command(async () => await ExecuteInitializeMemoriaCommand());
        }

        // methodes
        private async Task RefreshMemoriaList()
        {
            var result = await _memoriaService.GetAllMemoriaAsync();
            var memorias = await HandleResult(result);
            if (memorias == null) return;

            Locations.Clear();
            foreach (var memoria in memorias)
            {
                Locations.Add(memoria);
            }
        }
        [RelayCommand]
        private async Task ExecuteInitializeMemoriaCommand()
        {
            try
            {
                IsBusy = true;
                IsLoading = true;
                await RefreshMemoriaList();
            }
            finally
            {
                IsLoading = false;
                IsBusy = false;
            }
        }
        private async Task ExecuteNavigationCommand(string destination)
        {
            switch (destination)
            {
                case "add":
                    await Shell.Current.GoToAsync(nameof(CreateOrUpdatePage));
                    break;
                case "map":
                    await Shell.Current.GoToAsync(nameof(MapPage));
                    break;
                default:
                    await Shell.Current.GoToAsync("//settingsPage");
                    break;
            }
        }
        private async Task ExecuteDetailsMemoriaCommand(Guid memoriaId)
        {
            if (memoriaId == Guid.Empty)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    "No valid memoria selected",
                    "OK");

                return;
            }
            
            await Shell.Current.GoToAsync($"{nameof(DetailsPage)}?id={memoriaId}");
        }
        private async Task ExecuteUpdateMemoriaCommand(Guid memoriaId)
        {
            if (memoriaId == Guid.Empty)
            {
                await Shell.Current.DisplayAlertAsync(
                    "Error",
                    "No valid memoria selected",
                    "OK");

                return;
            }

            await Shell.Current.GoToAsync($"{nameof(CreateOrUpdatePage)}?id={memoriaId}");
        }
        private async Task ExecuteVisuallyFilterLocationsCommand(string name)
        {
            try
            {
                IsBusy = true;
                if (string.IsNullOrWhiteSpace(name))
                {
                    await RefreshMemoriaList();
                    return;
                }

                var result = await _memoriaService.GetMemoriaByFilterAsync(name);
                var filtered = await HandleResult(result);
                if (filtered == null) return;

                Locations.Clear();
                foreach(var memoria in filtered)
                {
                    Locations.Add(memoria);
                }
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task ExecuteDeleteMemoriaCommand(Guid memoriaId)
        {
            try
            {
                IsBusy = true;
                if (memoriaId == Guid.Empty)
                {
                    await Shell.Current.DisplayAlertAsync(
                        "Error",
                        "No valid memoria selected",
                        "OK");

                    return;
                }

                bool confirm = await Shell.Current.DisplayAlertAsync(
                    "Delete",
                    "Are you sure?",
                    "YES",
                    "NO");
                if (!confirm) return;
                var result = await _memoriaService.DeleteMemoriaAsync(memoriaId);
                var deleted = await HandleResult(result);
                if (!deleted) return;

                await RefreshMemoriaList();
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task BounceSearchAsync(string search)
        {
            _searchWay?.Cancel();
            _searchWay = new CancellationTokenSource();
            try
            {
                await Task.Delay(300, _searchWay.Token);

                await ExecuteVisuallyFilterLocationsCommand(search);
            }
            catch (TaskCanceledException)
            {

            }
        }
    }
}
