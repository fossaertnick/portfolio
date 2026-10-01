using Mde.Project.Mobile.Core.Dtos.Addresses;
using Mde.Project.Mobile.Core.Dtos.MediaItem;
using Mde.Project.Mobile.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Dtos.Memoria
{
    public class MemoriaDetailResponseDto
    {
        // properties
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OccationType Occation { get; set; }
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEditedOn { get; set; }
        public AddressResponseDTo Address { get; set; }
        public List<MediaItemResponseDto> MediaMaterial { get; set; } = new();
    }
}
