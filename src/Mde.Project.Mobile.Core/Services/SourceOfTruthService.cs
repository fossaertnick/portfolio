using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Dtos.Memoria;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace Mde.Project.Mobile.Core.Services
{
    public class SourceOfTruthService : ISourceOfTruthService
    {
        private readonly HttpClient _httpClient;
        private readonly ILocalMemoriaCache _localCache;

        // constructor
        public SourceOfTruthService(IHttpClientFactory httpClientFactory, ILocalMemoriaCache localCache)
        {
            _httpClient = httpClientFactory.CreateClient(Constants.MemoriaClientName);
            _localCache = localCache;
        }

        // methoden
        public async Task<ResultModel<IEnumerable<MemoriaList>>> GetAllMemoriasAsync()
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }, PropertyNameCaseInsensitive = true
                };
                var memoriaDtos = await _httpClient.GetFromJsonAsync<List<MemoriaListResponseDto>>($"{Constants.GetAllMemorias}", options);

                if (memoriaDtos is null || !memoriaDtos.Any())
                {
                    return ResultModel<IEnumerable<MemoriaList>>.Failure("No memorias were found from the API.", "No memorias were found.");
                }

                var allMemoria = memoriaDtos.Select(m => new MemoriaList
                {
                    Id = m.Id,
                    Name = m.Name,
                    OccationType = m.OccationType,
                    EventDate = m.EventDate,
                    Country = m.Country,
                    Latitude = m.Latitude,
                    Longitude = m.Longitude,
                    TotalPhotos = m.TotalPhotos,
                    TotalVideos = m.TotalVideos,
                });
                return ResultModel<IEnumerable<MemoriaList>>.Success(allMemoria);
            }
            catch (Exception ex)
            {
                return ResultModel<IEnumerable<MemoriaList>>.Failure(ex.Message.ToString(), "Something went wrong while picking up the memorias.");
            }
        }
        public async Task<ResultModel<Memoria>> GetMemoriaByIdAsync(Guid id)
        {
            try
            {
                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) }, PropertyNameCaseInsensitive = true
                };
                var memoriaDto = await _httpClient.GetFromJsonAsync<MemoriaDetailResponseDto>($"{Constants.GetAllMemorias}/{id}", options);

                if (memoriaDto is null) return ResultModel<Memoria>.Failure("No memoria was found in the API by this Id.", "No memoria was found.");

                var oneMemoria = new Memoria
                {
                    Id = memoriaDto.Id,
                    Name = memoriaDto.Name,
                    Occation = memoriaDto.Occation,
                    Description = memoriaDto.Description,
                    EventDate = memoriaDto.EventDate,
                    CreatedOn = memoriaDto.CreatedOn,
                    LastEditedOn = memoriaDto.LastEditedOn,
                    MemoriaAddress = new Address
                    {
                        Country = memoriaDto.Address.Country,
                        City = memoriaDto.Address.City,
                        Street = memoriaDto.Address.Street,
                        HouseNumber = memoriaDto.Address.HouseNumber,
                        Latitude = memoriaDto.Address.Latitude,
                        Longitude = memoriaDto.Address.Longitude,
                    },
                    MediaMaterial = memoriaDto.MediaMaterial.Select(media => new MediaItem
                    {
                        Id = media.Id,
                        FilePath = media.FilePath,
                        Type = media.MediaType
                    }).ToList()
                };
                return ResultModel<Memoria>.Success(oneMemoria);
            }
            catch (Exception ex)
            {
                return ResultModel<Memoria>.Failure(ex.Message.ToString(), "Something went wrong while picking up the memoria.");
            }
        }
        public async Task<ResultModel<MemoriaDetailResponseDto>> CreateMemoriaAsync(MemoriaRequestDto newMemoria)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.PostAsJsonAsync($"{Constants.GetAllMemorias}", newMemoria);

                if (!response.IsSuccessStatusCode)
                {
                    return ResultModel<MemoriaDetailResponseDto>.Failure($"API returned {response.StatusCode}", "Problem with storing the memoria", (int)response.StatusCode);
                }

                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                    PropertyNameCaseInsensitive = true
                };
                MemoriaDetailResponseDto? createdMemoria = await response.Content.ReadFromJsonAsync<MemoriaDetailResponseDto>(options);
                if (createdMemoria == null)
                {
                    return ResultModel<MemoriaDetailResponseDto>.Failure("API returned null DTO", "No data received from the server");
                }
                return ResultModel<MemoriaDetailResponseDto>.Success(createdMemoria);
            }
            catch (Exception ex)
            {
                return ResultModel<MemoriaDetailResponseDto>.Failure(ex.Message.ToString(), "Something went wrong while saving the memoria");
            }
        }
        public async Task<ResultModel<MemoriaDetailResponseDto>> UpdateMemoriaAsync(MemoriaRequestDto updatingMemoria, Guid id)
        {
            ResultModel<MemoriaDetailResponseDto> result = new();
            try
            {
                HttpResponseMessage response = await _httpClient.PutAsJsonAsync($"{Constants.GetAllMemorias}/{id}", updatingMemoria);

                if (!response.IsSuccessStatusCode)
                {
                    return ResultModel<MemoriaDetailResponseDto>.Failure($"API returned {response.StatusCode}", "Problem with updating the memoria", (int)response.StatusCode);
                }

                var options = new JsonSerializerOptions
                {
                    Converters = { new JsonStringEnumConverter(JsonNamingPolicy.CamelCase) },
                    PropertyNameCaseInsensitive = true
                };
                MemoriaDetailResponseDto? updatedMemoria = await response.Content.ReadFromJsonAsync<MemoriaDetailResponseDto>(options);
                if (updatedMemoria == null)
                {
                    return ResultModel<MemoriaDetailResponseDto>.Failure("API returned null DTO", "No data received from the server");
                }
                return ResultModel<MemoriaDetailResponseDto>.Success(updatedMemoria);
            }
            catch (Exception ex)
            {
                return ResultModel<MemoriaDetailResponseDto>.Failure(ex.Message.ToString(), "Something went wrong while updating the memoria");
            }
        }
        public async Task<ResultModel<bool>> DeleteMemoriaAsync(Guid id)
        {
            try
            {
                HttpResponseMessage response = await _httpClient.DeleteAsync($"{Constants.GetAllMemorias}/{id}");

                if (!response.IsSuccessStatusCode)
                {
                    return ResultModel<bool>.Failure($"API returned {response.StatusCode}", "Problem with deleting the memoria", (int)response.StatusCode);
                }

                return ResultModel<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.Message.ToString(), "Something went wrong while updating the memoria");
            }
        }
        public async Task<string> UploadPhotoAsync(string localPath)
        {
            using var content = new MultipartFormDataContent();
            using var stream =  File.OpenRead(localPath);
            content.Add(new StreamContent(stream), "file", Path.GetFileName(localPath));
            var response = await _httpClient.PostAsync($"{Constants.GetAllMemorias}/upload", content);
            response.EnsureSuccessStatusCode();
            return (await response.Content.ReadAsStringAsync()).Trim('"');  
        }
        public async Task<ResultModel<MediaItem>> CacheRemoteMediaItemAsync(string remoteUrl, MediaType mediaType)
        {
            try
            {
                using var httpClient = new HttpClient();
                var bytes = await httpClient.GetByteArrayAsync(remoteUrl);
                var extension = Path.GetExtension(remoteUrl);
                var localPath = await _localCache.CacheFileAsync(bytes, extension);

                return ResultModel<MediaItem>.Success(new MediaItem
                {
                    Type = mediaType,
                    FilePath = localPath
                });
            }
            catch (Exception ex)
            {
                return ResultModel<MediaItem>.Failure(ex.ToString());
            }
        }
    }
}
