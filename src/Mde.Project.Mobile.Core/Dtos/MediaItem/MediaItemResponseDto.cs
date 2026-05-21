using Mde.Project.Mobile.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Dtos.MediaItem
{
    public class MediaItemResponseDto
    {
        // properties
        public Guid Id { get; set; }
        public MediaType MediaType { get; set; }
        public string FilePath { get; set; }
    }
}
