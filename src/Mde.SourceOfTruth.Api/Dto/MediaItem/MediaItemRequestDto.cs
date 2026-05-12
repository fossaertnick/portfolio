using Mde.SourceOfTruth.Core.Entities.enums;
using System.ComponentModel.DataAnnotations;

namespace Mde.SourceOfTruth.Api.Dto.MediaItem
{
    public class MediaItemRequestDto
    {
        public required MediaType Type { get; set; }

        [Required]
        public required string FilePath { get; set; }
    }
}
