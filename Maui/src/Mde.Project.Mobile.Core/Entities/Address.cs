using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Entities
{
    public class Address
    {
        // properties
        public Guid Id { get; set; } = Guid.NewGuid();
        public string City { get; set; }
        public string Country { get; set; }
        public string Street { get; set; }
        public string? HouseNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }

        // navigation properties
        public Memoria Memoria { get; set; }
    }
}
