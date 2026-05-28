using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities.Models;
using System.Net.Http.Json;

namespace Mde.Project.Mobile.Domain.Services
{
    public class JwtService
    {
        private readonly HttpClient _httpClient;

        // constructor
        public JwtService(IHttpClientFactory httpClientFactory)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.MemoriaClientName);
        }

        // methoden
        public void EnsureDeviceId()
        {
            string deviceId = Preferences.Get("device_id", null);
            if (string.IsNullOrWhiteSpace(deviceId))
            {
                deviceId = Guid.NewGuid().ToString();
                Preferences.Set("device_id", deviceId);
            }
        }
        public async Task<ResultModel<string>> RequestJwtTokenAsync()
        {
            try
            {
                var deviceId = Preferences.Get("device_id", null);
                if (string.IsNullOrWhiteSpace(deviceId)) return ResultModel<string>.Failure("Geen device id gevonden.");

                var response = await _httpClient.PostAsJsonAsync($"{Constants.Authenticate}", deviceId);
                response.EnsureSuccessStatusCode();
                string token = await response.Content.ReadAsStringAsync();

                return ResultModel<string>.Success(token);
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure(ex.Message.ToString());
            }
        }
        public async Task EnsureTokenAsync()
        {
            var token = await SecureStorage.GetAsync("jwt");
            if (!string.IsNullOrWhiteSpace(token)) return;
            
            var newToken = await RequestJwtTokenAsync();
            await SecureStorage.SetAsync("jwt", newToken.Data);
        }
    }
}
