using Mde.SourceOfTruth.Api.Dto.Auth;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Mde.SourceOfTruth.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _config;

        // constructor
        public AuthController(IConfiguration config)
        {
            _config = config;
        }

        // IActionResult
        [HttpPost("deviceId")]
        public IActionResult Authenticatie([FromBody] AuthRequestDto requestDto)
        {

            string token = GenerateToken(requestDto.DeviceId);
            var response = new AuthResponseDto
            {
                Token = token,
            };

            return Ok(response);
        }

        // eigen methoden
        private string GenerateToken(string deviceId)
        {
            var claims = new[]
            {
                new Claim("deviceId", deviceId)
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
