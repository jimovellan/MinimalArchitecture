using MinimalArchitecture.Entities.Authorization.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
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
    }
}
