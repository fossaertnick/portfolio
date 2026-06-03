using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Microsoft.Toolkit.Uwp.Notifications;
using Microsoft.UI.Dispatching;

namespace Mde.Project.Mobile.Platforms.Windows
{
    public class WindowsNotificationsService : IPushNotificationService
    {
        private Timer? _timer;
        private List<string> _phrases = new List<string>();
        private static DispatcherQueue? _dispatcher;

        // methoden (chatterbox to user)
        public async Task InitializeNotifications()
        {
            _phrases = await ReadPhrases();
            _timer = new Timer(ShowNotifications, null, TimeSpan.Zero, TimeSpan.FromHours(8));
        }

        public Task<bool> RequestPermission()
        {
            return Task.FromResult(true);
        }

        public Task StopNotifications()
        {
            _timer?.Dispose();
            _timer = null;
            return Task.CompletedTask;
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
            _dispatcher?.TryEnqueue(() =>
            {
                new ToastContentBuilder()
                    .AddText("Memoriá")
                    .AddText(phrase)
                    .Show();
            });
        }
        public static void SetDispatcher(DispatcherQueue dispatcher)
        {
            _dispatcher = dispatcher;
        }

        public Task<bool> AreNotficationsEnabled()
        {
            return Task.FromResult(Preferences.Get(Constants.NotificationPermission, false));
        }
    }
}
