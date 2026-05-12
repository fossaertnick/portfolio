using Mde.Project.Mobile.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mde.Project.Mobile.Core.Entities
{
    public class Memoria
    {
        // properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEditedOn { get; set; }
        public OccationType Occation { get; set; }
        public string? Description { get; set; }

        // navigation properties
        public Guid AddressId { get; set; }
        public Address MemoriaAddress { get; set; }

        public List<MediaItem?> MediaMaterial { get; set; }
    }
}
