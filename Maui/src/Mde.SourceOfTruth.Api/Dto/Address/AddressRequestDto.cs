using System.ComponentModel.DataAnnotations;

namespace Mde.SourceOfTruth.Api.Dto.Address
{
    public class AddressRequestDto
    {
        [Required]
        public required string Country { get; set; }

        [Required]
        public required string City { get; set; }

        [Required]
        public required string Street { get; set; }

        public string? HouseNumber { get; set; }

        public required double Latitude { get; set; }

        public required double Longitude { get; set; }
    }
}
