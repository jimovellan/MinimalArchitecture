using MediatR;
using Microsoft.Extensions.Logging;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Common.Extensions;
using MinimalArchitecture.Common.Results;
using MinimalArchitecture.Entities.Authorization.Models;
using MinimalArchitecture.Entities.Repository;
using System.Linq;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.SeedMinimalData
{
    public class SeedMinimalDataRequestHandler : IRequestHandler<SeedMinimalDataRequest, Result>
    {
        private readonly IPasswordValidation _passwordValidation;
        private readonly ILogger<SeedMinimalDataRequestHandler> _logger;
        private readonly IRepositoryBase<User> _userRepository;
        private readonly IRepositoryBase<Rol> _rolRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SeedMinimalDataRequestHandler(IPasswordValidation passwordValidation,
                                             ILogger<SeedMinimalDataRequestHandler> logger,
                                             IRepositoryBase<User> userRepository,
                                             IRepositoryBase<Rol> rolRepository,
                                             IUnitOfWork unitOfWork)
        {
            _passwordValidation = passwordValidation;
            _logger = logger;
            _userRepository = userRepository;
            _rolRepository = rolRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<Result> Handle(SeedMinimalDataRequest request, CancellationToken cancellationToken)
        {
            var listOfRoles = new Rol[]{
                new Rol { Id = 1, Description = "Administrador", RolType = Entities.Authorization.Enums.RolType.All, },
                new Rol { Id = 2, Description = "Lector", RolType = Entities.Authorization.Enums.RolType.Read },
                new Rol { Id = 3, Description = "Publicador", RolType = Entities.Authorization.Enums.RolType.ReadWrite }

            };

            var ids = listOfRoles.Select(s => s.Id);

            var roles =  _rolRepository.GetQuery().Where(r => ids.Contains(r.Id)) ;

            var idsFinded = roles.Select(s => s.Id);

            foreach (var rol in listOfRoles.Where(r=> !idsFinded.Any(a=> a == r.Id)).ToList())
            {
                _rolRepository.Insert(rol);
            }

            await _unitOfWork.Save();

            var rolAdminin = (await _rolRepository.GetAsync(x=>x.Id == 1)).FirstOrDefault();

            rolAdminin!.ThrowExceptionIfNull(nameof(rolAdminin));

            if(!_userRepository.GetQuery().Any(x=>x.Name == "Administrador"))
            {
                _userRepository.Insert(new User
                {
                    Name = "Administrador",
                    Email = "Administrador@gmail.com",
                    Roles = new List<Rol>() { rolAdminin },
                    Hash = _passwordValidation.GenerateHash("123456"),
                });
            }

            await _unitOfWork.Save();
            return Result.Ok();

            
        }
    }
}
