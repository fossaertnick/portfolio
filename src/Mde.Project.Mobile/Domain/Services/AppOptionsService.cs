#if ANDROID
using Android.OS;
#endif
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Resources.Styles;
using System.Drawing;

namespace Mde.Project.Mobile.Domain.Services
{
    public class AppOptionsService 
    {
        private ResourceDictionary? _activeColorResource;

        // methoden (closing app)
        public void Close()
        {

#if ANDROID

            Process.KillProcess(Process.MyPid());

#elif WINDOWS

            Application.Current?.Quit();

#endif
        }

        // methoden (colorsettings)
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
                Preferences.Set("color_choice", (int)ColorChoice.System);
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

            Preferences.Set("color_choice", (int)choice);
        }
        public void LoadSavedTheme() 
        { 
            var stored = Preferences.Get("color_choice", (int)ColorChoice.System); 
            ApplyColorTheme((ColorChoice)stored); 
        }
    }
}

