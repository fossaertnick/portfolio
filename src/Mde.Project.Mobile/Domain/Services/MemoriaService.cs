using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    public class MemoriaService : IMemoriaService
    {
        private readonly IMediaService _mediaService;
        private readonly IGeoCodingService _geoCodingService;
        private readonly IDbContextFactory<AppDbContext> _dbContext;

        // constructor
        public MemoriaService(IMediaService mediaService, IDbContextFactory<AppDbContext> dbContext, IGeoCodingService geoCodingService)
        {
            _mediaService = mediaService;
            _dbContext = dbContext;
            _geoCodingService = geoCodingService;
        }

        // methodes
        public async Task<Memoria> GetMemoriaByIdAsync(Guid id)
        {
            if (id == Guid.Empty) throw new ArgumentException("Id mag niet leeg zijn");
            try
            {
                using var context = await _dbContext.CreateDbContextAsync();
                return await context.Memorias
                    .Include(m => m.MemoriaAddress)
                    .Include(m => m.MediaMaterial)
                    .SingleOrDefaultAsync(l => l.Id.Equals(id));
            }
            catch (Exception ex)
            {
                throw new Exception("Er liep iets mis bij het ophalen van de Memoria.", ex);
            }
        } // I want only ONE
        public async Task<IEnumerable<Memoria>> GetMemoriaByFilterAsync(string searchTerm)
        {
            try
            {
                using var context = await _dbContext.CreateDbContextAsync();
                return await context.Memorias
                    .Where(l => l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                    .Include(m => m.MemoriaAddress)
                    .Include(m => m.MediaMaterial).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Er liep iets mis bij het ophalen van de locaties.", ex);
            }
        } // You get what you type
        public async Task<IEnumerable<Memoria>> GetAllMemoriaAsync()
        {
            try
            {
                using var context = await _dbContext.CreateDbContextAsync();
                return await context.Memorias.Include(m => m.MemoriaAddress).Include(m => m.MediaMaterial).ToListAsync();
            }
            catch(Exception ex)
            {
                throw new Exception("Er liep iets mis bij het ophalen van de locaties.", ex);
            }
        } // I want them ALL
        private async Task UpdateMemoriaAsync(Memoria updateMemoria)
        {
            using var context = await _dbContext.CreateDbContextAsync();

            var existing = await context.Memorias
                    .Include(m => m.MemoriaAddress)
                    .Include(m => m.MediaMaterial)
                    .SingleOrDefaultAsync(l => l.Id.Equals(updateMemoria.Id));
            if (existing == null) throw new Exception("Memoria niet gevonden");

            var oldMedia = existing.MediaMaterial.ToList();

            existing.Name = updateMemoria.Name;
            existing.Description = updateMemoria.Description;
            existing.Occation = updateMemoria.Occation;
            existing.LastEditedOn = DateTime.Now;

            existing.MemoriaAddress.Country = updateMemoria.MemoriaAddress.Country;
            existing.MemoriaAddress.City = updateMemoria.MemoriaAddress.City;
            existing.MemoriaAddress.Street = updateMemoria.MemoriaAddress.Street;
            existing.MemoriaAddress.HouseNumber = updateMemoria.MemoriaAddress.HouseNumber;
            existing.MemoriaAddress.Latitude = updateMemoria.MemoriaAddress.Latitude;
            existing.MemoriaAddress.Longitude = updateMemoria.MemoriaAddress.Longitude;

            context.MediaItems.RemoveRange(oldMedia);
            foreach(var media in updateMemoria.MediaMaterial)
            {
                context.MediaItems.Add(new MediaItem
                {
                    Type = media.Type,
                    FilePath = media.FilePath,
                    MemoriaId = existing.Id,
                });
            }
            await context.SaveChangesAsync();
        } // Update
        private async Task CreateMemoriaAsync(Memoria createMemoria)
        {
            if(createMemoria == null) throw new ArgumentException("Memoria mag niet null zijn");
            try
            {
                using var context = await _dbContext.CreateDbContextAsync();
                createMemoria.CreatedOn = DateTime.Now;
                createMemoria.LastEditedOn = DateTime.Now;
                context.Memorias.Add(createMemoria);
                await context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                throw new Exception("Er liep iets mis bij het aanmaken van de Memoria.", ex);
            }
        }
        public async Task DeleteMemoriaAsync(Guid memoriaId)
        {

            try
            {
               using var context = await _dbContext.CreateDbContextAsync();
                var memoriaSpecificToId = await context.Memorias
                        .Include(m => m.MemoriaAddress)
                        .Include(m => m.MediaMaterial)
                        .SingleOrDefaultAsync(l => l.Id.Equals(memoriaId));

                await _mediaService.DeletePhotoAsync(memoriaSpecificToId);
                context.Memorias.Remove(memoriaSpecificToId);
                await context.SaveChangesAsync();
            }
            catch
            {
                throw new Exception("Er liep iets mis bij het verwijderen van de Memoria.");
            }
        } // Delete
        public async Task SaveMemoriaAsync(Memoria saveThisMemoria)
        {
            if (saveThisMemoria == null) throw new ArgumentNullException("Problemen met de binnenkomende data.");
            if (saveThisMemoria.MemoriaAddress == null) throw new ArgumentNullException("Problemen met de binnenkomende data. Er is geen adres meegegeven.");

            await _geoCodingService.ForwardGeoCodeAsync(saveThisMemoria);

            if (saveThisMemoria.Id == Guid.Empty)
            {
                await CreateMemoriaAsync(saveThisMemoria);
            }
            else
            {
                await UpdateMemoriaAsync(saveThisMemoria);
            }
        }
    }
}
