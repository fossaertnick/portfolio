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
        public async Task<ResultModel<StatisticsModel>> GetStatisticsAsync()
        {
            try
            {
                var result = await _sourceOfTruth.GetAllMemoriasAsync();
                if (!result.IsSucces) return CreateFailureFromResult<StatisticsModel>(result);

                var memorias = result.Data.ToList();
                var statistics = new StatisticsModel
                {
                    TotalMemorias = memorias.Count().ToString(),
                    WerkMemoria = memorias.Count(m => m.Occation.Equals(OccationType.Work)).ToString(),
                    ReisMemoria = memorias.Count(m => m.Occation.Equals(OccationType.Travel)).ToString(),
                    UitgaanMemoria = memorias.Count(m => m.Occation.Equals(OccationType.Friends)).ToString(),
                    AndereMemoria = memorias.Count(m => m.Occation.Equals(OccationType.Other)).ToString(),
                    TotalMemoriaFotos = memorias.Sum(m => m.MediaMaterial.Count(media => media.Type == MediaType.Photo)).ToString(),
                    TotalMemoriaVideo = memorias.Sum(m => m.MediaMaterial.Count(media => media.Type == MediaType.Video)).ToString(),
                    FavoriteMemoriaCountry = memorias.GroupBy(m => m.MemoriaAddress.Country).OrderByDescending(g => g.Count()).FirstOrDefault()?.Key ?? string.Empty,
                };

                return ResultModel<StatisticsModel>.Success(statistics);
            }
            catch (Exception ex)
            {
                return ResultModel<StatisticsModel>.Failure(ex.ToString());
            }
        }

        // aparte methoden
        private ResultModel<T> CreateFailureFromResult<T>(BaseResult result)
        {
            return ResultModel<T>.Failure(result.Errors.FirstOrDefault() ?? "Unknown error", result.UserMessage, result.StatusCode);
        }
    }
}
