using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IGeoCodingService
    {
        // map logica
        Task<Location?> ForwardGeoCodeAsync(string address);
        Task<Address?> ReverseGeoCodingAsync(Location coordinates);
    }
}
