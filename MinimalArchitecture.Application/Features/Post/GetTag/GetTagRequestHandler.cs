using MediatR;
using MinimalArchitecture.Entities.Posts.Models;
using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Post.GetTag
{
    public class GetTagRequestHandler : IRequestHandler<GetTagRequest, IQueryable<Tag>>
    {
        private readonly IRepositoryBase<Tag> _repositoryTag;

        public GetTagRequestHandler(IRepositoryBase<Tag> repositoryTag)
        {
            _repositoryTag = repositoryTag;
        }
        public async Task<IQueryable<Tag>> Handle(GetTagRequest request, CancellationToken cancellationToken)
        {
            return _repositoryTag.GetQuery();
        }
    }
}
