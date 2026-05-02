using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
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
        public async Task<Location?> ForwardGeoCodeAsync(string address)
        {
            var url =
                $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={ApiKey}";


            var json = await _httpClient.GetStringAsync(url);
            var response = JsonSerializer.Deserialize<GoogleResponse>(json);
            var result = response?.results?.FirstOrDefault();
            if (result == null) return null;

            return new Location(

            result.geometry.location.lat,
            result.geometry.location.lng);
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
                City = place?.Locality,
                Street = place?.Thoroughfare,
                HouseNumber = place?.SubThoroughfare,
            };
        }
    }
}
