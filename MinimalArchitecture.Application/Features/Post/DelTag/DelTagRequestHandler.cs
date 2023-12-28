using MediatR;
using Microsoft.Extensions.Logging;
using MinimalArchitecture.Application.Features.Post.UpdTag;
using MinimalArchitecture.Common.Errors;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;
using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Post.DelTag
{
    public class DelTagRequestHandler : IRequestHandler<DelTagRequest, Result>
    {
        private readonly ILogger<DelTagRequestHandler> _logger;
        private readonly IRepositoryBase<Tag> _repositoryTag;
        private readonly IUnitOfWork _unitOfWork;

        public DelTagRequestHandler(ILogger<DelTagRequestHandler> logger,
                                    IRepositoryBase<Tag> repositoryTag,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repositoryTag = repositoryTag;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result> Handle(DelTagRequest request, CancellationToken cancellationToken)
        {
            var tags = await _repositoryTag.GetAsync(w => w.Id == request.Id);

            if (tags.NoHasElement()) return Result.Fail<Tag>(TagErrors.TAG_NO_FOUND);

            var tag = tags.FirstOrDefault();

            _repositoryTag.Delete(tag);

            await _unitOfWork.Save();

            return Result.Ok(); 

        }
    }
}
