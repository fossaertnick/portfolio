using Mde.Project.Mobile.Domain.Models.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IStatisticService
    {
        // methoden
        int GetTotalMemorias();
        int GetMemoriasByOccasionAsync(OccationType type);
        int GetPhotoCountAsync();
        int GetVideoCountAsync();
        string GetFavoriteCountryAsync();
    }
}
