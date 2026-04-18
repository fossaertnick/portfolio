using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Locations
{
    public class Memoria : Location
    {
        // properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; } 
        public DateTime LastEditedOn { get; set; }
        public string Occation { get; set; }
        public string Description { get; set; }
    }
}
