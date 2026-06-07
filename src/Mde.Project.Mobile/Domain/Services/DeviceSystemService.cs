#if ANDROID
using Android.OS;
#endif

using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Mde.Project.Mobile.Resources.Styles;

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
                if (dict is Purple || dict is Sober || dict is Blue)
                {
                    merged.Remove(dict);
                }
            }

            _activeColorResource = null;

            if (choice == ColorChoice.Green)
            {
                Preferences.Set(Constants.ColorChoice, (int)ColorChoice.Green);
                return;
            }

            _activeColorResource = choice switch
            {
                ColorChoice.Blue => new Blue(),
                ColorChoice.Purple => new Purple(),
                ColorChoice.Sober => new Sober(),
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
            var stored = Preferences.Get(Constants.ColorChoice, (int)ColorChoice.Green);
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
    }
}
