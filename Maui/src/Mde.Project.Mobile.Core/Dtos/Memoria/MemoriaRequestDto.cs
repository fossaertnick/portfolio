using Mde.Project.Mobile.Core.Dtos.Addresses;
using Mde.Project.Mobile.Core.Dtos.MediaItem;
using Mde.Project.Mobile.Core.Entities.Enums;

namespace Mde.Project.Mobile.Core.Dtos.Memoria
{
    public class MemoriaRequestDto
    {
        // properties
        public string Name { get; set; }
        public OccationType Occation { get; set; }
        public string? Description { get; set; }
        public DateTime EventDate { get; set; }
        public AddressRequestDto Address { get; set; }
        public List<MediaItemRequestDto>? MediaMaterial { get; set; } = new();
    }
}
