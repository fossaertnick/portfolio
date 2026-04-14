using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    class MockCurrentLocationService : ICurrentLocationService
    {
        public Location LastLocation { get; set; } = null;

        public async Task StartTrackingAsync()
        {
            await Task.Delay(500);
            LastLocation = new Location(0, 0);
        }

        public void StopTracking()
        {
            LastLocation = null;
        }
    }
}
