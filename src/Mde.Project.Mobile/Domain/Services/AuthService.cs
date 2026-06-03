using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Dtos.Auth;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Core.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services
{
    public class AuthService : IAuthService
    {
        private readonly HttpClient _httpClient;

        // constructor
        public AuthService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.MemoriaClientName);
        }

        // methoden
        public async Task<ResultModel<string>> AuthenticateDeviceAsync()
        {
            try
            {
                var request = new AuthRequestDto
                {
                    DeviceIdentifier = await GetDeviceIdentifierAsync(),
                    DeviceName = DeviceInfo.Current.Name
                };

                var response = await _httpClient.PostAsJsonAsync(Constants.Authenticate, request);
                if (!response.IsSuccessStatusCode) return ResultModel<string>.Failure($"authentication failed", "Could not authenticate device.");

                var result = await response.Content.ReadFromJsonAsync<AuthResponseDto>();
                if(result == null) return ResultModel<string>.Failure($"No token received", "Could not authenticate device.");

                await SecureStorage.SetAsync(Constants.JwtTokenKey, result.Token);

                return ResultModel<string>.Success(result.Token);
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure($"Exception during authentication: {ex.Message}", "An error occurred while authenticating the device.");
            }
        }
        public async Task<string?> GetTokenAsync()
        {
            return await SecureStorage.Default.GetAsync(Constants.JwtTokenKey);
        }

        // ondersteunende methoden
        private async Task<string> GetDeviceIdentifierAsync()
        {
            var deviceIdentifier = await SecureStorage.GetAsync(Constants.PersonalizedDevice);
            if(string.IsNullOrWhiteSpace(deviceIdentifier))
            {
                deviceIdentifier = Guid.NewGuid().ToString();
                await SecureStorage.SetAsync(Constants.PersonalizedDevice, deviceIdentifier);
            }

            return deviceIdentifier;
        }
    }
}
