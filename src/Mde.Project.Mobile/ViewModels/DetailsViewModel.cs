using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.Mime.MediaTypeNames;

namespace Mde.Project.Mobile.ViewModels
{
    [QueryProperty(nameof(SelectedLocation), "specifics")]
    public class DetailsViewModel : ObservableObject
    {
        // fields
        private KnownLocation selectedLocation;

        // properties
        public KnownLocation SelectedLocation
        {
            get { return selectedLocation; }
            set
            {
                SetProperty(ref selectedLocation, value);
            }
        }
    }
}
