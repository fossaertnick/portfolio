using Mde.SourceOfTruth.Api.Dto.Auth;
using Mde.SourceOfTruth.Core.Data;
using Mde.SourceOfTruth.Core.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.EntityFrameworkCore;

namespace Mde.SourceOfTruth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly AppDbContext _context;
        private readonly IConfiguration _config;

        // constructor
        public AuthController(IConfiguration config, AppDbContext context)
        {
            _config = config;
            _context = context;
        }

        // IActionResult
        [HttpPost("deviceId")]
        public async Task<IActionResult> Authenticatie([FromBody] AuthRequestDto requestDto)
        {
            var device = await _context.Devices.FirstOrDefaultAsync(d => d.DeviceIdentifier == requestDto.DeviceIdentifier);
            if(device == null)
            {
                device = new Device
                {
                    Id = Guid.NewGuid(),
                    DeviceIdentifier = requestDto.DeviceIdentifier,
                    DeviceName = requestDto.DeviceName,
                    RegisteredOn = DateTime.UtcNow
                };
                _context.Devices.Add(device);
                await _context.SaveChangesAsync();
            }

            string token = GenerateToken(device.Id);
            var response = new AuthResponseDto
            {
                Token = token,
            };

            return Ok(response);
        }

        // eigen methoden
        private string GenerateToken(Guid deviceId)
        {
            var claims = new[]
            {
                new Claim("deviceId", deviceId.ToString())
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims = claims,
                expires: DateTime.UtcNow.AddDays(30),
                signingCredentials: creds
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
