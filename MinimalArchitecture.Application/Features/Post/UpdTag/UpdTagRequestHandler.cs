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

namespace MinimalArchitecture.Application.Features.Post.AddTag
{
    public class UpdTagRequestHandler : IRequestHandler<UpdTagRequest, Result<Tag>>
    {
        private readonly ILogger<UpdTagRequestHandler> _logger;
        private readonly IRepositoryBase<Tag> _repositoryTag;
        private readonly IUnitOfWork _unitOfWork;

        public UpdTagRequestHandler(ILogger<UpdTagRequestHandler> logger,
                                    IRepositoryBase<Tag> repositoryTag,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repositoryTag = repositoryTag;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Tag>> Handle(UpdTagRequest request, CancellationToken cancellationToken)
        {
            
            var tagsfinded = await _repositoryTag.GetAsync(w => w.Id == request.Id);

            if (tagsfinded.NoHasElement()) return Result.Fail<Tag>(TagErrors.TAG_NO_FOUND);

            var tag = tagsfinded.FirstOrDefault();

            if (tag is null) return Result.Fail<Tag>(TagErrors.TAG_NO_FOUND);

            var tagsDuplicated = await _repositoryTag.GetAsync(w => w.Name.ToLower() == request.Name.ToLower() & tag!.Id != w.Id);

            if (tagsDuplicated.HasElements()) return Result.Fail<Tag>(TagErrors.TAG_DUPLICATE);

            tag.Name = request.Name;

            _repositoryTag.Update(tag);

            await _unitOfWork.Save();

            return Result.Ok(tag);

        }
    }
}
