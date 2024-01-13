using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.AddUser
{
    public class AddUserRequestValidation:AbstractValidator<AddUserRequest>
    {
        public AddUserRequestValidation()
        {
            RuleFor(x => x.UserName).NotEmpty().NotNull();
            RuleFor(x => x.Password).NotEmpty().NotNull();
            RuleFor(x => x.Email).NotEmpty().NotNull().EmailAddress();
        }
    }
}
