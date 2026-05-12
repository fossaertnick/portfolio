using Mde.SourceOfTruth.Api.Dto.Address;
using Mde.SourceOfTruth.Api.Dto.MediaItem;
using Mde.SourceOfTruth.Core.Entities.enums;

namespace Mde.SourceOfTruth.Api.Dto.Memoria
{
    public class MemoriaDetailResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OccationType Occation { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedOn { get; set; }
        public DateTime LastEditedOn { get; set; }
        public AddressResponseDto Address { get; set; }
        public List<MediaItemResponseDto> MediaMaterial { get; set; } = new();
    }
}
