using MediatR;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Authorization.Specs;
using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.Login
{
    internal class LoginRequestHandler : IRequestHandler<LoginRequest, Result<string>>
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IPasswordValidation _passwordValidator;

        public LoginRequestHandler(IRepositoryBase<User> userRepository,
                                   IPasswordValidation passwordValidator)
        {
            _userRepository = userRepository;
            _passwordValidator = passwordValidator;
        }
        public async Task<Result<string>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {

            var user = await _userRepository.GetWithSpecAsync(new GetUserByNameCompleted(request.User),cancellationToken);
            return null;
            
        }


        

    }
}
