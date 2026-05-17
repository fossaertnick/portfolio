using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Dtos.Addresses
{
    public class AddressResponseDTo
    {
        // properties
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? HouseNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
