namespace Mde.SourceOfTruth.Api.Dto.Address
{
    public class AddressResponseDto
    {
        public string Country { get; set; }
        public string City { get; set; }
        public string Street { get; set; }
        public string? HouseNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    }
}
