using MediatR;
using Microsoft.Extensions.Logging;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;
using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Posts.GetPost
{
    public class GetPostRequestHandler : IRequestHandler<GetPostRequest, Result<Post>>
    {
        public GetPostRequestHandler(IUserInfoService userInfo,
                                     IRepositoryBase<Post> postRepository,
                                     ILogger<GetPostRequestHandler> logger)
        {
            
        }
        public Task<Result<Post>> Handle(GetPostRequest request, CancellationToken cancellationToken)
        {
            return null;   
        }
    }
}
