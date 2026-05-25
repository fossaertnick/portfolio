using CommunityToolkit.Mvvm.ComponentModel;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Plugin.LocalNotification;
using Microsoft.Maui.Storage;
using Mde.Project.Mobile.Core.Entities.Enums;

namespace Mde.Project.Mobile.ViewModels
{
    public class SettingsViewModel : ObservableObject
    {
        private readonly IRawToUserService _toUser;

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
        public SettingsViewModel(IRawToUserService toUser)
        {
            _toUser = toUser;
            notificationsEnabled = Preferences.Get("notification_permission", false);
        }

        // methoden
        public async Task Refresh()
        {
            var previous = Preferences.Get("notification_permission", false);
            var current = await LocalNotificationCenter.Current.AreNotificationsEnabled();
            Preferences.Set("notification_permission", current);
            _updatingInternally = true;
            NotificationsEnabled = current;
            _updatingInternally = false;
            if (current && !previous) await _toUser.InitializeNotifications();

            if (!current && previous) await _toUser.StopNotifications();
        }
        private Task UpdateNotificationSettings()
        {
            AppInfo.Current.ShowSettingsUI();
            return Task.CompletedTask;
        }
        public void SaveColorChoice(ColorChoice choice)
        {
            Preferences.Set("color_choice", (int)choice);
        }
    }
}
