using MediatR;
using MinimalArchitecture.Application.Services;
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

namespace MinimalArchitecture.Application.Features.Posts.AddPost
{
    public class AddPostRequestHandler : IRequestHandler<AddPostRequest, Result<Post>>
    {
        private readonly IRepositoryBase<Post> postRepository;
        private readonly IRepositoryBase<Tag> tagRepository;
        private readonly IRepositoryBase<Category> categoryRepository;
        private readonly IUserInfoService userInfoService;
        private readonly IUnitOfWork unitOfWork;

        public AddPostRequestHandler(IRepositoryBase<Post> postRepository,
                                     IRepositoryBase<Tag> tagRepository,
                                     IRepositoryBase<Category> categoryRepository,
                                     IUserInfoService userInfoService,
                                     IUnitOfWork unitOfWork)
        {
            this.postRepository = postRepository;
            this.tagRepository = tagRepository;
            this.categoryRepository = categoryRepository;
            this.userInfoService = userInfoService;
            this.unitOfWork = unitOfWork;
        }

        private AddPostRequest _request;

        public async Task<Result<Post>> Handle(AddPostRequest request, CancellationToken cancellationToken)
        {
            _request = request;

            var result = await CreatePostBaseWithRequestAsync(cancellationToken)
                                .ThenAsync(FillPostWithTasksAsync)
                                .ThenAsync(FindAndFillWithCategoryAsync);

            if (result.IsFailed) return result;

            await unitOfWork.Save();

            return result;

        }

        /// <summary>
        /// create the new post element with the minimal data
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Post>> CreatePostBaseWithRequestAsync(CancellationToken cancellationToken )
        {
            var post = new Post()
            {
                Title = _request.Title,
                Description = _request.Description,
                Html = _request.Html,
                Owner = userInfoService.GetUser().Id,
            };

            return post;
        }

        /// <summary>
        /// Fill the Enumerable with the Tags
        /// </summary>
        /// <param name="post"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<Post>> FillPostWithTasksAsync(Post post, CancellationToken cancellationToken)
        {
            if (_request.Tags.HasElements())
            {
                var existingTags = await tagRepository.GetAsync(w => _request.Tags.Contains(w.Name));

                var addedTags = _request.Tags.Where(w => !existingTags.Select(s=>s.Name).Any(a => a.ToLower().Equals(w.ToLower())))
                                             .Select(s=> new Tag() { Name = s });

                //tagRepository.Insert(addedTags);

                post.Tags = existingTags.Aggregate(addedTags, (acum, t) => acum.Append(t)).ToList();
            }

            return post;
        }
        /// <summary>
        /// search and fill with de category
        /// </summary>
        /// <param name="post"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<Result<Post>> FindAndFillWithCategoryAsync(Post post, CancellationToken cancellation)
        {
            var category = await categoryRepository.GetAsync(w => w.Id == _request.Category);

            if (category.NoHasElement()) return Result.Fail<Post>(CategoryErrors.CategoryNotFound);

            post.Category = category.FirstOrDefault()!;

            postRepository.Insert(post);

            return post;
        }

       
    }
}
