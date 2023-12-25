using Microsoft.AspNetCore.Authorization.Infrastructure;
using MinimalArchitecture.Entities.Authorization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Dto.Authentication
{
    public class UserInfo
    {
        public UserInfo()
        {
            Rol = new List<RolType>(); 
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public ICollection<RolType> Rol { get; set; }

        /// <summary>
        /// Build de user from the claims
        /// </summary>
        /// <param name="claims"></param>
        /// <returns></returns>
        public static UserInfo Build(IEnumerable<Claim> claims)
        {
            var userInfo = new UserInfo()
            {
                Email = claims.FirstOrDefault(w => w.Type == ClaimTypes.Email)?.Value ?? "Undefined",
                Id = int.Parse(claims.FirstOrDefault(w => w.Type == ClaimTypes.Sid)?.Value ?? "0"),
                Name = claims.FirstOrDefault(w => w.Type == ClaimTypes.Name)?.Value ?? "Undefined",
            };

            var roles = claims.Where(w => w.Type == ClaimTypes.Role).Select(s => s.Value) ?? new List<string>();


            foreach (var rol in roles)
            {
                Enum.TryParse<RolType>(rol, out RolType output);
                userInfo.Rol.Add(output);
            }

            return userInfo;
        }
    }
}
