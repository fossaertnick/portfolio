using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class MapViewModel : ObservableObject
    {
        // properties
        public ICommand NavigationCommand => new Command<string>(async (destination) =>
        {
            if (destination == "add")
            {
                await Shell.Current.GoToAsync("add");
            }
            else if (destination == "list")
            {
                await Shell.Current.GoToAsync("list");
            }
            else
            {
                await Shell.Current.GoToAsync("//settingsPage");
            }
        });
    }
}
