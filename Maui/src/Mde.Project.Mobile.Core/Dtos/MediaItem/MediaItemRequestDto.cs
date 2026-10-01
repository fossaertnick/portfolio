using Mde.Project.Mobile.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Mde.Project.Mobile.Core.Dtos.MediaItem
{
    public class MediaItemRequestDto
    {
        // properties
        public required MediaType Type { get; set; }
        public string FilePath { get; set; }
    }
}
