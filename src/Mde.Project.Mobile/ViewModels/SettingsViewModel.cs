using CommunityToolkit.Mvvm.ComponentModel;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;

namespace Mde.Project.Mobile.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        // properties
        public ICommand ChangeAvatarCommand => new Command(async () =>
        {
            await Shell.Current.GoToAsync("//settingsPage");
        });
    }
}
