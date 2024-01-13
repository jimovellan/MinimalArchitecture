using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Posts.AddPost
{
    public class AddPostRequestValidation: AbstractValidator<AddPostRequest>
    {
        public AddPostRequestValidation()
        {
            RuleFor(r => r.Html).NotEmpty().NotNull();

            RuleFor(r => r.Title).NotEmpty().NotNull();

            RuleFor(r => r.Description).NotEmpty().NotNull();

            RuleFor(r => r.Category).NotEmpty().NotNull();
        }
    }
}
