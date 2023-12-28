using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.Login
{
    public class LoginReponse
    {
        public string Token { get; set; }
        public String RefreshToken { get; set; } = default!;
    }
}
