using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;

namespace Mde.Project.Mobile.Domain.Services
{
    public class MediaService : IMediaService
    {
        // methoden
        public ResultModel<MediaItem> PrepareMediaItem(FileResult fileResult)
        {
            try
            {
                if (fileResult == null) return ResultModel<MediaItem>.Failure("photo input was null");

                return ResultModel<MediaItem>.Success(new MediaItem
                {
                    Type = MediaType.Photo,
                    FilePath = fileResult.FullPath
                }); 
            }
            catch(Exception ex)
            {
                return ResultModel<MediaItem>.Failure(ex.ToString());
            }
        }
        public async Task<ResultModel<bool>> DeleteMediaItemsCollectionAsync(IEnumerable<MediaItem> mediaItems)
        {
            try
            {
                foreach(var media in mediaItems)
                {
                    if (string.IsNullOrWhiteSpace(media.FilePath)) continue;

                    if(File.Exists(media.FilePath)) File.Delete(media.FilePath);
                }
                    
                return ResultModel<bool>.Success(true, "Media succefully deleted.");
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while deleting the requested media.");
            }
        }
    }
}
