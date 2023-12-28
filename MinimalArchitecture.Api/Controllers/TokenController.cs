using Carter;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Results;

namespace MinimalArchitecture.Api.EndPoints
{
    [ApiController]
    [Route("[controller]")]
    public class TokenController:ControllerBase
    {
        private readonly ITokenService tokenService;

        public TokenController(ITokenService tokenService)
        {
            this.tokenService = tokenService;
        }




        /// <summary>
        /// Validate de integrity of tokken
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpGet("validate/{token}")]
        public async Task<Result> ValidateToken(string token, CancellationToken cancellation)
        {
            return tokenService.ValidateToken(token);
        }
    }
}
