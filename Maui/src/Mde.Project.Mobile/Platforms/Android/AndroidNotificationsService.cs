using Mde.Project.Mobile.Core.Services.Interfaces;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace Mde.Project.Mobile.Platforms.Android
{
    public class AndroidNotificationsService : IPushNotificationService
    {
        public Task<bool> AreNotficationsEnabled()
        {
            return LocalNotificationCenter.Current.AreNotificationsEnabled();
        }

        // methoden (chatterbox to user)
        public async Task InitializeNotifications()
        {
            LocalNotificationCenter.Current.CancelAll();

            var phrases = await ReadPhrases();
            for (int chatterbox = 0; chatterbox < (30 * 3); chatterbox++)
            {
                var request = new NotificationRequest
                {
                    NotificationId = chatterbox + 1,
                    Title = "Memoriá",
                    Description = phrases[Random.Shared.Next(phrases.Count)],
                    Schedule = new NotificationRequestSchedule
                    {
                        NotifyTime = DateTime.Now.AddHours(chatterbox * 8)
                    }
                };
                await LocalNotificationCenter.Current.Show(request);
            }
        }

        public async Task<bool> RequestPermission()
        {
            return await LocalNotificationCenter.Current.RequestNotificationPermission();
        }

        public Task StopNotifications()
        {
            LocalNotificationCenter.Current.CancelAll();

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
    }
}
