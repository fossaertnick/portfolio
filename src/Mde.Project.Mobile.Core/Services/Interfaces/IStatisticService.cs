using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Services.Interfaces
{
    public interface IStatisticService
    {
        // methoden
        Task<ResultModel<string>> GetTotalMemorias();
        Task<ResultModel<string>> GetMemoriasByOccasionAsync(OccationType type);
        Task<ResultModel<string>> GetPhotoCountAsync();
        Task<ResultModel<string>> GetVideoCountAsync();
        Task<ResultModel<string>> GetFavoriteCountryAsync();
    }
}
