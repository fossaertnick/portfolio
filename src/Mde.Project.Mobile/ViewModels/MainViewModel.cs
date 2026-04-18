using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Pages;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class MainViewModel : ObservableObject
    {
        // properties
        public ICommand NavigationCommand => new Command<string>(async (destination) =>
        {
            if (destination == "add")
            {
                await Shell.Current.GoToAsync($"{nameof(CreateOrUpdatePage)}?mode=create");
            }
            else if (destination == "list")
            {
                await Shell.Current.GoToAsync(nameof(ListPage));
            }
            else
            {
                await Shell.Current.GoToAsync("//settingsPage");
            }
        });
    }
}
