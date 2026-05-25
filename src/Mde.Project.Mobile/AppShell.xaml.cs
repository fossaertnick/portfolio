using Mde.Project.Mobile.Domain.Services;
using Plugin.LocalNotification;
using System.Diagnostics;

namespace Mde.Project.Mobile
{
    public partial class AppShell : Shell
    {
        private readonly AppCloser _closer;
        private readonly RawToUserService _toUser;

        // constructor
        public AppShell()
        {
            InitializeComponent();
            _closer = new AppCloser();
            _toUser = new RawToUserService();
        }

        // methoden
        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await InitializeNotification();
        }
        private async Task InitializeNotification()
        {
            try
            {
                if (!Preferences.ContainsKey("notification_permission"))
                {
                    var granted = await LocalNotificationCenter.Current.RequestNotificationPermission();
                    Preferences.Set("notification_permission", granted);
                    if (granted) await _toUser.InitializeNotifications();
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
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

            _closer.Close();
        }
    }
}
