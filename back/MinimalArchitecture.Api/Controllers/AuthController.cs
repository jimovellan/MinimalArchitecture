using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalArchitecture.Application.Features.Autorization.Login;
using MinimalArchitecture.Application.Features.Autorization.RefreshToken;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Results;

namespace MinimalArchitecture.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AuthController:ControllerBase
    {
        private readonly IMediator mediator;


        public AuthController(IMediator mediator)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost("Login")]
        public async Task<Result<LoginReponse>> Login(LoginRequest request,  CancellationToken cancellationToken)
        {

            var result = await mediator.Send(request, cancellationToken);

            return result;

        }

        [AllowAnonymous]
        [HttpPost("RefreshToken")]
        public async Task<Result<RefreshTokenResponse>> RefreshToken(RefreshTokenRequest request, CancellationToken cancellation) => await mediator.Send(request,cancellation);

    }
}
