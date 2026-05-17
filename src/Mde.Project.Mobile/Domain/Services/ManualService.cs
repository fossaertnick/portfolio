using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile.Domain.Services
{
    public class ManualService : IManualService
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
    }
}
