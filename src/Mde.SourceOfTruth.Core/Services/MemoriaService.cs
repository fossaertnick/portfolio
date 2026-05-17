using Mde.SourceOfTruth.Core.Data;
using Mde.SourceOfTruth.Core.Entities;
using Mde.SourceOfTruth.Core.Services.Interfaces;
using Mde.SourceOfTruth.Core.Services.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Mde.SourceOfTruth.Core.Services
{
    public class MemoriaService : IMemoriaService
    {
        private readonly AppDbContext _SourceDbContext;

        // constructor
        public MemoriaService(AppDbContext sourceDbContext)
        {
            _SourceDbContext = sourceDbContext;
        }

        // methoden
        public async Task<ResultModel<Memoria>> GetMemoriaByIdAsync(Guid id)
        {
            if (id == Guid.Empty) return ResultModel<Memoria>.Failure("Id was empty.");
            try
            {
                var oneMemoriaById = await _SourceDbContext.Memorias.Include(m => m.MemoriaAddress).Include(m => m.MediaMaterial).SingleOrDefaultAsync(m => m.Id == id);
                if (oneMemoriaById is null)
                {
                    return ResultModel<Memoria>.Failure($"The memoria with id: {id} could not be found.");
                }
                return ResultModel<Memoria>.Success(oneMemoriaById);
            }
            catch (Exception ex)
            {
                return ResultModel<Memoria>.Failure($"Something went wrong while picking all the memorias: {ex.Message}.");
            }
        }
        public async Task<ResultModel<IEnumerable<Memoria>>> GetAllMemoriasAsync()
        {
            try
            {
                var allMemorias = await _SourceDbContext.Memorias.Include(m => m.MemoriaAddress).Include(m => m.MediaMaterial).ToListAsync();
                if (!allMemorias.Any())
                {
                    return ResultModel<IEnumerable<Memoria>>.Failure($"There were no memorias to be found.");
                }
                return ResultModel<IEnumerable<Memoria>>.Success(allMemorias);
            }
            catch(Exception ex)
            {
                return ResultModel<IEnumerable<Memoria>>.Failure($"Something went wrong while picking all the memorias: {ex.Message}.");
            }
        }
        public async Task<ResultModel<Memoria>> CreateMemoriaAsync(Memoria newMemoria)
        {
            if(newMemoria == null) return ResultModel<Memoria>.Failure($"Memoria parameter was null");
            try
            {
                bool exists = await _SourceDbContext.Memorias.Include(m => m.MemoriaAddress).Include(m => m.MediaMaterial).AnyAsync(m => m.Name == newMemoria.Name);
                if(exists) return ResultModel<Memoria>.Failure($"A memoria with this name '{newMemoria.Name}' already exists.");
                newMemoria.CreatedOn = DateTime.UtcNow;
                newMemoria.LastEditedOn = DateTime.UtcNow;

                _SourceDbContext.Memorias.Add(newMemoria);
                await _SourceDbContext.SaveChangesAsync();
                return ResultModel<Memoria>.Success(newMemoria);
            }
            catch (Exception ex)
            {
                return ResultModel<Memoria>.Failure($"Something went wrong while creating this new memoria: {ex.Message}");
            }
        }
        public async Task<ResultModel<Memoria>> UpdateMemoriaAsync(Memoria updatingMemoria)
        {
            if (updatingMemoria == null) return ResultModel<Memoria>.Failure($"Memoria parameter was null");
            try
            {
                bool exists = await _SourceDbContext.Memorias.AnyAsync(m => m.Name == updatingMemoria.Name && m.Id != updatingMemoria.Id);
                if (exists) return ResultModel<Memoria>.Failure($"A memoria with this name '{updatingMemoria.Name}' already exists.");

                var oldMemoria = await GetMemoriaByIdAsync(updatingMemoria.Id);
                if (!oldMemoria.IsSucces || oldMemoria.Data == null) return ResultModel<Memoria>.Failure($"{oldMemoria.Errors.FirstOrDefault()}");

                oldMemoria.Data.Name = updatingMemoria.Name;
                oldMemoria.Data.Description = updatingMemoria.Description;
                oldMemoria.Data.Occation = updatingMemoria.Occation;
                oldMemoria.Data.EventDate = updatingMemoria.EventDate;
                oldMemoria.Data.LastEditedOn = DateTime.UtcNow;

                oldMemoria.Data.MemoriaAddress.Country = updatingMemoria.MemoriaAddress.Country;
                oldMemoria.Data.MemoriaAddress.City = updatingMemoria.MemoriaAddress.City;
                oldMemoria.Data.MemoriaAddress.Street = updatingMemoria.MemoriaAddress.Street;
                oldMemoria.Data.MemoriaAddress.HouseNumber = updatingMemoria.MemoriaAddress.HouseNumber;
                oldMemoria.Data.MemoriaAddress.Latitude = updatingMemoria.MemoriaAddress.Latitude;
                oldMemoria.Data.MemoriaAddress.Longitude = updatingMemoria.MemoriaAddress.Longitude;

                var newMediaCollection = updatingMemoria.MediaMaterial ?? new List<MediaItem>();
                var removedMedia = oldMemoria.Data.MediaMaterial
                    .Where(oldMedia => !newMediaCollection.Any(newMedia => newMedia.FilePath == oldMedia.FilePath)).ToList();
                foreach(var media in removedMedia)
                {
                    var fileName = Path.GetFileName(media.FilePath);
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                    if(File.Exists(fullPath)) File.Delete(fullPath);
                }

                _SourceDbContext.MediaItems.RemoveRange(oldMemoria.Data.MediaMaterial);

                if(updatingMemoria.MediaMaterial != null)
                {
                    foreach(var media in updatingMemoria.MediaMaterial)
                    {
                        media.MemoriaId = oldMemoria.Data.Id;

                        oldMemoria.Data.MediaMaterial.Add(media);
                    }

                }

                await _SourceDbContext.SaveChangesAsync();
                return ResultModel<Memoria>.Success(oldMemoria.Data);
            }
            catch (Exception ex)
            {
                return ResultModel<Memoria>.Failure($"Something went wrong while updating this memoria: {ex.Message}");
            }
        }
        public async Task<ResultModel<Memoria>> DeleteMemoriaAsync(Guid id)
        {
            if(id == Guid.Empty) return ResultModel<Memoria>.Failure("Id parameter was leeg");
            try
            {
                var oneMemoriaById = await _SourceDbContext.Memorias.Include(m => m.MediaMaterial).SingleOrDefaultAsync(m => m.Id == id);
                if (oneMemoriaById is null) return ResultModel<Memoria>.Failure($"Something went wrong while picking up the Memoria with id: {id}");

                foreach (var media in oneMemoriaById.MediaMaterial)
                {
                    if(string.IsNullOrWhiteSpace(media.FilePath)) continue;
                    var fileName = Path.GetFileName(media.FilePath);
                    var fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", fileName);
                    if (File.Exists(fullPath)) File.Delete(fullPath);
                }

                _SourceDbContext.Remove(oneMemoriaById);
                await _SourceDbContext.SaveChangesAsync();
                return ResultModel<Memoria>.Success(oneMemoriaById);
            }
            catch(Exception ex)
            {
                return ResultModel<Memoria>.Failure($"Something went wrong while deleting the memoria: {ex}.");
            }
        }
    }
}
