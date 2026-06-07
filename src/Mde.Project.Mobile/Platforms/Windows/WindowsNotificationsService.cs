using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.UI.Dispatching;
using System.Diagnostics;

namespace Mde.Project.Mobile.Platforms.Windows
{
    public class WindowsNotificationsService : IPushNotificationService
    {
        private Timer? _timer;
        private List<string> _phrases = new List<string>();
        private const string LastNotificationTime = "LastNotificationTime";

        // methoden (chatterbox to user)
        public async Task InitializeNotifications()
        {
            _phrases = await ReadPhrases();

            var lastTicks = Preferences.Get(LastNotificationTime, 0L);
            if(lastTicks == 0)
            {
                ShowNotifications(null);
                Preferences.Set(LastNotificationTime, DateTime.UtcNow.Ticks);
            }

            _timer = new Timer(CheckNotification, null, TimeSpan.FromMinutes(1), TimeSpan.FromMinutes(1));
        }

        public Task<bool> RequestPermission()
        {
            throw new NotImplementedException();
        }

        public Task StopNotifications()
        {
            throw new NotImplementedException();
        }

        // ondersteunende methoden
        private async Task<List<string>> ReadPhrases()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("chatterBox.md");
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();

            return content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        private void ShowNotifications(object? state)
        {
            var phrase = _phrases[Random.Shared.Next(_phrases.Count)];
            try
            {
                new ToastContentBuilder()
                    .AddText("Memoriá")
                    .AddText(phrase)
                    .Show();

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to show notification: {ex}");
            }
        }
        private void CheckNotification(object? state)
        {
            var lastTicks = Preferences.Get(LastNotificationTime, 0L);
            var lastTime = new DateTime(lastTicks, DateTimeKind.Utc);
            if(DateTime.UtcNow - lastTime >= TimeSpan.FromHours(8))
            {
                ShowNotifications(null);
                Preferences.Set(LastNotificationTime, DateTime.UtcNow.Ticks);
            }
        }

        public Task<bool> AreNotficationsEnabled()
        {
            throw new NotImplementedException();
        }
    }
}
