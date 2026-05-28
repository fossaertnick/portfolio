using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System.Collections.ObjectModel;
using System.Runtime.CompilerServices;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class CreateOrUpdateViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IMediaService _mediaService;
        private readonly IMemoriaService _memoriaService;
        private readonly ILocationService _locationService;
        private readonly IGeoCodingService _geoCodingService;
        private readonly ISpeechToTextService _speechToTextService;

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
        private DateTime eventDate = DateTime.Now;
        private TimeSpan eventTime = DateTime.Now.TimeOfDay;
        private Memoria selectedMemoria;
        private ImageSource photo;
        private double progressBar;
        private bool isSaving;
        private bool isListening;

        // properties
        public EditMode EditMode
        {
            get { return editMode; }
            set
            {
                if(SetProperty(ref editMode, value))
                {
                    OnPropertyChanged(nameof(IsCreateMode));
                    OnPropertyChanged(nameof(IsUpdateMode));
                }
            }
        }
        public bool IsCreateMode => EditMode == EditMode.Create;
        public bool IsUpdateMode => EditMode == EditMode.Update;
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
        public DateTime EventDate
        {
            get { return eventDate; }
            set => SetProperty(ref eventDate, value);
        }
        public TimeSpan EventTime
        {
            get { return eventTime; }
            set => SetProperty(ref eventTime, value);
        }
        public DateTime FullEventDate
        {
            get { return EventDate.Date + eventTime; }
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
        public double ProgressBar
        {
            get { return progressBar; }
            set
            {
                SetProperty(ref progressBar, value);
            }
        }
        public bool IsSaving
        {
            get { return isSaving; }
            set
            {
                SetProperty(ref  isSaving, value);
            }
        }
        public bool IsListening
        {
            get { return isListening; }
            set
            {
                SetProperty<bool>(ref isListening, value);
            }
        }
        public ObservableCollection<MediaItem> TemporaryItems { get; set; } = new();

        // commands
        public ICommand CancelCommand => new Command<string>(async (situation) =>
        {
            await ExecuteCancelCommand(situation);
        });
        public ICommand CreateCommand => new Command(async () =>
        {
            await CreateOrUpdateMemoriaAsync();
        });
        public ICommand ClickForLocationCommand => new Command(async () =>
        {
            await GiveUserHisCurrentLocation();
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
        public ICommand DeleteTemporaryMediaItemCommand => new Command<MediaItem>(async (image) =>
        {
            await RemoveImage(image);
        });

        // constructor
        public CreateOrUpdateViewModel(IMediaService mediaService, IGeoCodingService geoCodingService, IMemoriaService memoriaService, ILocationService locationService, ISpeechToTextService speechToTextService)
        {
            _mediaService = mediaService;
            _geoCodingService = geoCodingService;
            _memoriaService = memoriaService;
            _locationService = locationService;
            _speechToTextService = speechToTextService;
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
                IsBusy = true;
                if (query.TryGetValue("id", out var value) && Guid.TryParse(value?.ToString(), out var id))
                {
                    await InitUpdate(id);
                }
                else if (query.ContainsKey("Latitude") && query.ContainsKey("Longitude"))
                {
                    InitCreate();
                    Country = query["Country"]?.ToString() ?? string.Empty;
                    City = query["City"]?.ToString() ?? string.Empty;
                    Street = query["Street"]?.ToString() ?? string.Empty;
                    HouseNumber = query["HouseNumber"]?.ToString() ?? string.Empty;
                    Latitude = Convert.ToDouble(query["Latitude"]);
                    Longitude = Convert.ToDouble(query["Longitude"]);
                }
                else
                {
                    InitCreate();
                }
            }
            finally
            {
                IsBusy = false;
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
            var result = await _memoriaService.GetMemoriaByIdAsync(id);
            var memoria = await HandleResult(result);
            if (memoria == null) return;

            SelectedMemoria = memoria;
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
            EventDate = DateTime.Now;
            EventTime = DateTime.Now.TimeOfDay;
            Latitude = 0;
            Longitude = 0;
            TemporaryItems.Clear();
        }
        private async Task GiveUserHisCurrentLocation()
        {
            try
            {
                IsBusy = true;
                var permissionResult = await _locationService.EnsureLocationPermission();
                var hasPermission = await HandleResult(permissionResult);
                if(hasPermission != true) return;

                var locationResult = await _locationService.GetCurrentLocationAsync();
                var currentCoordinates = await HandleResult(locationResult);
                if(currentCoordinates ==  null) return;

                var addressResult = await _geoCodingService.ReverseGeoCodingAsync(currentCoordinates);
                var addressFound = await HandleResult(addressResult);
                if(addressFound == null) return;

                Country = addressFound.Country;
                City = addressFound.City;
                Street = addressFound.Street;
                HouseNumber = addressFound.HouseNumber ?? string.Empty;
                Latitude = currentCoordinates.Latitude;
                Longitude = currentCoordinates.Longitude;
            }
            finally
            {
                IsBusy = false;
            }

        } 
        private async Task CreateOrUpdateMemoriaAsync()
        {
            try
            {
                IsBusy = true;
                IsSaving = true;
                ProgressBar = 0.1;
                var validation = CheckIncomingValues();
                if(!validation.Item1)
                {
                    await Shell.Current.DisplayAlertAsync(
                        "Error",
                        validation.Item2,
                        "OK");

                    return;
                }

                ProgressBar = 0.25;

                Memoria memoria;
                if(SelectedMemoria == null)
                {
                    memoria = new Memoria
                    {
                        Name = Name,
                        EventDate = FullEventDate,
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
                        Description = Description,
                        Occation = Occation,
                        EventDate = selectedMemoria.EventDate,
                        MemoriaAddress = new Address
                        {
                            Country = Country,
                            City = City,
                            Street = Street,
                            HouseNumber = HouseNumber,
                            Latitude = Latitude,
                            Longitude = Longitude
                        },
                        MediaMaterial = TemporaryItems.Select(m => new MediaItem
                        {
                            Type = m.Type,
                            FilePath = m.FilePath,
                            MemoriaId = m.MemoriaId,
                        }).ToList()
                    };
                }
                ProgressBar = 0.50;

                var geoResult = await _geoCodingService.ForwardGeoCodeAsync(memoria);
                var geoSuccess = await HandleResult(geoResult);
                if (geoSuccess != true) return;

                progressBar = 0.75;

                var saveResult = await _memoriaService.SaveMemoriaAsync(memoria);
                var saveSuccess = await HandleResult(saveResult);
                TemporaryItems.Clear();
                if (saveSuccess != true)
                {
                    IsSaving = false;
                    ProgressBar = 0;
                    ResetFields();
                    await Shell.Current.GoToAsync(nameof(ListPage));
                    return;
                }
                
                ProgressBar = 1.0;
                await Task.Delay(150);

                await Shell.Current.GoToAsync(nameof(ListPage));
                IsSaving = false;
                ProgressBar = 0;
            }
            finally
            {
                IsBusy = false;
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
        private async Task RemoveImage(MediaItem image)
        {
            try
            {
                IsBusy = true;
                if (image == null) return;

                TemporaryItems.Remove(image);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task PickPhotoAsync()
        {
            try
            {
                IsBusy = true;
                var photo = await MediaPicker.Default.PickPhotoAsync();
                if (photo == null) return;

                var result = _mediaService.PrepareMediaItem(photo);
                var mediaItem = await HandleResult(result);
                if(mediaItem == null) return;

                TemporaryItems.Add(mediaItem);
            }
            finally
            {
                IsBusy = false;
            }
        } 
        private async Task TakePhotoAsync()
        {
            try
            {
                IsBusy = true;
                if (!MediaPicker.Default.IsCaptureSupported) return;

                var photo = await MediaPicker.Default.CapturePhotoAsync();
                if (photo == null) return;

                var result = _mediaService.PrepareMediaItem(photo);
                var mediaItem = await HandleResult(result);
                if(mediaItem == null) return;

                TemporaryItems.Add(mediaItem);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task TakeVideoAsync()
        {
            try
            {
                IsBusy = true;
                if (!MediaPicker.Default.IsCaptureSupported) return;

                var video = await MediaPicker.Default.CaptureVideoAsync();
                if (video == null) return;

                var result = await _mediaService.SaveVideoAsync(video);
                var mediaItem = await HandleResult(result);
                if(mediaItem == null) return;

                TemporaryItems.Add(mediaItem);
            }
            finally
            {
                IsBusy = false;
            }
        }
        private async Task ExecuteCancelCommand(string situation)
        {
            if (situation == "cancel")
            {
                bool bevestiging = await Shell.Current.DisplayAlertAsync(
                "Cancel",
                "You sure you want to cancel this action?",
                "Yes",
                "No"
                );

                if (!bevestiging) return;
            {
                try
                {
                    IsBusy = true;
                    var result = await _mediaService.DeleteMediaItemsCollectionAsync(TemporaryItems);
                    var succes = await HandleResult(result);
                    if (succes != true) return;

                    await Shell.Current.GoToAsync(nameof(ListPage));
                }
                finally
                {
                    TemporaryItems.Clear();
                    IsBusy = false;
                }
            }

            }
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
        public async void StartSpeech()
        {
            try
            {
                IsBusy = true;

                var status = await Permissions.RequestAsync<Permissions.Microphone>();
                if (status != PermissionStatus.Granted)
                {
                    await Shell.Current.DisplayAlertAsync("Error", "No microphone permission", "ok");
                    return;
                }
                IsListening = true;

                _speechToTextService.StartListening(text =>
                {
                    MainThread.BeginInvokeOnMainThread(() =>
                    {
                        if (string.IsNullOrWhiteSpace(text)) return;
                        ApplySpeechTofields(text);
                    });
                });
            }
            finally
            {
                IsListening = false;
                IsBusy = false;
            }
        }
        public void StopSpeech()
        {
            IsListening = false;
            _speechToTextService?.StopListening();
        }
        private void ApplySpeechTofields(string text)
        {
            text = text.ToLower();

            if (text.Contains("naam")) Name = ExtractAfter(text, "naam");
            if (text.Contains("beschrijving")) Description = ExtractAfter(text, "beschrijving");
            if (text.Contains("land")) Country = ExtractAfter(text, "land");
            if (text.Contains("stad")) City = ExtractAfter(text, "stad");
            if (text.Contains("straat")) Street = ExtractAfter(text, "straat");
            if (text.Contains("nummer")) HouseNumber = ExtractAfter(text, "nummer");
        }
        private string ExtractAfter(string text, string description)
        {
            var index = text.IndexOf(description);
            if (index == -1) return string.Empty;

            var result = text[(index + description.Length)..].Trim();

            var stopWord = new[]
            {
                "naam",
                "beschrijving",
                "land",
                "stad",
                "straat",
                "nummer",
            };

            foreach(var stop in stopWord)
            {
                var stopIndex = result.IndexOf(stop);
                if (stopIndex > -1) result = result[..stopIndex].Trim();
            }

            return result;
        }
    }
}
