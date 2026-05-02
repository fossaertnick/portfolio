using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Models.enums;
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
        public OccationType Occation { get; set; }
        public string Description { get; set; }
        public Address MemoriaAddress { get; set; }
        public List<MediaItem> MediaMaterial { get; set; } = new();
    }
}
