using Mde.SourceOfTruth.Core.Entities.enums;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Mde.SourceOfTruth.Core.Entities
{
    public class Memoria
    {
        // properties
        public Guid Id { get; set; }
        public required string Name { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEditedOn { get; set; }
        public required OccationType Occation { get; set; }
        public string? Description { get; set; }

        // navigation properties
        public Address MemoriaAddress { get; set; } = null!;

        public ICollection<MediaItem> MediaMaterial { get; set; } = new List<MediaItem>();
    }
}
