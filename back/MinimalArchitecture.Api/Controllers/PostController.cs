using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalArchitecture.Application.Features.Posts.AddPost;
using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;

namespace MinimalArchitecture.Api.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class PostController:QueryableController<Post>
    {
        private readonly IMediator mediator;

        public PostController(IMediator mediator,
                              AppDBContext appDBContext):base(appDBContext)
        {
            this.mediator = mediator;
        }

        [HttpPost]
        public async Task<Result<Post>> AddPost(AddPostRequest request, CancellationToken cancellationToken)
        {
            return await mediator.Send(request, cancellationToken);
        }
    }
}
