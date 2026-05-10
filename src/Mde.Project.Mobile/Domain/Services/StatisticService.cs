using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mde.Project.Mobile.Domain.Services
{
    public class StatisticService : IStatisticService
    {
        private readonly IDbContextFactory<AppDbContext> _dbContext;

        // constructor
        public StatisticService(IDbContextFactory<AppDbContext> dbContext)
        {
            _dbContext = dbContext;
        }

        // methoden
        public async Task<int> GetTotalMemorias()
        {
            using var context = await _dbContext.CreateDbContextAsync();
            var locations = await context.Memorias.ToListAsync();
            return locations.Count();
        }
        public async Task<int> GetMemoriasByOccasionAsync(OccationType type)
        {
            using var context = await _dbContext.CreateDbContextAsync();
            var locations = await context.Memorias.ToListAsync();
            return locations.Count(m => m.Occation.Equals(type));
        }
        public async Task<int> GetPhotoCountAsync()
        {
            using var context = await _dbContext.CreateDbContextAsync();
            var locations = await context.Memorias.Include(m => m.MediaMaterial).ToListAsync();
            if (!locations.Any()) return default;
            return locations.Sum(m => m.MediaMaterial.Count(media => media.Type == MediaType.Photo));
        }
        public async Task<int> GetVideoCountAsync()
        {
            using var context = await _dbContext.CreateDbContextAsync();
            var locations = await context.Memorias.Include(m => m.MediaMaterial).ToListAsync();
            return locations.Sum(m => m.MediaMaterial.Count(media => media.Type == MediaType.Video));
        }
        public async Task<string> GetFavoriteCountryAsync()
        {
            using var context = await _dbContext.CreateDbContextAsync();
            var locations = await context.Memorias.Include(m => m.MemoriaAddress).ToListAsync();

            return locations.GroupBy(m => m.MemoriaAddress.Country)
                .OrderByDescending(g => g.Count())
                .FirstOrDefault()?.Key; 
        }
    }
}
