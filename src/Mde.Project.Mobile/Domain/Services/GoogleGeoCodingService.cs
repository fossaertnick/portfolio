using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System.Text.Json;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    public class GoogleGeoCodingService : IGeoCodingService
    {
        private readonly HttpClient _httpClient;

        // constructor
        public GoogleGeoCodingService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // FORWARD GEOCODING (finding coordiantes based on address)
        public async Task<ResultModel<bool>> ForwardGeoCodeAsync(Memoria saveMemoria)
        {
            try
            {
                if (saveMemoria == null) return ResultModel<bool>.Failure("Incoming memoria was null", "No valid memoria received.");

                var address =
                    $"{saveMemoria.MemoriaAddress.Street} {saveMemoria.MemoriaAddress.HouseNumber}, " +
                    $"{saveMemoria.MemoriaAddress.City}, {saveMemoria.MemoriaAddress.Country}";

                var url =
                    $"https://maps.googleapis.com/maps/api/geocode/json?address={Uri.EscapeDataString(address)}&key={Constants.GeoCodeaApiKey}";
                var json = await _httpClient.GetStringAsync(url);
                var response = JsonSerializer.Deserialize<GoogleResponse>(json);
                var result = response?.results?.FirstOrDefault();
                if (result == null) return ResultModel<bool>.Failure("No coordinates found for address", "No coordinates found for this address.");

                saveMemoria.MemoriaAddress.Latitude = result.geometry.location.lat;
                saveMemoria.MemoriaAddress.Longitude = result.geometry.location.lng;

                return ResultModel<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while picking up the coordinates.");
            }
        }

        // REVERSE GEOCODING (finding address based on coordinates)
        public async Task<ResultModel<Address>> ReverseGeoCodingAsync(Location coordinates)
        {
            try
            {
                if (coordinates == null) return ResultModel<Address>.Failure("Coordinates were null", "No valid coordinates received.");

                var placemarks = await Geocoding.Default.GetPlacemarksAsync(coordinates.Latitude, coordinates.Longitude);
                var place = placemarks?.FirstOrDefault();
                if (place == null) return ResultModel<Address>.Failure("No placemark found", "No address found for this location.");

                var address = new Address
                {
                    Country = place.CountryName,
                    City = place.Locality ?? place.SubAdminArea ?? place.AdminArea,
                    Street = place.Thoroughfare ?? place.FeatureName,
                    HouseNumber = place.SubThoroughfare ?? string.Empty,
                };

                return ResultModel<Address>.Success(address);
            }
            catch(Exception ex)
            {
                return ResultModel<Address>.Failure(ex.ToString(), "Something went wrong while picking up the address.");
            }
        }
    }
}
