using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MinimalArchitecture.Application.Dto.Authentication;
using MinimalArchitecture.Entities.Authorization.Enums;

namespace MinimalArchitecture.Application.Services
{
    public interface  IUserInfoService
    {

        UserInfo GetUser();

        bool IsAuth();

        bool IsInRole(RolType roleType);

    }
}
