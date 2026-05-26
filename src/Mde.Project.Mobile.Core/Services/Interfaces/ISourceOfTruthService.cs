using Mde.Project.Mobile.Core.Dtos.Memoria;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Enums;
using Mde.Project.Mobile.Core.Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Core.Services.Interfaces
{
    public interface ISourceOfTruthService
    {
        // methoden
        Task<ResultModel<Memoria>> GetMemoriaByIdAsync(Guid id);
        Task<ResultModel<IEnumerable<MemoriaList>>> GetAllMemoriasAsync();
        Task<ResultModel<MemoriaDetailResponseDto>> CreateMemoriaAsync(MemoriaRequestDto newMemoria);
        Task<string> UploadPhotoAsync(string localPath);
        Task<ResultModel<MemoriaDetailResponseDto>> UpdateMemoriaAsync(MemoriaRequestDto updatingMemoria, Guid id);
        Task<ResultModel<bool>> DeleteMemoriaAsync(Guid id);
        Task<ResultModel<MediaItem>> CacheRemoteMediaItemAsync(string filePath, MediaType mediaType);
    }
}
