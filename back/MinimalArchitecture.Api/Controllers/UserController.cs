using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MinimalArchitecture.Application.Features.Autorization.AddUser;
using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;

namespace MinimalArchitecture.Api.Controllers
{
    public class UserController:QueryableController<User>
    {
        private readonly IMediator mediator;

        public UserController(AppDBContext ctx,
                              IMediator mediator):base(ctx)
        {
            this.mediator = mediator;
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<Result> AddUser(AddUserRequest request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
