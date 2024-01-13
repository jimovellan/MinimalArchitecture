using MediatR;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Posts.Models;

namespace MinimalArchitecture.Application.Features.Posts.AddPost
{
    public class AddPostRequest : IRequest<Result<Post>>
    {
        public AddPostRequest()
        {
            Tags = new List<string>();
        }
        public string Title { get; set; } = default!;
        public int Category { get; set; }
        public string Description { get; set; } = default!;
        public string Html { get; set; } = default!;
        public List<string> Tags { get; set; }
    }
}
