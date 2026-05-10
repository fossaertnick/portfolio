using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Domain.Dtos;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System.Text.Json;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    public class GoogleGeoCodingService : IGeoCodingService
    {
        private readonly HttpClient _httpClient;
        private const string ApiKey = "AIzaSyD6BQDVKAtnEm6N3LFjEm2s3XY_nDujX8U"; // hardgecodeerd om het voorbeeld werkend te maken, in een echte app zou ik dit in een veilige opslag plaatsen

        // constructor
        public GoogleGeoCodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // methoden

        // FORWARD GEOCODING (finding coordiantes based on address)
        public async Task ForwardGeoCodeAsync(Memoria saveMemoria)
        {
            var address =
                $"{saveMemoria.MemoriaAddress.Street} {saveMemoria.MemoriaAddress.HouseNumber}, " +
                $"{saveMemoria.MemoriaAddress.City}, {saveMemoria.MemoriaAddress.Country}";

            var url =
                $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={ApiKey}";


            var json = await _httpClient.GetStringAsync(url);
            var response = JsonSerializer.Deserialize<GoogleResponse>(json);
            var result = response?.results?.FirstOrDefault();
            if (result != null)
            {
                saveMemoria.MemoriaAddress.Latitude = result.geometry.location.lat;
                saveMemoria.MemoriaAddress.Longitude = result.geometry.location.lng;
            }
            else
            {
                throw new Exception("Geen coördinaten gevonden voor het opgegeven adres.");
            }
        }

        // REVERSE GEOCODING (finding address based on coordinates)
        public async Task<Address?> ReverseGeoCodingAsync(Location coordinates)
        {
            if (coordinates == null) return null;

            var placemarks = await Geocoding.Default.GetPlacemarksAsync(coordinates.Latitude, coordinates.Longitude);
            var place = placemarks?.FirstOrDefault();

            if (place == null) return null;

            return new Address
            {
                Country = place?.CountryName,
                City = place?.Locality ?? place?.SubAdminArea ?? place?.AdminArea,
                Street = place?.Thoroughfare ?? place?.FeatureName,
            };
        }
    }
}
