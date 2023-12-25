using MediatR;
using Microsoft.Extensions.Logging;
using MinimalArchitecture.Application.Dto.Authentication;
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
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.RefreshToken
{
    public class RefreshTokenHandler : IRequestHandler<RefreshTokenRequest, Result<RefreshTokenResponse>>
    {
        private readonly ILogger<RefreshTokenHandler> _logger;
        private readonly IRepositoryBase<User> _userRepository;
        private readonly ITokenService _tokenService;
        private readonly IUnitOfWork _unitOfWork;

        public RefreshTokenHandler(ILogger<RefreshTokenHandler> logger,
                                   IRepositoryBase<User> userRepository,
                                   ITokenService tokenService,
                                   IUnitOfWork unitOfWork)
        {
            _logger = logger;
            _userRepository = userRepository;
            _tokenService = tokenService;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result<RefreshTokenResponse>> Handle(RefreshTokenRequest request, CancellationToken cancellationToken)
        {
            return await GetUserFromToken(request, cancellationToken)
                               .ThenAsync(FindUser)
                               .ThenAsync(GenerateNewTokenAndTokenRefresh);

   
        }
        /// <summary>
        /// destructure token into a user info
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public   Result<(UserInfo userInfo, RefreshTokenRequest request)> GetUserFromToken(RefreshTokenRequest request, CancellationToken cancellation)
        {
            var userInfo = _tokenService.GetUser(request.Token);

            if (userInfo.IsFailed) return Result.Fail<(UserInfo, RefreshTokenRequest)>(userInfo.Errors);

            return Result.Ok((userInfo.Value, request));
        }
        
        /// <summary>
        /// Find the user complete from de database
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<Result<(User user, RefreshTokenRequest request)>> FindUser((UserInfo user, RefreshTokenRequest request) input, CancellationToken cancellation)
        {
            var userFindend = await _userRepository.GetWithSpecAsync(new GetUserByIdCompleted(input.user.Id, true, true));

            if (userFindend.NoHasElement()) return Result.Fail<(User, RefreshTokenRequest)>(UserErrors.UserNotFound);
            
            var user = userFindend.FirstOrDefault();

            return Result.Ok((user, input.request));
        }

       
        /// <summary>
        /// Generate new token and cancel old refresh token
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellation"></param>
        /// <returns></returns>
        public async Task<Result<RefreshTokenResponse>> GenerateNewTokenAndTokenRefresh((User user, RefreshTokenRequest request) input,CancellationToken cancellation)
        {
            //search a match with de request active 
            var token = input.user.Tokens.FirstOrDefault(t => t.AsociatedToken == input.request.Token &&
                                                  t.RefreshToken == input.request.RefreshToken &&
                                                  t.Active &&
                                                  t.ExpirationTime > DateTime.Now);

            if (token is null) return Result.Fail<RefreshTokenResponse>(TokenErrors.NotValidToken);

            token.Active = false;


            var newToken = _tokenService.GenerateToken(input.user);

            newToken.ThrowExceptionIfNull(nameof(newToken));

            var refreshToken = new Token()
            {
                Active = true,
                ExpirationTime = DateTime.Now.AddDays(1),
                AsociatedToken = newToken,
                RefreshToken = Guid.NewGuid().ToString(),
            };

            input.user.Tokens.Add(refreshToken);

            await _unitOfWork.Save();

            return Result.Ok(new RefreshTokenResponse()
            {
                Token = refreshToken.AsociatedToken,
                RefreshToken = refreshToken.RefreshToken
            });

        }
    



    }
}
