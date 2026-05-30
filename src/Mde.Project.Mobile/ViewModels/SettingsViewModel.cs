using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Plugin.LocalNotification;

namespace Mde.Project.Mobile.ViewModels
{
    public partial class SettingsViewModel : BaseViewModel
    {
        private readonly IDeviceSystemService _toUser;

        // field
        private bool notificationsEnabled;
        private bool _updatingInternally;

        // properties
        public bool NotificationsEnabled
        {
            get { return notificationsEnabled; }
            set
            {
                if (SetProperty(ref notificationsEnabled, value))
                {
                    if (!_updatingInternally) _ = UpdateNotificationSettings();
                }
            }
        }

        // constructor
        public SettingsViewModel(IDeviceSystemService toUser)
        {
            _toUser = toUser;
            NotificationsEnabled = Preferences.Get("", false);
        }

        // methoden
        public async Task Refresh()
        {
            var previous = Preferences.Get(Constants.NotificationPermission, false);
            var current = await LocalNotificationCenter.Current.AreNotificationsEnabled();
            Preferences.Set(Constants.NotificationPermission, current);
            _updatingInternally = true;
            NotificationsEnabled = current;
            _updatingInternally = false;
            if (current && !previous) await _toUser.InitializeNotifications();

            if (!current && previous) await _toUser.StopNotifications();
        }
        private async Task UpdateNotificationSettings()
        {
            AppInfo.Current.ShowSettingsUI();
            return;
        }
        public void SaveColorChoice(ColorChoice choice)
        {
            Preferences.Set(Constants.ColorChoice, (int)choice);
        }
    }
}
