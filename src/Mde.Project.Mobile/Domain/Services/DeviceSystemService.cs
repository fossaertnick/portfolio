#if ANDROID
using Android.OS;
#endif

using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Resources.Styles;
using Plugin.LocalNotification;
using Plugin.LocalNotification.Core.Models;
using Environment = System.Environment;

namespace Mde.Project.Mobile.Domain.Services
{
    public class DeviceSystemService : IDeviceSystemService
    {
        private ResourceDictionary? _activeColorResource;

        // methode (veranderen kleur systeem)
        public void ApplyColorTheme(ColorChoice choice)
        {
            var merged = Application.Current.Resources.MergedDictionaries;

            foreach (var dict in merged.ToList())
            {
                if (dict is Purple || dict is Green || dict is blue)
                {
                    merged.Remove(dict);
                }
            }

            _activeColorResource = null;

            if (choice == ColorChoice.System)
            {
                Preferences.Set(Constants.ColorChoice, (int)ColorChoice.System);
                return;
            }

            _activeColorResource = choice switch
            {
                ColorChoice.Blue => new blue(),
                ColorChoice.Purple => new Purple(),
                ColorChoice.Green => new Green(),
                _ => null
            };

            if (_activeColorResource != null)
            {
                merged.Add(_activeColorResource);
            }

            Preferences.Set(Constants.ColorChoice, (int)choice);
        }
        public void LoadSavedTheme()
        {
            var stored = Preferences.Get(Constants.ColorChoice, (int)ColorChoice.System);
            ApplyColorTheme((ColorChoice)stored);
        }

        // methode (app sluiten)
        public void Close()
        {

#if ANDROID

            Process.KillProcess(Process.MyPid());

#elif WINDOWS

            Application.Current?.Quit();

#endif
        }

        // methoden (manual to user)
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
