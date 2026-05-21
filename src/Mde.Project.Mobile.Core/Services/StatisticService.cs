using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Core.Services.Interfaces;
using System.Net.Http.Headers;

namespace Mde.Project.Mobile.Core.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ISourceOfTruthService _sourceOfTruth;

        // constructor
        public StatisticService(ISourceOfTruthService sourceOfTruth)
        {
            _sourceOfTruth = sourceOfTruth;

        }
        // methoden
        public async Task<ResultModel<string>> GetTotalMemorias()
        {
            try
            {
                var result = await GetMemoriaAsync();
                if (!result.IsSucces) return CreateFailureFromResult<string>(result);

                string totalCount = result.Data.Count().ToString();
                return ResultModel<string>.Success(totalCount);
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure(ex.ToString(), "Something went wrong while picking up the total count.");
            }
        }
        public async Task<ResultModel<string>> GetMemoriasByOccasionAsync(OccationType type)
        {
            try
            {
                var result = await GetMemoriaAsync();
                if (!result.IsSucces) return CreateFailureFromResult<string>(result);

                string totalCount = result.Data.Count(m => m.Occation.Equals(type)).ToString();
                return ResultModel<string>.Success(totalCount);
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure(ex.ToString(), "Something went wrong while picking up occation count.");
            }
        }
        public async Task<ResultModel<string>> GetPhotoCountAsync()
        {
            try
            {
                var result = await GetMemoriaAsync();
                if (!result.IsSucces) return CreateFailureFromResult<string>(result);

                string totalCount = result.Data.Sum(m => m.MediaMaterial.Count(media => media.Type == MediaType.Photo)).ToString();
                return ResultModel<string>.Success(totalCount);
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure(ex.ToString(), "Something went wrong while picking up the total photo count.");
            }
        }
        public async Task<ResultModel<string>> GetVideoCountAsync()
        {
            try
            {
                var result = await GetMemoriaAsync();
                if (!result.IsSucces) return CreateFailureFromResult<string>(result);

                string totalCount = result.Data.Sum(m => m.MediaMaterial.Count(media => media.Type == MediaType.Video)).ToString();
                return ResultModel<string>.Success(totalCount);
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure(ex.ToString(), "Something went wrong while picking up the total video count.");
            }
        }
        public async Task<ResultModel<string>> GetFavoriteCountryAsync()
        {
            try
            {
                var result = await GetMemoriaAsync();
                if (!result.IsSucces) return CreateFailureFromResult<string>(result);

                string favoriteCountry = result.Data.GroupBy(m => m.MemoriaAddress.Country).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key ?? string.Empty;
                return ResultModel<string>.Success(favoriteCountry);
            }
            catch (Exception ex)
            {
                return ResultModel<string>.Failure(ex.ToString(), "Something went wrong while picking up the favorite country.");
            }
        }

        // aparte methoden
        private ResultModel<T> CreateFailureFromResult<T>(BaseResult result)
        {
            return ResultModel<T>.Failure(result.Errors.FirstOrDefault() ?? "Unknown error", result.UserMessage, result.StatusCode);
        }
        private async Task<ResultModel<IEnumerable<Memoria>>> GetMemoriaAsync()
        {
            try
            {
                var result = await _sourceOfTruth.GetAllMemoriasAsync();
                if(!result.IsSucces) return CreateFailureFromResult<IEnumerable<Memoria>>(result);

                return ResultModel<IEnumerable<Memoria>>.Success(result.Data);
            }
            catch(Exception ex)
            {
                return ResultModel<IEnumerable<Memoria>>.Failure(ex.ToString(), "Something went wrong while picking up the memorias");
            }
        }
    }
}
