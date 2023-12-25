using Microsoft.AspNetCore.Http;
using MinimalArchitecture.Application.Dto.Authentication;
using MinimalArchitecture.Application.Services;
using MinimalArchitecture.Entities.Authorization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Services
{
    public class UserInfoService : IUserInfoService
    {
        private UserInfo _userInfo = null;

        public UserInfoService(IHttpContextAccessor httpContext)
        {
            try
            {
                var claims = httpContext?.HttpContext?.User?.Claims;
                _userInfo = UserInfo.Build(claims);
            }
            catch (Exception ex)
            {
                _ = ex;
            }
            
        }

        public UserInfo GetUser()
        {
            return _userInfo;
        }

        public bool IsAuth()
        {
            return _userInfo != null;
        }

        public bool IsInRole(RolType roleType)
        {
            return IsAuth() && _userInfo.Rol.Any(a => a == roleType);
        }
    }
}
