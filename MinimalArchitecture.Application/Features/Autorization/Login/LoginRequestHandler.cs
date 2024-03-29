﻿using MediatR;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Configuration;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Errors;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Authorization.Specs;
using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.Login
{
    internal class LoginRequestHandler : IRequestHandler<LoginRequest, Result<LoginReponse>>
    {
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IPasswordValidation _passwordValidator;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMemoryCache memoryCache;

        public LoginRequestHandler(IRepositoryBase<User> userRepository,
                                   IPasswordValidation passwordValidator,
                                   ITokenService tokenService,
                                   IUnitOfWork unitOfWork,
                                   IMemoryCache memoryCache)
        {
            _userRepository = userRepository;
            _passwordValidator = passwordValidator;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
            this.memoryCache = memoryCache;
        }
        public async Task<Result<LoginReponse>> Handle(LoginRequest request, CancellationToken cancellationToken)
        {
            var value = memoryCache.Get<int?>("prueba");
            if(value is null){
                memoryCache.Set<int>("prueba", 5);
            }
            

            var usersFinded = await _userRepository.GetWithSpecAsync(new GetUserByNameCompleted(request.User!,true),cancellationToken);
            

            if(usersFinded.NoHasElement()) return Result.Fail<LoginReponse>(UserErrors.UserNotFound,cancellationToken);

            var user = usersFinded.FirstOrDefault();

            if(user is null) return Result.Fail<LoginReponse>(UserErrors.UserNotFound,cancellationToken);

            if(!_passwordValidator.Validate(request.Password!, user.Hash!)) return Result.Fail<LoginReponse>(LoginErrors.LOGIN_INVALID,cancellationToken);

            var token = _tokenService.GenerateToken(user);

           
            token.ThrowExceptionIfNull(nameof(token));

            user.Tokens?.ToList().ForEach(x => x.Active = false);


            var newRefreshToken = Guid.NewGuid().ToString();

            user.Tokens!.Add(new Token()
            {
                AsociatedToken = token,
                RefreshToken = newRefreshToken,
                ExpirationTime = DateTime.UtcNow.AddDays(1),
                Active = true
            });

            await _unitOfWork.Save();


            return Result.Ok(new LoginReponse
            {
                Token = token,
                RefreshToken = newRefreshToken,
                
            }, cancellationToken);
            
        }


        

    }
}
