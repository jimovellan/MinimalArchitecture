using Azure.Core;
using Carter;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using MinimalArchitecture.Api.Controllers;
using MinimalArchitecture.Application.Features.Autorization.Login;
using MinimalArchitecture.Application.Features.Autorization.RefreshToken;
using MinimalArchitecture.Application.Features.Post.AddTag;
using MinimalArchitecture.Application.Features.Post.DelTag;
using MinimalArchitecture.Application.Features.Post.GetTag;
using MinimalArchitecture.Application.Features.Post.UpdTag;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;
using System.Reflection.Metadata.Ecma335;
using System.Threading;

namespace MinimalArchitecture.Api.EndPoints
{
    [ApiController]
    [Route("[controller]")]
    public class TagController: QueryableController<Tag>
    {

        private readonly IMediator mediator;


        public TagController(IMediator mediator, AppDBContext ctx):base(ctx)
        {
            this.mediator = mediator;
        }

        [HttpPut("{id}")]
        public async Task<Result<Entities.Posts.Models.Tag>> UpdateTagAsync(UpdTagRequest request,
                                                        int id,
                                                        CancellationToken cancelationToken) 
        {
            request.Id = id;
            return await mediator.Send(request, cancelationToken);
        }

        [HttpPost()]
        public async Task<Result<Entities.Posts.Models.Tag>> AddTagAsync(AddTagRequest request,
                                                    CancellationToken cancellationToken
                                                    ) =>
                                                    await mediator.Send(request, cancellationToken);

        [HttpDelete("{id}")]
        public async Task<Result> DeleteTagAsync(int id, IMediator mediator, CancellationToken cancelationToken)
        {
            var request = new DelTagRequest() { Id = id };
            return await mediator.Send(request, cancelationToken);
        }

        
    }
}
