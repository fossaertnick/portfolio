using Mde.SourceOfTruth.Core.Entities.enums;

namespace Mde.SourceOfTruth.Api.Dto.Memoria
{
    public class MemoriaListResponseDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public OccationType Occation { get; set; }
        public string? Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public int MediaCount { get; set; }
        public DateTime LastEditedOn { get; set; }
    }
}
