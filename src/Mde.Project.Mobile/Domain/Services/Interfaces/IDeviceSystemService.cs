using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IDeviceSystemService
    {
        // methode (veranderen kleur systeem)
        void ApplyColorTheme(ColorChoice choice);
        void LoadSavedTheme();

        // methode (app sluiten)
         void Close();

        // methoden (manual to user)
        Task<ResultModel<string>> HelpTheUserAsync();
    }
}
