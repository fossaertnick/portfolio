using Mde.SourceOfTruth.Core.Entities.enums;

namespace Mde.SourceOfTruth.Core.Entities
{
    public class MediaItem
    {
        // properties
        public Guid Id { get; set; }
        public MediaType Type { get; set; }
        public string FilePath { get; set; }
        public DateTime CreatedAt { get; set; }

        // navigation properties
        public Guid MemoriaId { get; set; }
        public Memoria Memoria { get; set; } = null!;
    }
}
