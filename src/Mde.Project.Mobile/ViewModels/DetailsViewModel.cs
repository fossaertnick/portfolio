using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Models.enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Pages;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Mde.Project.Mobile.ViewModels
{
    public class DetailsViewModel : ObservableObject, IQueryAttributable
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


        // Commands
        public ICommand UpdateMemoriaCommand => new Command<Guid>(async (memoriaDetailsId) =>
        {
            if (memoriaDetailsId != Guid.Empty)
            {
                await Shell.Current.GoToAsync($"{nameof(CreateOrUpdatePage)}?id={memoriaDetailsId}");
            }
        });
        public ICommand DeleteMemoriaCommand => new Command<Guid>(async (specificMemoriaId) =>
        {
            ExecuteDeleteMemoriaCommand(specificMemoriaId);
        });

        // constructor
        public DetailsViewModel(IMemoriaService memoriaService)
        {
            _memoriaService = memoriaService;
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
                    SelectedLocation = await _memoriaService.GetMemoriaByIdAsync(id);

                    LoadExistingImages();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        private void LoadExistingImages()
        {
            var list = new List<MediaItem>();

            if (SelectedLocation?.MediaMaterial != null)
            {
                foreach (var media in SelectedLocation.MediaMaterial)
                {
                    list.Add(new MediaItem
                    {
                        FilePath = media.FilePath,
                        ImageSource = ImageSource.FromFile(media.FilePath)
                    });
                }
            }
            TemporaryItems = new ObservableCollection<MediaItem>(list);
        }
        private async Task ExecuteDeleteMemoriaCommand(Guid specificMemoriaId)
        {
            if (specificMemoriaId != Guid.Empty)
            {
                await _memoriaService.DeleteMemoriaAsync(specificMemoriaId);
                await Shell.Current.GoToAsync(nameof(ListPage));
            }
        }
    }
}
