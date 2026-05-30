using CommunityToolkit.Maui.Extensions;
using CommunityToolkit.Mvvm.Input;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public partial class DetailsViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IMemoriaService _memoriaService;

        // fields
        private Memoria selectedLocation;
        private ObservableCollection<MediaItem> temporaryItems;

        // properties
        public Memoria SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                SetProperty(ref selectedLocation, value);
            }
        }
        public ObservableCollection<MediaItem> TemporaryItems
        {
            get { return temporaryItems; }
            set
            {
                SetProperty(ref temporaryItems, value);
            }
        }
        public bool HasMedia => TemporaryItems.Any() == true;
        public GridLength MediaColumnWidth => HasMedia ? new GridLength(1, GridUnitType.Star) : new GridLength(0);
        public GridLength ContentColumnWidth => HasMedia ? new GridLength(2, GridUnitType.Star) : new GridLength(1, GridUnitType.Star);


        // Commands
        public ICommand UpdateMemoriaCommand => new Command<Guid>(async (memoriaDetailsId) =>
        {
            await ExecuteUpdateMemoriaCommand(memoriaDetailsId);
        });
        public ICommand DeleteMemoriaCommand => new Command<Guid>(async (specificMemoriaId) =>
        {
            await ExecuteDeleteMemoriaCommand(specificMemoriaId);
        });

        // constructor
        public DetailsViewModel(IMemoriaService memoriaService)
        {
            _memoriaService = memoriaService;
            TemporaryItems = new ObservableCollection<MediaItem>();
            TemporaryItems.CollectionChanged += (_, __) =>
            {
                OnPropertyChanged(nameof(HasMedia));
                OnPropertyChanged(nameof(ContentColumnWidth));
                OnPropertyChanged(nameof(MediaColumnWidth));
            };
        }

        // methoden
        [RelayCommand]
        private async Task OpenImageAsync(string path)
        {
            await Shell.Current.CurrentPage.ShowPopupAsync(new ImagePopup(path));
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
                    "Confirmation",
                    "Are you sure?",
                    "Yes",
                    "No");
                if (!confirm) return;
                var result = await _memoriaService.DeleteMemoriaAsync(memoriaId);
                var deleted = await HandleResult(result);
                if (!deleted) return;

                await Shell.Current.GoToAsync(nameof(ListPage));
            }
            finally
            {
                IsBusy = false;
            }
        }
        async void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await HandleNavigation(query);
        }

        // ondersteunende methoden
        private async Task HandleNavigation(IDictionary<string, object> query)
        {
            try
            {
                IsBusy = true;
                if (!query.TryGetValue("id", out var value)) return;

                if(!Guid.TryParse(value?.ToString(), out var id))
                {
                    await Shell.Current.DisplayAlertAsync(
                        "Error",
                        "No valid memoria-id received.",
                        "OK");

                    return;
                }

                var result = await _memoriaService.GetMemoriaByIdAsync(id);
                var memoria = await HandleResult(result);
                if (memoria == null) return;

                SelectedLocation = memoria;
                LoadExistingImages();
            }
            finally
            {
                IsBusy = false;
            }
        }
        private void LoadExistingImages()
        {
            TemporaryItems.Clear();
            if (SelectedLocation?.MediaMaterial == null) return;

            foreach (var media in SelectedLocation.MediaMaterial)
            {
                TemporaryItems.Add(new MediaItem
                {
                    FilePath = media.FilePath,
                });
            }

            OnPropertyChanged(nameof(HasMedia));
            OnPropertyChanged(nameof(ContentColumnWidth));
            OnPropertyChanged(nameof(MediaColumnWidth));
        }
    }
}
