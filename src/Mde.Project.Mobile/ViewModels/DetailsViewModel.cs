using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using static System.Net.Mime.MediaTypeNames;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedLocation), "specifics")]
    public class DetailsViewModel : ObservableObject
    {
        private readonly IMemoriaService _memorialService;

        // fields
        private Memoria selectedLocation;

        // properties
        public Memoria SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                SetProperty(ref selectedLocation, value);
            }
        }

        // constructor
        public DetailsViewModel(IMemoriaService memorialService)
        {
            _memorialService = memorialService;
        }
    }
}
