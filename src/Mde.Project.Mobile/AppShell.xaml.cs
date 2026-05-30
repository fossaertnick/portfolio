using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Domain.Services;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Plugin.LocalNotification;
using System.Diagnostics;

namespace Mde.Project.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly IDeviceSystemService _toUser;

        // constructor
        public AppShell(IDeviceSystemService toUser)
        {
            InitializeComponent();
            _toUser = toUser;
        }

        // methoden
        protected override void OnAppearing()
        {
            base.OnAppearing();
            _ = InitializeNotification();
        }
        private async void ManualPage_Clicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(ManualPage));
        }
        private async void Logout_Clicked(object sender, EventArgs e)
        {
            bool bevestiging = await DisplayAlertAsync(
                                "Confirmation",
                                "Are you sure you want to close the app?",
                                "Yes",
                                "No"
);

            if (!bevestiging) return;

            _toUser.Close();
        }
        public async Task InitializeNotification()
        {
            try
            {
                if (!Preferences.ContainsKey(Constants.NotificationPermission))
                {
                    var granted = await LocalNotificationCenter.Current.RequestNotificationPermission();
                    Preferences.Set(Constants.NotificationPermission, granted);
                    if (granted) await _toUser.InitializeNotifications();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }
    }
}
