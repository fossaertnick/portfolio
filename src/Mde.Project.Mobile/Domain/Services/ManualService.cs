using Mde.Project.Mobile.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services
{
    public class ManualService : IManualService
    {
        public async Task<string> HelpTheUserAsync()
        {
            using var stream = await FileSystem.OpenAppPackageFileAsync("manual.md");
            using var reader = new StreamReader(stream);

            return await reader.ReadToEndAsync();
        }
    }
}
