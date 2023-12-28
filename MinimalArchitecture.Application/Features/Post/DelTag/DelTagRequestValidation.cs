using FluentValidation;
using MinimalArchitecture.Application.Features.Post.DelTag;

namespace MinimalArchitecture.Application.Features.Post.DelTag
{
    public class DelTagRequestValidation: AbstractValidator<DelTagRequest>
    {
        public DelTagRequestValidation()
        {
            RuleFor(p => p.Id).GreaterThan(0);
            
        }
    }

}
