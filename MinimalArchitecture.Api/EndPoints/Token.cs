using Carter;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Results;

namespace MinimalArchitecture.Api.EndPoints
{
    public class Token:CarterModule
    {
        public override void AddRoutes(IEndpointRouteBuilder app)
        {
            var map = app.MapGroup("api/token");
            map.MapGet("Validate/{token}",ValidateToken);
        }

        /// <summary>
        /// Validate de integrity of tokken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public  static Result ValidateToken(ITokenService tokenService, string token)
        {
            return tokenService.ValidateToken(token);
        }
    }
}
