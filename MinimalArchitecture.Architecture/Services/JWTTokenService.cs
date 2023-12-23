using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Architecture.Config;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Common.Models;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace MinimalArchitecture.Architecture.Services
{
    public class JWTTokenService : ITokenService
    {
        private readonly JWTSettings _jwtSettings;
        private readonly ILogger<JWTTokenService> _logger;

        public JWTTokenService(IOptions<JWTSettings> jwtSettings,
                               ILogger<JWTTokenService> logger)
        {
            jwtSettings.Value.ThrowExceptionIfNull(nameof(jwtSettings));

            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }
        public string GenerateToken(User user)
        {
            user.ThrowExceptionIfNull(nameof(user));

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Sid, user.Id.ToString()),
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypes.Email, user.Email)
            };

            foreach (var rol in user.Roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, rol.RolType.ToString()));
            }

            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Secret));

            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);
            var tokenDescriptor = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                audience: _jwtSettings.Audience,
                claims: claims,
                expires: DateTime.Now.AddMinutes(_jwtSettings.MinToExpire),
                signingCredentials: credentials);

            return new JwtSecurityTokenHandler().WriteToken(tokenDescriptor);

            

        }

        public Result ValidateToken(string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();

                var key = System.Text.Encoding.UTF8.GetBytes(_jwtSettings.Secret);

                var tokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _jwtSettings.Issuer,
                    ValidateAudience = true,
                    ValidAudience = _jwtSettings.Audience,
                    ValidateIssuerSigningKey = true,
                    RequireExpirationTime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };

                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken validatedToken);

                return Result.Ok();
            }
            catch (SecurityTokenExpiredException ex)
            {
                _logger.LogError(ex, "JWTTokenService - ValidateToken - EXPIRED");
                return Result.Fail(Common.Errors.Token.ExpiredToken);
            }
            catch(SecurityTokenException ex)
            {
                _logger.LogError(ex, "JWTTokenService - ValidateToken - ERROR");
                return Result.Fail(Common.Errors.Token.NotValidToken);
            }
           



    }
    }
}
