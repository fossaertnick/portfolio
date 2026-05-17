using Mde.SourceOfTruth.Core.Entities.enums;

namespace Mde.SourceOfTruth.Api.Dto.MediaItem
{
    public class MediaItemResponseDto
    {
        public MediaType MediaType { get; set; }
        public string FilePath { get; set; }
    }
}
