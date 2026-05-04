using Mde.Project.Mobile.Domain.Locations;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface ISeedingService
    {
        // properties
        public List<Memoria> Locations { get; set; }

        // methoden
        void SeedingMemoria();
    }
}
