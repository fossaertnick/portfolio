using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System.Globalization;
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


                var lat = coordinates.Latitude.ToString(CultureInfo.InvariantCulture);
                var lng = coordinates.Longitude.ToString(CultureInfo.InvariantCulture);
                var url = $"https://maps.googleapis.com/maps/api/geocode/json?latlng={lat},{lng}&key={Constants.GeoCodeaApiKey}";
                var json = await _httpClient.GetStringAsync(url);
                var response = JsonSerializer.Deserialize<GoogleResponse>(json);
                var result = response?.results?.FirstOrDefault();
                if (result == null) return ResultModel<Address>.Failure("No address found.", "");

                var address = new Address
                {
                    Country = result.address_components?
                        .FirstOrDefault(x => x.types.Contains("country"))?.long_name,

                    City =
                        result.address_components?
                            .FirstOrDefault(x => x.types.Contains("locality"))?.long_name
                        ?? result.address_components?
                            .FirstOrDefault(x => x.types.Contains("postal_town"))?.long_name
                        ?? result.address_components?
                            .FirstOrDefault(x => x.types.Contains("administrative_area_level_2"))?.long_name
                        ?? result.address_components?
                            .FirstOrDefault(x => x.types.Contains("administrative_area_level_1"))?.long_name,

                    Street = result.address_components?
                        .FirstOrDefault(x => x.types.Contains("route"))?.long_name,

                    HouseNumber = result.address_components?
                        .FirstOrDefault(x => x.types.Contains("street_number"))?.long_name,
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
