using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Authorization.Enums
{
    [Flags]
    public enum RolType
    {
        None = 0,
        Read = 1,
        Write = 2,
        //CustomRole1 = 4,
        //CustomRole2 = 8,
        //CustomRole3 = 16,
        //CustomRole4 = 32,
        //CustomRole5 = 64,
        Admin = 512, // Nuevo valor alto para Admin
        ReadWrite = Read | Write, // Combinación de lectura y escritura
        All = Read | Write | Admin // Todos los permisos
    }
}
