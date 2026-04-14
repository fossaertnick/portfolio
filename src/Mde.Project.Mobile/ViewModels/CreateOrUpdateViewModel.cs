using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class CreateOrUpdateViewModel : ObservableObject
    {
        // properties
        public ICommand SpecificActionCommand => new Command<string>(async (destination) =>
        {
            if (destination == "list")
            {
                await Application.Current.MainPage.DisplayAlert("Success", "De locatie is succesvol opgeslagen!", "OK");
                await Shell.Current.GoToAsync("list");
            }
            else if (destination == "cancel")
            {
                await Shell.Current.GoToAsync("list");
            }
        });
    }
}
