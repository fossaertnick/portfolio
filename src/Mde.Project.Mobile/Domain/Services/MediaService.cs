using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Core.Services.Interfaces;
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
        public async Task<ResultModel<MediaItem>> SaveVideoAsync(FileResult video)
        {
            try
            {
                if (video == null) return ResultModel<MediaItem>.Failure("Video input was null", "No valid video received.");

                var fileName = $"{Guid.NewGuid()}{Path.GetExtension(video.FileName)}";
                var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

                using var stream = await video.OpenReadAsync();
                using var fileStream = File.Create(filePath);

                await stream.CopyToAsync(fileStream);

                var mediaItem = new MediaItem
                {
                    Type = MediaType.Video,
                    FilePath = filePath,
                };

                return ResultModel<MediaItem>.Success(mediaItem, "Video succesfully saved.");
            }
            catch (Exception ex)
            {
                return ResultModel<MediaItem>.Failure(ex.ToString(), "Something went wrong while saving the video.");
            }
        }
        public async Task<ResultModel<bool>> DeleteMediaItemsCollectionAsync(IEnumerable<MediaItem> mediaItems)
        {
            try
            {
                if (mediaItems == null || !mediaItems.Any()) return ResultModel<bool>.Failure("Mediaitems was empty", "No mediaitems found.");

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
        public async Task<ResultModel<bool>> DeleteMediaItemAsync(MediaItem mediaItem)
        {
            try
            {
                if (mediaItem == null) return ResultModel<bool>.Failure("MediaItem was null", "No mediaItem was received.");

                if (string.IsNullOrWhiteSpace(mediaItem.FilePath)) return ResultModel<bool>.Failure("Filepath was empty", "No valid mediapath found.");

                if(File.Exists(mediaItem.FilePath)) File.Delete(mediaItem.FilePath);

                return ResultModel<bool>.Success(true, "MediaItem was succesfully deleted.");
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while deleting the media item.");
            }
        }
        public async Task<ResultModel<bool>> SyncMediaFiles(Memoria existingMemoria, Memoria updatedMemoria)
        {
            try
            {
                var existingFiles = existingMemoria.MediaMaterial?.Select(m => m.FilePath).ToList() ?? new List<string>();
                var newFiles = updatedMemoria.MediaMaterial?.Select(m => m.FilePath).ToList() ?? new List<string>();
                var filesToDelete = existingFiles.Where(oldFile => !newFiles.Contains(oldFile));
                foreach (var file in filesToDelete)
                {
                    if (File.Exists(file)) File.Delete(file);
                }
                existingMemoria.MediaMaterial = updatedMemoria.MediaMaterial?.Select(m => new MediaItem
                {
                    Id = m.Id,
                    Type = m.Type,
                    FilePath = m.FilePath,
                    MemoriaId = existingMemoria.Id
                }).ToList();

                return ResultModel<bool>.Success(true, "Media was succesfully synced.");
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString(), "Something went wrong while sychronising the mediafiles.");
            }
        }
    }
}
