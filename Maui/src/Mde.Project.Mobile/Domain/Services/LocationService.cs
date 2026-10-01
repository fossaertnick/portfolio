using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System.Globalization;
using System.Text.Json;

namespace Mde.Project.Mobile.Domain.Services
{
    public class LocationService : ILocationService
    {
        private readonly HttpClient _httpClient;

        // constructor
        public LocationService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // methoden
        public async Task<ResultModel<bool>> EnsureLocationPermission()
        {
            try
            {
                var status = await Permissions.CheckStatusAsync<Permissions.LocationWhenInUse>();
                if (status != PermissionStatus.Granted) status = await Permissions.RequestAsync<Permissions.LocationWhenInUse>();

                if (status != PermissionStatus.Granted) return ResultModel<bool>.Failure($"Location permission status: {status}", "Location access was not allowed");

                return ResultModel<bool>.Success(true, "Location access was allowed");
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong with the requesting of the location access");
            }
        }
        public async Task<ResultModel<Location>> GetCurrentLocationAsync()
        {
            try
            {
                var permissionResult = await EnsureLocationPermission();
                if (!permissionResult.IsSucces) return ResultModel<Location>.Failure(permissionResult.Errors.FirstOrDefault() ?? "Permission denied", permissionResult.UserMessage, permissionResult.StatusCode);
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(5));
                var location = await Geolocation.Default.GetLocationAsync(request);
                if (location == null) return ResultModel<Location>.Failure("Location result was null", "Your location could not be found.");

                return ResultModel<Location>.Success(location, "Location succesfully found.");
            }
            catch(PermissionException ex)
            {
                return ResultModel<Location>.Failure(ex.ToString(), "No access to location.");
            }
            catch(Exception ex)
            {
                return ResultModel<Location>.Failure(ex.ToString(), "Something went wrong while picking up the location.");
            }
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
            catch (Exception ex)
            {
                return ResultModel<Address>.Failure(ex.ToString(), "Something went wrong while picking up the address.");
            }
        }
    }
}
