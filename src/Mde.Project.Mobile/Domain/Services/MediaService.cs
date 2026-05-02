using Mde.Project.Mobile.Domain.Locations;
using Mde.Project.Mobile.Domain.Models;
using Mde.Project.Mobile.Domain.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services
{
    public class MediaService : IMediaService
    {
        // methoden
        public async Task<MediaItem> SavePhotoASync(FileResult photo)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(photo.FileName)}";
            var filePath = Path.Combine(FileSystem.AppDataDirectory, fileName);

            using var stream = await photo.OpenReadAsync();
            using var fileStream = File.Create(filePath);

            await stream.CopyToAsync(fileStream);

            return new MediaItem
            {
                Id = Guid.NewGuid(),
                Type = Models.enums.MediaType.Photo,
                FilePath = filePath,
                CreatedAt = DateTime.Now,
            };
        }
        public async Task DeletePhotoAsync(Memoria memoriaSpecificToId)
        {
            if (memoriaSpecificToId?.MediaMaterial == null) return;

            foreach (var media in memoriaSpecificToId.MediaMaterial)
            {
                try
                {
                    if (!string.IsNullOrWhiteSpace(media.FilePath) && File.Exists(media.FilePath))
                    {
                        File.Delete(media.FilePath);
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Fout bij verwijderen bestand: {ex.Message}");
                }
            }
        }

        public Task SyncMediaFiles(Memoria existingMemoria, Memoria updatedMemoria)
        {
            var existingFiles = existingMemoria.MediaMaterial?.Select(m => m.FilePath).ToList() ?? new List<string>();

            var newFiles = updatedMemoria.MediaMaterial?.Select(m => m.FilePath).ToList() ?? new List<string>();

            var filesToDelete = existingFiles.Where(oldFile => !newFiles.Contains(oldFile));

            foreach (var file in filesToDelete)
            {
                if (File.Exists(file))
                {
                    File.Delete(file);
                }
            }

            existingMemoria.MediaMaterial = updatedMemoria.MediaMaterial?.Select(m => new MediaItem
            {
                Id = m.Id,
                Type = m.Type,
                FilePath = m.FilePath,
                CreatedAt = m.CreatedAt,
                MemoriaId = existingMemoria.Id
            }).ToList();
            return Task.CompletedTask;
        }
    }
}
