using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IMediaService
    {
        // methoden
        ResultModel<MediaItem> PrepareMediaItem(FileResult fileResult);
        Task<ResultModel<bool>> DeleteMediaItemsCollectionAsync(IEnumerable<MediaItem> mediaItem);
        Task<ResultModel<bool>> DeleteMediaItemAsync(MediaItem mediaItem);
        Task<ResultModel<bool>> SyncMediaFiles(Memoria existingMemoria, Memoria updatedMemoria);
        Task<ResultModel<MediaItem>> SaveVideoAsync(FileResult video);
    }
}
