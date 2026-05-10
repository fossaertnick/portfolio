using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class CreateOrUpdateViewModel : ObservableObject, IQueryAttributable
    {
        private readonly IMediaService _mediaService;
        private readonly IMemoriaService _memoriaService;
        private readonly ILocationService _locationService;
        private readonly IGeoCodingService _geoCodingService;

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
            await TakePhotoAsync();
        });
        public ICommand PickPhotoCommand => new Command(async () =>
        {
            await PickPhotoAsync();
        });
        public ICommand TakeVideoCommand => new Command(async () =>
        {
            await TakeVideoAsync();
        });
        public ICommand DeleteTemporaryMediaItemCommand => new Command<MediaItem>( (image) =>
        {
            RemoveImage(image);
        });

        // constructor
        public CreateOrUpdateViewModel(IMediaService mediaService, IGeoCodingService geoCodingService, IMemoriaService memoriaService, ILocationService locationService)
        {
            _mediaService = mediaService;
            _geoCodingService = geoCodingService;
            _memoriaService = memoriaService;
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
                Longitude = SelectedMemoria.MemoriaAddress.Longitude;
                Latitude = SelectedMemoria.MemoriaAddress.Latitude;
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

            var adresFound = await _geoCodingService.ReverseGeoCodingAsync(currentCoordinates);
            if (adresFound == null) return;

            Country = adresFound.Country;
            City = adresFound.City;
            Street = adresFound.Street;
            Latitude = currentCoordinates.Latitude;
            Longitude = currentCoordinates.Longitude;
        }
        private async Task CreateOrUpdateMemoriaAsync()
        {
            if(CheckIncomingValues() == (true, string.Empty))
            {
                Memoria memoria;

                if(SelectedMemoria == null)
                {
                    memoria = new Memoria
                    {
                        Name = Name,
                        MemoriaAddress = new Address
                        {
                            Country = Country,
                            City = City,
                            Street = Street,
                            HouseNumber = HouseNumber,
                            Latitude = Latitude,
                            Longitude = Longitude
                        },
                        Description = Description,
                        Occation = Occation,
                        MediaMaterial = TemporaryItems.ToList(),
                    };
                }
                else
                {
                    memoria = new Memoria
                    {
                        Id = SelectedMemoria.Id,
                        Name = Name,
                        MemoriaAddress = new Address
                        {
                            Country = Country,
                            City = City,
                            Street = Street,
                            HouseNumber = HouseNumber,
                            Latitude = Latitude,
                            Longitude = Longitude
                        },
                        Description = Description,
                        Occation = Occation,
                        CreatedOn = SelectedMemoria.CreatedOn,
                        LastEditedOn = DateTime.Now,
                        MediaMaterial = TemporaryItems.Select(m => new MediaItem
                        {
                            Id = m.Id,
                            Type = m.Type,
                            FilePath = m.FilePath,
                            MemoriaId = m.MemoriaId,
                        }).ToList()
                    };
                }
                TemporaryItems.Clear();

                await _memoriaService.SaveMemoriaAsync(memoria);

                await Shell.Current.GoToAsync(nameof(ListPage));
            }
            else
            {
                string message = CheckIncomingValues().Item2;
                await Application.Current.MainPage.DisplayAlert(
                    "Iets ging fout met je ingave",
                    $"{message}",
                    "OK"
                );
            }
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
        private void RemoveImage(MediaItem image)
        {
            if (image == null) return;

            TemporaryItems.Remove(image);
        }
        private async Task PickPhotoAsync()
        {
            var photo = await MediaPicker.Default.PickPhotoAsync();
            if (photo == null) return;

            var mediaItem = await _mediaService.SavePhotoASync(photo);
            TemporaryItems.Add(mediaItem);
        }
        private async Task TakePhotoAsync()
        {
            if (!MediaPicker.Default.IsCaptureSupported) return;

            var photo = await MediaPicker.Default.CapturePhotoAsync();
            if (photo == null) return;

            var mediaItem = await _mediaService.SavePhotoASync(photo);
            TemporaryItems.Add(mediaItem);
        }
        private async Task TakeVideoAsync()
        {
            if (!MediaPicker.Default.IsCaptureSupported) return;

            var video = await MediaPicker.Default.CaptureVideoAsync();
            Console.WriteLine(video?.FullPath ?? "null");
            if (video == null) return;

            var mediaItem = await _mediaService.SaveVideoAsync(video);
            TemporaryItems.Add(mediaItem);
        }
        private (bool, string) CheckIncomingValues()
        {
            string message = string.Empty;
            if (string.IsNullOrWhiteSpace(Country) || string.IsNullOrWhiteSpace(City) || string.IsNullOrWhiteSpace(Street))
            {
                message = "Het land, stad en straat moeten ingevuld zijn";
                return (false, message);
            }
            if (string.IsNullOrWhiteSpace(Name))
            {
                message = "De naam van de Memoria moet ingevuld zijn";
                return (false, message);
            }
            return (true, message);
        }
    }
}
