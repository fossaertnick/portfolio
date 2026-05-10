using Mde.Project.Mobile.Core.Entities;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IMediaService
    {
        // methoden
        Task<MediaItem> SavePhotoASync(FileResult photo);
        Task DeletePhotoAsync(Memoria memoriaSpecificToId);
        Task SyncMediaFiles(Memoria existingMemoria, Memoria updatedMemoria);
        Task<MediaItem> SaveVideoAsync(FileResult video);
    }
}
