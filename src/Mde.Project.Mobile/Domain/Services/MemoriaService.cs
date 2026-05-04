using Mde.Project.Mobile.Domain.Models.enums;
using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile.Domain.Locations.Mock
{
    public class MemoriaService : IMemoriaService
    {
        private readonly IGeoCodingService _googleGeoCodingService;
        private readonly IMediaService _mediaService;
        private readonly ISeedingService _seedingService;

        // constructor
        public MemoriaService(ISeedingService seedingService, IGeoCodingService googleGeoCodingService, IMediaService mediaService)
        {
            _seedingService = seedingService;
            _googleGeoCodingService = googleGeoCodingService;
            _mediaService = mediaService;
        }

        // methodes
        public Task<Memoria> GetMemoriaByIdAsync(Guid id)
        {
            return Task.FromResult(_seedingService.Locations.SingleOrDefault(l => l.Id == id));
        } // I want only ONE
        public Task<IEnumerable<Memoria>> GetAllMemoriaAsync()
        {
            return Task.FromResult(_seedingService.Locations.AsEnumerable());
        } // I want them ALL
        public Task<IEnumerable<Memoria>> GetMemoriaByFilterAsync(string searchTerm)
        {
            return Task.FromResult(_seedingService.Locations.Where(l => l.Name.Contains(searchTerm, StringComparison.OrdinalIgnoreCase)));
        } // You get what you type
        private async Task UpdateMemoriaAsync(Memoria updateMemoria)
        {
            var memoriaToBeUpdated = await GetMemoriaByIdAsync(updateMemoria.Id);

            if (memoriaToBeUpdated == null) throw new ArgumentException("Niet gevonden");
            else 
            {
                memoriaToBeUpdated.Name = updateMemoria.Name;
                memoriaToBeUpdated.Latitude = updateMemoria.Latitude;
                memoriaToBeUpdated.Longitude = updateMemoria.Longitude;
                memoriaToBeUpdated.Description = updateMemoria.Description;
                memoriaToBeUpdated.Occation = updateMemoria.Occation;

                memoriaToBeUpdated.MemoriaAddress.Country = updateMemoria.MemoriaAddress.Country;
                memoriaToBeUpdated.MemoriaAddress.City = updateMemoria.MemoriaAddress.City;
                memoriaToBeUpdated.MemoriaAddress.Street = updateMemoria.MemoriaAddress.Street;
                memoriaToBeUpdated.MemoriaAddress.HouseNumber = updateMemoria.MemoriaAddress.HouseNumber;

                await _mediaService.SyncMediaFiles(memoriaToBeUpdated, updateMemoria);
            }
        } // Update
        public async Task DeleteMemoriaAsync(Guid memoriaId)
        {
            var memoriaSpecificToId = await GetMemoriaByIdAsync(memoriaId);
            if (memoriaSpecificToId != null)
            {
                await _mediaService.DeletePhotoAsync(memoriaSpecificToId);
                _seedingService.Locations.Remove(memoriaSpecificToId);
            }
            else
            {
                throw new ArgumentException("Er ging iets fout bij het verwijderen");
            }
        } // Delete
        public async Task SaveChangesAsync(Memoria saveMemoria)
        {
            if(saveMemoria == null) throw new ArgumentNullException(nameof(saveMemoria));

            var address =
                    $"{saveMemoria.MemoriaAddress.Street} {saveMemoria.MemoriaAddress.HouseNumber}, " +
                    $"{saveMemoria.MemoriaAddress.City}, {saveMemoria.MemoriaAddress.Country}";

            var location = await _googleGeoCodingService.ForwardGeoCodeAsync(address);

            if(location != null)
            {
                saveMemoria.Latitude = location.Latitude;
                saveMemoria.Longitude = location.Longitude;
            }

            if(saveMemoria.Id == Guid.Empty)
            {
                saveMemoria.Id = Guid.NewGuid();
                if(saveMemoria.MediaMaterial != null)
                {
                    foreach(var media in saveMemoria.MediaMaterial)
                    {
                        media.MemoriaId = saveMemoria.Id;
                    }
                }
                _seedingService.Locations.Add(saveMemoria);
            }
            else
            {
                await UpdateMemoriaAsync(saveMemoria);
            }
        } // Will we Create Or Update
    }
}
