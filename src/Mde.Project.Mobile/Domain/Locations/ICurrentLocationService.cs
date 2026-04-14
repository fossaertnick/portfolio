using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations
{
    public interface ICurrentLocationService
    {
        // properties
        Location LastLocation { get; set; }

        // methodes
        Task StartTrackingAsync();
        void StopTracking();
    }
}
