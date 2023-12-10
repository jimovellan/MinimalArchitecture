using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalArchitecture.Application.Features.Autorization.Login;
using MinimalArchitecture.Common.Results;

namespace MinimalArchitecture.Api.EndPoints;
public class AuthEndPoints : CarterModule
{
    public override void AddRoutes(IEndpointRouteBuilder app)
    {
        var map = app.MapGroup("api/login");
        map.MapPost("",Loggin);
        map.MapGet("{identificador}",Get);
    }

   
    [HttpGet()]
    public static async Task<IResult> Get(string identificador){
        return Results.Ok(identificador);
    }

    public async static Task<Result<string>> Loggin(LoginRequest request, IMediator mediator, CancellationToken cancellationToken)
    {

        var result = await mediator.Send(request, cancellationToken);

        return result;
    }
    
    
    
}
