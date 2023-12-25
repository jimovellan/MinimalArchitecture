using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalArchitecture.Application.Features.Autorization.Login;
using MinimalArchitecture.Application.Features.Autorization.RefreshToken;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;
using System.Reflection.Metadata.Ecma335;

namespace MinimalArchitecture.Api.EndPoints;
public class AuthEndPoints : CarterModule
{

    public AuthEndPoints():base("api/auth")
    {
        this.RequireAuthorization();   
       
    }
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        
        var map = app.MapGroup("");
        
        map.MapPost("Login",async Task<Result<LoginReponse>>(LoginRequest request, IMediator mediator, ITokenService tokenService, CancellationToken cancellationToken) =>
        {
            

            var result = await mediator.Send(request, cancellationToken);

            return result;

        })
            .AllowAnonymous();
        
        map.MapGet("{identificador}",async Task<Result<String>>(string identificador) => Result.Ok(identificador) );

        map.MapPost("RefreshToken", 
            async Task<Result<RefreshTokenResponse>> (RefreshTokenRequest request, IMediator mediator, CancellationToken cancellation) => await mediator.Send(request))
            .AllowAnonymous();
            
    }

  
   

    
    

    
}
