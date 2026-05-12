using Mde.SourceOfTruth.Api.Dto.Address;
using Mde.SourceOfTruth.Api.Dto.MediaItem;
using Mde.SourceOfTruth.Core.Entities.enums;
using Microsoft.AspNetCore.Antiforgery;
using System.ComponentModel.DataAnnotations;

namespace Mde.SourceOfTruth.Api.Dto.Memoria
{
    public class MemoriaRequestDto
    {
        [Required]
        [StringLength(35, MinimumLength = 2)]
        public required string Name { get; set; }

        public required OccationType Occation { get; set; }

        public string? Description { get; set; }

        [Required]
        public required AddressRequestDto Address { get; set; }

        public List<MediaItemRequestDto>? MediaMaterial { get; set; }
    }
}
