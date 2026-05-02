using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Models.enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class CreateOrUpdateViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IMemoriaService _memoriaService;
        private readonly IGeoCodingService _geoService;
        private readonly IMediaService _mediaService;
        private readonly ILocationService _locationService;

        // fields
        private EditMode editMode;
        private string name;
        private string description;
        private OccationType occation;
        private double longitude;
        private double latitude;
        private string city;
        private string country;
        private string street;
        private string houseNumber;
        private DateTime createdOn = DateTime.Now;
        private Memoria selectedMemoria;
        private ImageSource photo;

        // properties
        public EditMode EditMode
        {
            get { return editMode; }
            set
            {
                if(SetProperty(ref editMode, value))
                {
                    OnPropertyChanged(nameof(IsCreateMode));
                }
            }
        }
        public bool IsCreateMode => EditMode == EditMode.Create;
        public Guid Id { get; set; }
        public string Name
        {
            get { return name; }
            set => SetProperty(ref name, value);
        }
        public string Description
        {
            get { return description; }
            set => SetProperty(ref description, value);
        }
        public OccationType Occation
        {
            get { return occation; }
            set
            {
                if(occation != value)
                {
                    occation = value;
                    OnPropertyChanged();
                }
            }
        }

        public double Longitude
        {
            get { return longitude; }
            set => SetProperty(ref longitude, value);
        }
        public double Latitude
        {
            get { return latitude; }
            set => SetProperty(ref latitude, value);
        }
        public string City
        {
            get { return city; }
            set => SetProperty(ref city, value);
        }
        public string Country
        {
            get { return country; }
            set => SetProperty(ref country, value);
        }
        public string Street
        {
            get { return street; }
            set => SetProperty(ref street, value);
        }
        public string HouseNumber
        {
            get { return houseNumber; }
            set => SetProperty(ref houseNumber, value);
        }
        public DateTime CreatedOn
        {
            get { return createdOn; }
            set => SetProperty(ref createdOn, value);
        }
        public Memoria SelectedMemoria
        {
            get => selectedMemoria;
            set => SetProperty(ref selectedMemoria, value);
        }
        public ImageSource Photo
        {
            get { return photo; }
            set
            {
                SetProperty(ref photo, value);
            }
        }
        public ObservableCollection<MediaItem> TemporaryItems { get; set; } = new();

        // commands
        public ICommand CancelCommand => new Command<string>(async (situation) =>
        {
            if (situation is string && situation == "cancel")
            {
                foreach(var media in TemporaryItems.ToList())
                {
                    if(!string.IsNullOrWhiteSpace(media.FilePath) && File.Exists(media.FilePath))
                    {
                        try
                        {
                            File.Delete(media.FilePath);
                        }
                        catch (Exception ex)
                        {
                            Console.WriteLine($"Fout bij verwijderen bestand: {ex.Message}");
                        }
                    }
                }
                TemporaryItems.Clear();
                await Shell.Current.GoToAsync(nameof(ListPage));
            }
        });
        public ICommand CreateCommand => new Command(async () =>
        {
            await CreateOrUpdateMemoriaAsync();
        });
        public ICommand ClickForLocationCommand => new Command(async () =>
        {
            GiveUserHisCurrentLocation();
        });
        public ICommand TakePhotoCommand => new Command(async () =>
        {
            // await TakePhotoAsync();
        });
        public ICommand PickPhotoCommand => new Command(async () =>
        {
            await PickPhotoAsync();
        });
        public ICommand DeleteTemporaryMediaItemCommand => new Command<MediaItem>( (image) =>
        {
            RemoveImage(image);
        });

        // constructor
        public CreateOrUpdateViewModel(IMemoriaService memorialService, IGeoCodingService geoService, IMediaService mediaService, ILocationService locationService)
        {
            _geoService = geoService;
            _mediaService = mediaService;
            _memoriaService = memorialService;
            _locationService = locationService;
        }

        // methoden
        async void IQueryAttributable.ApplyQueryAttributes(IDictionary<string, object> query)
        {
            await HandleNavigation(query);
        }
        private async Task HandleNavigation(IDictionary<string, object> query)
        {
            try
            {
                if (query.TryGetValue("id", out var value) && Guid.TryParse(value?.ToString(), out var id))
                {
                    await InitUpdate(id);
                }
                else
                {
                    InitCreate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void InitCreate()
        {
            EditMode = EditMode.Create;
            SelectedMemoria = null;
            ResetFields();
        }
        private async Task InitUpdate(Guid id)
        {
            EditMode = EditMode.Update;
            SelectedMemoria = await _memoriaService.GetMemoriaByIdAsync(id);

            ApplyMemoriaToFields(SelectedMemoria);

            LoadAdres();
            await LoadExistingImages();
        }
        private void LoadAdres()
        {
            if(SelectedMemoria.MemoriaAddress == null)
            {

                City = string.Empty;
                Country = string.Empty;
                Street = string.Empty;
                HouseNumber = string.Empty;
            }
            else
            {
                City = SelectedMemoria.MemoriaAddress.City;
                Country = SelectedMemoria.MemoriaAddress.Country;
                Street = SelectedMemoria.MemoriaAddress.Street;
                HouseNumber = SelectedMemoria.MemoriaAddress.HouseNumber;
            }
        }
        private void ApplyMemoriaToFields(Memoria memoria)
        {
            if (memoria == null) return;

            Id = memoria.Id;
            Name = memoria.Name;
            Description = memoria.Description;
            Occation = memoria.Occation;
            CreatedOn = memoria.CreatedOn;
            Longitude = memoria.Longitude;
            Latitude = memoria.Latitude;
        }
        private void ResetFields()
        {
            Id = Guid.Empty;
            Name = string.Empty;
            Description = string.Empty;
            Occation = default;
            City = string.Empty;
            Country = string.Empty;
            Street = string.Empty;
            HouseNumber = string.Empty;
            CreatedOn = DateTime.Now;
            Latitude = 0;
            Longitude = 0;
        }
        private async void GiveUserHisCurrentLocation()
        {
            Location currentCoordinates = await _locationService.GetCurrentLocationAsync();
            if (currentCoordinates == null) return;

            var adresFound = await _geoService.ReverseGeoCodingAsync(currentCoordinates);
            if (adresFound == null) return;

            Country = adresFound.Country;
            City = adresFound.City;
            Street = adresFound.Street;
            HouseNumber = adresFound.HouseNumber;
            Latitude = currentCoordinates.Latitude;
            Longitude = currentCoordinates.Longitude;
        }
        private async Task PickPhotoAsync()
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo == null) return;

            var mediaItem = await _mediaService.SavePhotoASync(photo);
            TemporaryItems.Add(mediaItem);
        }
        private void RemoveImage(MediaItem image)
        {
            if (image == null) return;

            TemporaryItems.Remove(image);
        }
        private async Task CreateOrUpdateMemoriaAsync()
        {
            var memoria = SelectedMemoria ?? new Memoria();

            memoria.Name = Name;
            memoria.MemoriaAddress = new Domain.Models.Address
            {
                Country = Country,
                City = City,
                Street = Street,
                HouseNumber = HouseNumber,
            };
            memoria.Latitude = Latitude;
            memoria.Longitude = Longitude;
            memoria.Description = Description;
            memoria.Occation = Occation;
            memoria.MediaMaterial = TemporaryItems.ToList();
            TemporaryItems.Clear();

            if(memoria.CreatedOn == default)
            {
                memoria.CreatedOn = CreatedOn;
            }

            memoria.LastEditedOn = DateTime.UtcNow;

            await _memoriaService.SaveChangesAsync(memoria);

            await Shell.Current.GoToAsync(nameof(ListPage));
        }
        private async Task LoadExistingImages()
        {
            TemporaryItems.Clear();
            if (SelectedMemoria?.MediaMaterial != null)
            {
                foreach (var media in SelectedMemoria.MediaMaterial)
                {
                    TemporaryItems.Add(media);
                }
            }
        }





        /*private async Task TakePhotoAsync()
        {
            if(MediaPicker.Default.IsCaptureSupported)
            {
                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo == null) return;

                var stream = await photo.OpenReadAsync();
                Photo = ImageSource.FromStream(() => stream);
            }
        }*/
        /*private async Task TakeVideoAsync()
        {
            if(MediaPicker.Default.IsCaptureSupported)
            {
                var video = await MediaPicker.Default.CaptureVideoAsync();
                if (video == null) return;

                // gebruik pad van de video
            }
        }*/
    }
}
