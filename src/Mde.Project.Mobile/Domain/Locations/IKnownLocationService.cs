using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations
{
    public interface IKnownLocationService
    {
        // methodes
        public Task<IEnumerable<KnownLocation>> GetKnownLocationsAsync(string searchTerm);
        public Task<IEnumerable<KnownLocation>> GetAllKnownLocationsAsync();
    }
}
