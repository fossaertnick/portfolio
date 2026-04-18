using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Mvvm.ComponentModel;
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
        private string occation;
        private double longitude;
        private double latitude;
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
        public string Occation
        {
            get { return occation; }
            set => SetProperty(ref occation, value);
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
                    }
                    else // (selectedMemoria == null)
                    {
                        Name = default;
                        Description = default;
                        Occation = default;
                        CreatedOn = DateTime.Now;
                        Longitude = default;
                        Latitude = default;
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
            CreateOrUpdateMemoriaAsync();
        });

        // constructor
        public CreateOrUpdateViewModel(IMemoriaService memorialService)
        {
            _memorialService = memorialService;
        }

        // methoden
        private async void CreateOrUpdateMemoriaAsync()
        {
            Memoria memoria = new Memoria();
            if (SelectedMemoria == null)
            {
                memoria = new Memoria();
                memoria.CreatedOn = CreatedOn;
            }
            else
            {
                memoria = SelectedMemoria;
            }

            memoria.Name = Name;
            memoria.Longitude = Longitude;
            memoria.Latitude = Latitude;
            memoria.Description = Description;
            memoria.Occation = Occation;
            memoria.LastEditedOn = DateTime.UtcNow;

            if(memoria.Id.Equals(Guid.Empty))
            {
                await _memorialService.CreateMemoriaAsync(memoria);
                await Toast.Make("Memoria created successfully!").Show();
            }
            else
            {
                await _memorialService.UpdateMemoriaAsync(memoria);
                await Toast.Make("Memoria updated successfully!").Show();
            }

            await Shell.Current.GoToAsync(nameof(ListPage));
        }
        private async Task GetCurrentCoordinatesASync()
        {
            try
            {
                var location = await Geolocation.GetLocationAsync();
                if(location != null)
                {
                    Latitude = location.Latitude;
                    Longitude = location.Longitude;
                }
            }
            catch
            {
                Latitude = default;
                Longitude = default;
            }
        }
        public async Task InitializeNewMemoriaAsync()
        {
            Name = default;
            Description = default;
            Occation = default;
            CreatedOn = DateTime.Now;

            await GetCurrentCoordinatesASync();
        }
        public async Task CreateNewMemoriaAsync()
        {
            SelectedMemoria = null;
            await InitializeNewMemoriaAsync();
        }
    }
}
