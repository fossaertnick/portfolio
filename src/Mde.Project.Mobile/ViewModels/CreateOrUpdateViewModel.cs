using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedMemoria), nameof(SelectedMemoria))]
    [QueryProperty(nameof(Mode), "mode")]
    public class CreateOrUpdateViewModel : ObservableObject
    {
        private readonly IMemoriaService _memorialService;

        // fields
        private Guid id;
        private string mode;
        private string pageTitle;
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

        // properties
        public Guid Id { get; set; }
        public string PageTitle
        {
            get { return pageTitle; }
            set
            {
                SetProperty(ref pageTitle, value);
            }
        }
        public string Mode 
        {
            set
            {
                if (value == "create")
                {
                    PageTitle = "Create ...";
                    SelectedMemoria = null;
                }
                else if (value == "update")
                {
                    PageTitle = "Update ...";
                }
            }
        }
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
            get { return selectedMemoria; }
            set 
            {
                if(SetProperty(ref selectedMemoria, value))
                {
                    if(selectedMemoria != null)
                    {
                        Id = selectedMemoria.Id;
                        Name = selectedMemoria.Name;
                        Description = selectedMemoria.Description;
                        Occation = selectedMemoria.Occation;
                        CreatedOn = selectedMemoria.CreatedOn;
                        Longitude = selectedMemoria.Longitude;
                        Latitude = selectedMemoria.Latitude;
                        City = selectedMemoria.City;
                        Country = selectedMemoria.Country;
                        Street = selectedMemoria.Street;
                        HouseNumber = selectedMemoria.HouseNumber;
                    }
                    else // (selectedMemoria == null)
                    {
                        Name = string.Empty;
                        Description = string.Empty;
                        Occation = default;
                        City = string.Empty;
                        Country = string.Empty;
                        Street = string.Empty;
                        HouseNumber = string.Empty;
                        CreatedOn = DateTime.Now;
                        Longitude = 0.0;
                        Latitude = 0.0;
                    }
                }
            }
        }
        public ICommand CancelCommand => new Command<string>(async (situation) =>
        {
            if (situation is string && situation == "cancel")
            {
                await Shell.Current.GoToAsync(nameof(ListPage));
            }
        });
        public ICommand CreateCommand => new Command(async () =>
        {
            await CreateOrUpdateMemoriaAsync();
        });
        public ICommand ClickForLocationCommand => new Command(async () =>
        {
            Location currentCoordinates = await _memorialService.GetCurrentCoordinatesAsync();
            if(currentCoordinates != null)
            {
                var (country, city, street, number) = await _memorialService.GetAddressConnectedToCoordinates(currentCoordinates);

                Country = country;
                City = city;
                Street = street;
                HouseNumber = number;
                Latitude = currentCoordinates.Latitude;
                Longitude = currentCoordinates.Longitude;
            }
        });

        // constructor
        public CreateOrUpdateViewModel(IMemoriaService memorialService)
        {
            _memorialService = memorialService;
        }

        // methoden
        private async Task CreateOrUpdateMemoriaAsync()
        {
            var memoria = SelectedMemoria ?? new Memoria();

            memoria.Name = Name;
            memoria.Country = Country;
            memoria.City = City;
            memoria.Street = Street;
            memoria.HouseNumber = HouseNumber;
            memoria.Latitude = Latitude;
            memoria.Longitude = Longitude;
            memoria.Description = Description;
            memoria.Occation = Occation;

            if(memoria.CreatedOn == default)
            {
                memoria.CreatedOn = CreatedOn;
            }

            memoria.LastEditedOn = DateTime.UtcNow;

            await _memorialService.SaveChangesAsync(memoria);
            await Shell.Current.GoToAsync(nameof(ListPage));
        }
    }
}
