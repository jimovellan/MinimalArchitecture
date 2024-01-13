using MinimalArchitecture.Application.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Services
{
    public class PasswordValidatorService : IPasswordValidation
    {
        public string GenerateHash(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool Validate(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}
