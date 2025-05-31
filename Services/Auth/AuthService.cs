using System;
using System.Security.Claims;
using System.Threading.Tasks;
using DTOs;
using Microsoft.Extensions.Configuration;
using Services.Auth;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly JwtTokenGenerator _tokenGenerator;

        public AuthService(IConfiguration config)
        {
            _tokenGenerator = new JwtTokenGenerator(config);
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

            var token = _tokenGenerator.GenerateToken(claims);
            return Task.FromResult(new TokenResponse(token));
        }
    }
}
