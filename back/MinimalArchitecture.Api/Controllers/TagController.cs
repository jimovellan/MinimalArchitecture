using MediatR;
using Microsoft.AspNetCore.Mvc;
using MinimalArchitecture.Api.Controllers;
using MinimalArchitecture.Application.Features.Tags.AddTag;
using MinimalArchitecture.Application.Features.Tags.DelTag;
using MinimalArchitecture.Application.Features.Tags.UpdTag;
using MinimalArchitecture.Architecture.Repository;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;

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
        public async Task<Result<Tag>> UpdateTagAsync(UpdTagRequest request,
                                                        int id,
                                                        CancellationToken cancelationToken) 
        {
            request.Id = id;
            return await mediator.Send(request, cancelationToken);
        }

        [HttpPost()]
        public async Task<Result<Tag>> AddTagAsync(AddTagRequest request,
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
