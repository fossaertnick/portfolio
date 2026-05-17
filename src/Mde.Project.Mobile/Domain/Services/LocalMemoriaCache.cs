using Mde.Project.Mobile.Core.Data;
using Mde.Project.Mobile.Core.Entities;
using Mde.Project.Mobile.Core.Entities.Models;
using Mde.Project.Mobile.Core.Services.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.Project.Mobile.Domain.Services
{
    public class LocalMemoriaCache : ILocalMemoriaCache
    {
        private readonly IDbContextFactory<AppDbContext> _factory;

        // constructoren
        public LocalMemoriaCache(IDbContextFactory<AppDbContext> factory)
        {
            _factory = factory;
        }

        // methoden
        public async Task<ResultModel<bool>> DeleteAsync(Guid id)
        {
            try
            {
                using var context = await _factory.CreateDbContextAsync();
                var memoria = await context.Memorias.Include(m => m.MediaMaterial).FirstOrDefaultAsync(m => m.Id == id);
                if (memoria == null) return ResultModel<bool>.Failure("Memoria not found in local database.");

                foreach(var media in memoria.MediaMaterial)
                {
                    if(File.Exists(media.FilePath)) File.Delete(media.FilePath);
                }

                context.Memorias.Remove(memoria);
                await context.SaveChangesAsync();
                return ResultModel<bool>.Success(true);
            }
            catch (Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString());
            }
        }
        public async Task<ResultModel<Memoria>> GetByIdAsync(Guid id)
        {
            try
            {
                using var context = await _factory.CreateDbContextAsync();
                var memoria = await context.Memorias.Include(m => m.MemoriaAddress).Include(m => m.MediaMaterial).FirstOrDefaultAsync(m => m.Id == id);
                if (memoria == null) return ResultModel<Memoria>.Failure("Memoria was not found in local storage");

                return ResultModel<Memoria>.Success(memoria);
            }
            catch (Exception ex)
            {
                return ResultModel<Memoria>.Failure(ex.ToString());
            }
        }
        public async Task<ResultModel<bool>> SaveAsync(Memoria memoria)
        {
            try
            {
                using var context = await _factory.CreateDbContextAsync();
                context.Memorias.Add(memoria);
                await context.SaveChangesAsync();
                return ResultModel<bool>.Success(true);
            }
            catch(Exception ex)
            {
                return ResultModel<bool>.Failure(ex.ToString());
            }
        }
        public async Task<string> CacheFileAsync(byte[] bytes, string extension)
        {
            var fileName = $"{Guid.NewGuid()}{extension}";
            var localPath = Path.Combine(FileSystem.AppDataDirectory, fileName);
            await File.WriteAllBytesAsync(localPath, bytes);

            return localPath;
        }
    }
}
