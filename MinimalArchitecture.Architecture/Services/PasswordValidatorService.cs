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
            throw new NotImplementedException();
        }

        public bool Validate(string password, string hash)
        {
            throw new NotImplementedException();
        }
    }
}
