using Carter;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalArchitecture.Application.Features.Autorization.Login;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;

namespace MinimalArchitecture.Api.EndPoints;
public class AuthEndPoints : CarterModule
{
    
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        
        var map = app.MapGroup("api/Auth");
        map.MapPost("",Loggin);
        map.MapGet("{identificador}",Get);
        
    }

    [Authorize]
    [HttpGet()]
    public static async Task<IResult> Get(string identificador){
        return Results.Ok(identificador);
    }

    [ProducesResponseType(typeof(Result<string>), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status500InternalServerError)]
    public async static Task<Result<string>> Loggin(LoginRequest request, IMediator mediator, ITokenService tokenService, CancellationToken cancellationToken)
    {


        var user = new User() { Active = true, Email = "jimovllan@gmail.com", Name = "Nacho movellan", Roles = new List<Rol>() { new Rol() { Description = "Administrador", Id = 1, RolType = Entities.Authorization.Enums.RolType.Admin } } };
        

        var token = tokenService.GenerateToken(user);

       //var result = await mediator.Send(token, cancellationToken);

        return Result.Ok(token);
    }
    

    
}
