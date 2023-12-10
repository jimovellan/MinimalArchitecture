using FluentValidation;
using MediatR;
using MinimalArchitecture.Common.Results;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.Login
{
    public class LoginRequest : IRequest<Result<string>>
    {
        public string? User { get; set; }
        public string? Password { get; set; }
    }

    public class LoginRequestValidation : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidation()
        {
            RuleFor(r => r.User).NotEmpty().MinimumLength(3);
            RuleFor(r => r.Password).NotEmpty();
        }
    }
}
