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
        private ObservableCollection<MemoriaList> locations = new ObservableCollection<MemoriaList>();
        private string searchTerm;
        private CancellationTokenSource? _searchWay;

        // properties
        public ObservableCollection<MemoriaList> Locations
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
        public bool ShowMapButton => DeviceInfo.Current.Platform == DevicePlatform.Android;
        public bool NoMemorias => IsOffline || Locations.Count <= 0;
        public int SearchColumnSpan => ShowMapButton ? 1 : 2;


        // Commands
        public ICommand InitializeMemoriaCommand => new Command(async () =>
        {
            await ExecuteInitializeMemoriaCommand();
        });
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
        }

        // methodes
        [RelayCommand]
        private async Task ExecuteInitializeMemoriaCommand()
        {
            try
            {
                IsBusy = true;
                await RefreshMemoriaList();
            }
            finally
            {
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
        private async Task ExecuteDeleteMemoriaCommand(Guid memoriaId)
        {
            bool bevestiging = await Shell.Current.DisplayAlertAsync(
                "Delete",
                "You want to delete this Memoria?",
                "Yes",
                "No"
                );

            if (!bevestiging) return;
            
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
                    "Yes",
                    "No");
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

                await ExecuteVisuallyFilterLocation(search);
            }
            catch (TaskCanceledException)
            {

            }
        }
        protected override async Task OnInternetRestored()
        {
            await ExecuteInitializeMemoriaCommand();
        }

        // ondersteunende methoden
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
        private async Task ExecuteVisuallyFilterLocation(string name)
        {
            try
            {
                IsBusy = true;
                if (string.IsNullOrWhiteSpace(name))
                {
                    await RefreshMemoriaList();
                    return;
                }

                await Task.Delay(1);
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
    }
}
