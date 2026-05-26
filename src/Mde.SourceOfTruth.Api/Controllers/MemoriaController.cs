using Mde.SourceOfTruth.Api.Dto.Address;
using Mde.SourceOfTruth.Api.Dto.MediaItem;
using Mde.SourceOfTruth.Api.Dto.Memoria;
using Mde.SourceOfTruth.Core.Entities;
using Mde.SourceOfTruth.Core.Entities.enums;
using Mde.SourceOfTruth.Core.Services.Interfaces;
using Mde.SourceOfTruth.Core.Services.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace Mde.SourceOfTruth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class MemoriaController : ControllerBase
    {
        private readonly IMemoriaService _memoriaService;

        // constructor
        public MemoriaController(IMemoriaService memoriaService)
        {
            _memoriaService = memoriaService;
        }

        // ActionResult (GET)
        [HttpGet]
        public async Task<ActionResult<MemoriaListReponseDto>> GetAllMemorias()
        {
            ResultModel<IEnumerable<Memoria>> result = await _memoriaService.GetAllMemoriasAsync();
            if(!result.IsSucces) return BadRequest(result.Errors);

            IEnumerable<MemoriaListReponseDto> memoriaResponses = result.Data.Select(m => new MemoriaListReponseDto
            {
                Id = m.Id,
                Name = m.Name,
                OccationType = m.Occation,
                EventDate = m.EventDate,
                Latitude = m.MemoriaAddress.Latitude,
                Longitude = m.MemoriaAddress.Longitude,
                Country = m.MemoriaAddress.Country,
                TotalPhotos = m.MediaMaterial.Count(ph => ph.Type.Equals(MediaType.Photo)),
                TotalVideos = m.MediaMaterial.Count(ph => ph.Type.Equals(MediaType.Video)),
            });
            return Ok(memoriaResponses);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<MemoriaDetailResponseDto>> GetMemoriaById(Guid id)
        {
            ResultModel<Memoria> result = await _memoriaService.GetMemoriaByIdAsync(id);
            if(!result.IsSucces || result.Data == null) return NotFound(result.Errors);

            MemoriaDetailResponseDto memoriaResponse = new MemoriaDetailResponseDto
            {
                Id = result.Data.Id,
                Name = result.Data.Name,
                Occation = result.Data.Occation,
                Description= result.Data.Description,
                EventDate = result.Data.EventDate,
                CreatedOn = result.Data.CreatedOn,
                LastEditedOn= result.Data.LastEditedOn,
                Address = new AddressResponseDto
                {
                    Country = result.Data.MemoriaAddress.Country,
                    City = result.Data.MemoriaAddress.City,
                    Street = result.Data.MemoriaAddress.Street,
                    HouseNumber = result.Data.MemoriaAddress.HouseNumber,
                    Latitude = result.Data.MemoriaAddress.Latitude,
                    Longitude = result.Data.MemoriaAddress.Longitude,
                },
                MediaMaterial = result.Data.MediaMaterial.Select(media => new MediaItemResponseDto
                {
                    FilePath = media.FilePath,
                    MediaType = media.Type,
                }).ToList()
            };
            return Ok(memoriaResponse);
        }

        // ActionResult (POST)
        [HttpPost]
        public async Task<ActionResult<MemoriaDetailResponseDto>> CreateMemoria(MemoriaRequestDto memoriaRequest)
        {
            Memoria newMemoria = MapToMakeEntity(memoriaRequest);

            ResultModel<Memoria> createdResult = await _memoriaService.CreateMemoriaAsync(newMemoria);
            if (!createdResult.IsSucces || createdResult.Data == null) return BadRequest(createdResult.Errors);

            MemoriaDetailResponseDto memoriaResponseDto = MapToShowReponse(createdResult.Data);

            return CreatedAtAction(nameof(GetMemoriaById), new { id = memoriaResponseDto.Id}, memoriaResponseDto);
        }
        [HttpPost("upload")]
        public async Task<ActionResult<string>> UploadMediaItems(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid()}{Path.GetExtension(file.FileName)}";
            var folder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img");
            Directory.CreateDirectory(folder);
            var fullPath = Path.Combine(folder, fileName);
            using var stream = System.IO.File.Create(fullPath);
            await file.CopyToAsync(stream);
            var url = $"https://06dfrpsm-44338.brs.devtunnels.ms/img/{fileName}";

            return Ok(url);
        }

        // ActionResult (PUT)
        [HttpPut("{id}")]
        public async Task<ActionResult<MemoriaDetailResponseDto>> UpdateMemoria(Guid id, MemoriaRequestDto memoriaRequestDto)
        {
            Memoria updatingMemoria = MapToMakeEntity(memoriaRequestDto);
            updatingMemoria.Id = id;

            ResultModel<Memoria> updatedResult = await _memoriaService.UpdateMemoriaAsync(updatingMemoria);
            if(!updatedResult.IsSucces || updatedResult.Data == null) return BadRequest(updatedResult.Errors);

            MemoriaDetailResponseDto memoriaResponseDto = MapToShowReponse(updatedResult.Data);

            return Ok(memoriaResponseDto);
        }

        // ActionResult (DELETE)
        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteMemoria(Guid id)
        {
            ResultModel<Memoria> deletedResult = await _memoriaService.DeleteMemoriaAsync(id);
            if (!deletedResult.IsSucces) return NotFound(deletedResult.Errors);

            return NoContent();
        }

        // aparte methoden
        private MemoriaDetailResponseDto MapToShowReponse(Memoria result)
        {
            MemoriaDetailResponseDto memoriaResponseDto = new MemoriaDetailResponseDto
            {
                Id = result.Id,
                Name = result.Name,
                Occation = result.Occation,
                Description = result.Description,
                EventDate = result.EventDate,
                CreatedOn = result.CreatedOn,
                LastEditedOn = result.LastEditedOn,
                Address = new AddressResponseDto
                {
                    Country = result.MemoriaAddress.Country,
                    City = result.MemoriaAddress.City,
                    Street = result.MemoriaAddress.Street,
                    HouseNumber = result.MemoriaAddress.HouseNumber,
                    Latitude = result.MemoriaAddress.Latitude,
                    Longitude = result.MemoriaAddress.Longitude,
                },
                MediaMaterial = result.MediaMaterial.Select(media => new MediaItemResponseDto
                {
                    FilePath = media.FilePath,
                    MediaType = media.Type,
                }).ToList()
            };
            return memoriaResponseDto;
        }
        private Memoria MapToMakeEntity(MemoriaRequestDto memoriaRequest)
        {
            Memoria newMemoria = new Memoria
            {
                Name = memoriaRequest.Name,
                Occation = memoriaRequest.Occation,
                Description = memoriaRequest.Description,
                EventDate = memoriaRequest.EventDate,
                MemoriaAddress = new Address
                {
                    Country = memoriaRequest.Address.Country,
                    City = memoriaRequest.Address.City,
                    Street = memoriaRequest.Address.Street,
                    HouseNumber = memoriaRequest.Address.HouseNumber,
                    Latitude = memoriaRequest.Address.Latitude,
                    Longitude = memoriaRequest.Address.Longitude,
                },
            };
            if (memoriaRequest.MediaMaterial != null)
            {
                newMemoria.MediaMaterial = memoriaRequest.MediaMaterial
                    .Where(media => !string.IsNullOrWhiteSpace(media.FilePath))
                    .Select(media => new MediaItem
                {
                    FilePath = media.FilePath,
                    Type = media.Type,
                    CreatedAt = DateTime.UtcNow
                }).ToList();
            }
            return newMemoria;
        }
    }
}

