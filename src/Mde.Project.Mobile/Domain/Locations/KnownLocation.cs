using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations
{
    public class KnownLocation : Location
    {
        // properties
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } 
        public string Occation { get; set; }
    }
}
