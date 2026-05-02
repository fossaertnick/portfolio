using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services.Interfaces
{
    public interface IMediaService
    {
        // methoden
        Task<MediaItem> SavePhotoASync(FileResult photo);
        Task DeletePhotoAsync(Memoria memoriaSpecificToId);
        Task SyncMediaFiles(Memoria existingMemoria, Memoria updatedMemoria);
    }
}
