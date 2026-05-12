namespace Mde.SourceOfTruth.Core.Entities
{
    public class Address
    {
        public Guid Id { get; set; }
        public required string City { get; set; }
        public required string Country { get; set; }
        public required string Street { get; set; }
        public string? HouseNumber { get; set; }
        public required double Latitude { get; set; }
        public required double Longitude { get; set; }

        // navigation properties
        public Guid MemoriaId { get; set; }
        public Memoria Memoria { get; set; } = null!;
    }
}
