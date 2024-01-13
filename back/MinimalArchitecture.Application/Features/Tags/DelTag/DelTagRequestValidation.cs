using FluentValidation;

namespace MinimalArchitecture.Application.Features.Tags.DelTag
{
    public class DelTagRequestValidation : AbstractValidator<DelTagRequest>
    {
        public DelTagRequestValidation()
        {
            RuleFor(p => p.Id).GreaterThan(0);

        }
    }

}
