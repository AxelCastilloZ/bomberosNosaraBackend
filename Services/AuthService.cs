using System;
using System.Text;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.IdentityModel.Tokens;

using Microsoft.Extensions.Configuration;
using DTOs;
using Microsoft.IdentityModel.Tokens;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _config;

        public AuthService(IConfiguration config)
        {
            _config = config;
        }

        public Task<TokenResponse> LoginAsync(UserCredential credentials)
        {
            if (credentials.Email != "admin" || credentials.Password != "1234")
                throw new UnauthorizedAccessException("Credenciales inválidas");

            var claims = new[]
            {
                new Claim(ClaimTypes.Email, credentials.Email),
                new Claim(ClaimTypes.NameIdentifier, Guid.NewGuid().ToString()),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]!));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddHours(1),
                signingCredentials: creds
            );

            var jwt = new JwtSecurityTokenHandler().WriteToken(token);

            return Task.FromResult(new TokenResponse(jwt));
        }
    }
}
