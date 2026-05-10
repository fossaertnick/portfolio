using Mde.Project.Mobile.Core.Entities.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Mde.Project.Mobile.Core.Entities
{
    public class MediaItem : Synced
    {
        // properties
        public Guid Id { get; set; }
        public MediaType Type { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsPhoto => Type == MediaType.Photo;
        public bool IsVideo => Type == MediaType.Video;

        // navigation properties
        public Guid MemoriaId { get; set; }
        public Memoria Memoria { get; set; }
    }
}
