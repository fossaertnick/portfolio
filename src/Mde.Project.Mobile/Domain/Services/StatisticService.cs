using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Models.enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly ISeedingService _seedingService;

        // constructor
        public StatisticService(ISeedingService seedingService)
        {
            _seedingService = seedingService;
        }

        // methoden
        public int GetTotalMemorias()
        {
            var locations = _seedingService.Locations ?? Enumerable.Empty<Memoria>();
            return locations.Count();
        }
        public int GetMemoriasByOccasionAsync(OccationType type)
        {
            var locations = _seedingService.Locations ?? Enumerable.Empty<Memoria>();
            return locations.Count(m => m.Occation.Equals(type));
        }
        public int GetPhotoCountAsync()
        {
            var locations = _seedingService.Locations ?? Enumerable.Empty<Memoria>();
            if (!locations.Any()) return default;
            return locations.Sum(m => m.MediaMaterial.Count(media => media.Type == Models.enums.MediaType.Photo));
        }
        public int GetVideoCountAsync()
        {
            var locations = _seedingService.Locations ?? Enumerable.Empty<Memoria>();
            return locations.Sum(m => m.MediaMaterial.Count(media => media.Type == Models.enums.MediaType.Video));
        }
        public string GetFavoriteCountryAsync()
        {
            var locations = _seedingService.Locations ?? Enumerable.Empty<Memoria>();
            return locations.GroupBy(m => m.MemoriaAddress.Country)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key; 
        }
    }
}
