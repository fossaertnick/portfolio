using Mde.Project.Mobile.Domain.Models.enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Models
{
    public class MediaItem
    {
        // properties
        public Guid Id { get; set; }
        public Guid? MemoriaId { get; set; }

        public MediaType Type { get; set; }

        public string FilePath { get; set; }
        public ImageSource? ImageSource { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
