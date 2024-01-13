using MediatR;
using Microsoft.Extensions.Logging;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Errors;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Repository;

namespace MinimalArchitecture.Application.Features.Autorization.AddUser
{
    public class AddUserRequestHandler : IRequestHandler<AddUserRequest, Result>
    {
        private readonly ILogger<AddUserRequestHandler> logger;
        private readonly IPasswordValidation passwordValidation;
        private readonly IRepositoryBase<User> userRepository;
        private readonly IRepositoryBase<Rol> rolRepository;
        private readonly IUnitOfWork unitOfWork;

        public AddUserRequestHandler(ILogger<AddUserRequestHandler> logger,
                                     IPasswordValidation passwordValidation,
                                     IRepositoryBase<User> userRepository,
                                     IRepositoryBase<Rol> rolRepository,
                                     IUnitOfWork unitOfWork)
        {
            this.logger = logger;
            this.passwordValidation = passwordValidation;
            this.userRepository = userRepository;
            this.rolRepository = rolRepository;
            this.unitOfWork = unitOfWork;
        }

        public Task<Result> Handle(AddUserRequest request, CancellationToken cancellationToken)
        {
            return ValidateIfExistsUser(request, cancellationToken)
                         .ThenAsync(CreateUser);

        }

        /// <summary>
        /// validate if exists previously
        /// </summary>
        /// <param name="request"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public async Task<Result<AddUserRequest>> ValidateIfExistsUser(AddUserRequest request, CancellationToken cancellationToken)
        {
            if(await userRepository.ExistAsync(w => w.Name == request.UserName))
            {
                return Result.Fail<AddUserRequest>(UserErrors.UserExistsPreviously);
            }

            if (await userRepository.ExistAsync(w => w.Email == request.Email))
            {
                return Result.Fail<AddUserRequest>(UserErrors.UserEmailExistsPreviously);
            }

            return request;

        }

        /// <summary>
        /// create new user with the request info
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<Result> CreateUser(AddUserRequest request)
        {
            var newUser = new User()
            {
                Active = true,
                Hash = passwordValidation.GenerateHash(request.Password),
                Email = request.Email,
                Name = request.UserName
            };

            var roles = await rolRepository.GetAsync(w => w.RolType == Entities.Authorization.Enums.RolType.ReadWrite);

            if (roles.NoHasElement()) throw new Exception("No exists publicador rol");

            newUser.Roles.Add(roles.FirstOrDefault()!);

            userRepository.Insert(newUser);

            await unitOfWork.Save();

            return Result.Ok();
        }


    }
}
