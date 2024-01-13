using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Tags.AddTag
{
    public class AddTagRequestValidation : AbstractValidator<AddTagRequest>
    {
        public AddTagRequestValidation()
        {
            RuleFor(p => p.Name).NotEmpty().NotNull();
        }
    }

}
