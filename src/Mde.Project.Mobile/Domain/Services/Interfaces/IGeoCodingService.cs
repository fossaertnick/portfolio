using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IGeoCodingService
    {
        // methoden
        Task<ResultModel<bool>> ForwardGeoCodeAsync(Memoria saveMemoria);
        Task<ResultModel<Address>> ReverseGeoCodingAsync(Location coordinates);
    }
}
