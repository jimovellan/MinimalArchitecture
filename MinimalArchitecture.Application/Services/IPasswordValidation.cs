using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Services
{
    /// <summary>
    /// Service to management encrypted passwords
    /// </summary>
    public interface IPasswordValidation
    {
        bool Validate(string password, string hash);
        string GenerateHash(string password);
    }
}
