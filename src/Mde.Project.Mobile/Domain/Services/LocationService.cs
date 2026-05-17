using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile.Domain.Services
{
    public class LocationService : ILocationService
    {
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
                
                var request = new GeolocationRequest(GeolocationAccuracy.Medium, TimeSpan.FromSeconds(10));
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
    }
}
