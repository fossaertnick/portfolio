using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;

namespace Mde.Project.Mobile.Domain.Services
{
    public class RawToUserService : IRawToUserService
    {
        // methoden
        public async Task<ResultModel<string>> HelpTheUserAsync()
        {
            try
            {
                using var stream = await FileSystem.OpenAppPackageFileAsync("manual.md");
                using var reader = new StreamReader(stream);
                var content = await reader.ReadToEndAsync();
                if (string.IsNullOrWhiteSpace(content)) return ResultModel<string>.Failure("Manual file was empty", "The manual file does not contain anything.");

                return ResultModel<string>.Success(content);
            }
            catch (FileNotFoundException ex)
            {
                return ResultModel<string>.Failure(ex.ToString(), "The manual could not be found.");
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure(ex.ToString(), "Something went wrong while opening the manual.");
            }
        }


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
        private async Task<List<string>> ReadPhrases()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("chatterBox.md");
            using var reader = new StreamReader(stream);
            var content = await reader.ReadToEndAsync();

            return content.Split(Environment.NewLine, StringSplitOptions.RemoveEmptyEntries).ToList();
        }
        public Task StopNotifications()
        {
            LocalNotificationCenter.Current.CancelAll();

            return Task.CompletedTask;
        }
    }
}
