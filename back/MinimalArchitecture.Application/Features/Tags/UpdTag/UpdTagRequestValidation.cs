using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Tags.UpdTag
{
    public class DelTagRequestValidation : AbstractValidator<UpdTagRequest>
    {
        public DelTagRequestValidation()
        {
            RuleFor(p => p.Id).GreaterThan(0);
            RuleFor(p => p.Name).NotEmpty().NotNull();
        }
    }

}
