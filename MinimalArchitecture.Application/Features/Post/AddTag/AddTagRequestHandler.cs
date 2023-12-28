using MediatR;
using Microsoft.Extensions.Logging;
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
    public class AddTagRequestHandler : IRequestHandler<AddTagRequest, Result<Tag>>
    {
        private readonly ILogger<UpdTagRequestHandler> _logger;
        private readonly IRepositoryBase<Tag> _repositoryTag;
        private readonly IUnitOfWork _unitOfWork;

        public AddTagRequestHandler(ILogger<UpdTagRequestHandler> logger,
                                    IRepositoryBase<Tag> repositoryTag,
                                    IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _repositoryTag = repositoryTag;
            _unitOfWork = unitOfWork;
        }

        public async Task<Result<Tag>> Handle(AddTagRequest request, CancellationToken cancellationToken)
        {
            var tags = await _repositoryTag.GetAsync(w => w.Name.ToLower() == request.Name.ToLower());

            if (tags.HasElements()) return Result.Fail<Tag>(TagErrors.TAG_DUPLICATE);

            var tag = new Tag() { Name = request.Name };

            _repositoryTag.Insert(tag);

            await _unitOfWork.Save();

            return Result.Ok(tag);

        }
    }
}
