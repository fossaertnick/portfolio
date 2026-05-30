using Mde.Project.Mobile.Core.Dtos.Addresses;
using Mde.Project.Mobile.Core.Dtos.MediaItem;
using Mde.Project.Mobile.Core.Dtos.Memoria;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Core.Services.Interfaces;

namespace Mde.Project.Mobile.Core.Services
{
    public class MemoriaService : IMemoriaService
    {
        private readonly ISourceOfTruthService _sourceOfTruth;
        private readonly ILocalMemoriaCache _localRepo;

        // constructor
        public MemoriaService(ISourceOfTruthService sourceOfTruth, ILocalMemoriaCache localRepo)
        {
            _sourceOfTruth = sourceOfTruth;
            _localRepo = localRepo;
        }

        // methoden
        public async Task<ResultModel<bool>> DeleteMemoriaAsync(Guid memoriaId)
        {
            try
            {
                var result = await _sourceOfTruth.DeleteMemoriaAsync(memoriaId);
                if (!result.IsSucces) return CreateFailureFromResult<bool>(result);

                var localDelete = await _localRepo.DeleteAsync(memoriaId);
                if (!localDelete.IsSucces) Console.WriteLine(localDelete.Errors.FirstOrDefault());

                return ResultModel<bool>.Success(true, "Memoria succefully deleted.");
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Er liep iets mis bij het verwijderen van de Memoria.");
            }
        }
        public Task<ResultModel<IEnumerable<MemoriaList>>> GetAllMemoriaAsync()
        {
            return GetMemoriaAsync();
        }
        public async Task<ResultModel<IEnumerable<MemoriaList>>> GetMemoriaByFilterAsync(string searchTerm)
        {
            try
            {
                var result = await GetMemoriaAsync();
                if (!result.IsSucces) return CreateFailureFromResult<IEnumerable<MemoriaList>>(result);

                var filteredMemorias = result.Data.Where(m => m.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase));
                return ResultModel<IEnumerable<MemoriaList>>.Success(filteredMemorias);
            }
            catch (Exception ex)
            {
                return ResultModel<IEnumerable<MemoriaList>>.Failure(ex.ToString(), "Something went wrong while filtering the memorias.");
            }
        }
        public async Task<ResultModel<Memoria>> GetMemoriaByIdAsync(Guid id)
        {
            try
            {
                if (id == Guid.Empty) return ResultModel<Memoria>.Failure("Guid was empty", "No valid memoria-id was received");
                var localMemoria = await _localRepo.GetByIdAsync(id);
                if (localMemoria.IsSucces) return ResultModel<Memoria>.Success(localMemoria.Data);

                var remoteMemoria = await _sourceOfTruth.GetMemoriaByIdAsync(id);
                if (!remoteMemoria.IsSucces) return CreateFailureFromResult<Memoria>(remoteMemoria);

                foreach(var media in remoteMemoria.Data.MediaMaterial)
                {
                    if(Uri.IsWellFormedUriString(media.FilePath, UriKind.Absolute))
                    {
                        var cachedResult = await _sourceOfTruth.CacheRemoteMediaItemAsync(media.FilePath, media.Type);
                        if(cachedResult.IsSucces) media.FilePath = cachedResult.Data.FilePath;
                    }
                }

                var saveResult = await _localRepo.SaveAsync(remoteMemoria.Data);
                if (!saveResult.IsSucces) Console.WriteLine(saveResult.Errors.FirstOrDefault());

                return ResultModel<Memoria>.Success(remoteMemoria.Data);
            }
            catch (Exception ex)
            {
                return ResultModel<Memoria>.Failure(ex.ToString(), "Something went wrong while picking up the memoria.");
            }
        }
        public async Task<ResultModel<bool>> SaveMemoriaAsync(Memoria saveThisMemoria)
        {
            try
            {
                if (saveThisMemoria == null) return ResultModel<bool>.Failure("Incoming memoria was null", "No valid memoria received.");

                foreach (var media in saveThisMemoria.MediaMaterial)
                {
                    if (File.Exists(media.FilePath))
                    {
                        var remoteUrl = await _sourceOfTruth.UploadPhotoAsync(media.FilePath);
                        media.FilePath = remoteUrl;
                    }
                }

                MemoriaRequestDto request = TransformIntoDto(saveThisMemoria);
                if (saveThisMemoria.Id == Guid.Empty)
                {
                    var createdResult = await _sourceOfTruth.CreateMemoriaAsync(request);
                    if (!createdResult.IsSucces) return CreateFailureFromResult<bool>(createdResult);

                    return ResultModel<bool>.Success(true, "Memoria was succesfully created.");
                }
                else
                {
                    var updatedResult = await _sourceOfTruth.UpdateMemoriaAsync(request, saveThisMemoria.Id);
                    if (!updatedResult.IsSucces) return CreateFailureFromResult<bool>(updatedResult);

                    var localDelete = await _localRepo.DeleteAsync(saveThisMemoria.Id);
                    if (!localDelete.IsSucces) Console.WriteLine(localDelete.Errors.FirstOrDefault());

                    return ResultModel<bool>.Success(true, "Memoria was succesfully updated.");
                }
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while saving the update.");
            }
        }

        // ondersteunende methoden
        private ResultModel<T> CreateFailureFromResult<T>(BaseResult result)
        {
            return ResultModel<T>.Failure(result.Errors.FirstOrDefault() ?? "Unknown error", result.UserMessage, result.StatusCode);
        }
        private async Task<ResultModel<IEnumerable<MemoriaList>>> GetMemoriaAsync()
        {
            try
            {
                var result = await _sourceOfTruth.GetAllMemoriasAsync();
                if (!result.IsSucces) return CreateFailureFromResult<IEnumerable<MemoriaList>>(result);

                return ResultModel<IEnumerable<MemoriaList>>.Success(result.Data);
            }
            catch (Exception ex)
            {
                return ResultModel<IEnumerable<MemoriaList>>.Failure(ex.ToString(), "Something went wrong while picking up the memorias");
            }
        }
        private MemoriaRequestDto TransformIntoDto(Memoria createOrUpdateMemoria)
        {
            MemoriaRequestDto request = new MemoriaRequestDto
            {
                Name = createOrUpdateMemoria.Name,
                Occation = createOrUpdateMemoria.Occation,
                Description = createOrUpdateMemoria.Description,
                EventDate = createOrUpdateMemoria.EventDate,
                Address = new AddressRequestDto
                {
                    Country = createOrUpdateMemoria.MemoriaAddress.Country,
                    City = createOrUpdateMemoria.MemoriaAddress.City,
                    Street = createOrUpdateMemoria.MemoriaAddress.Street,
                    HouseNumber = createOrUpdateMemoria.MemoriaAddress.HouseNumber,
                    Latitude = createOrUpdateMemoria.MemoriaAddress.Latitude,
                    Longitude = createOrUpdateMemoria.MemoriaAddress.Longitude
                },
                MediaMaterial = createOrUpdateMemoria.MediaMaterial?.Select(media => new MediaItemRequestDto
                {
                    FilePath = media.FilePath,
                    Type = media.Type
                }).ToList() ?? new List<MediaItemRequestDto>()
            };

            return request;
        }
    }
}
