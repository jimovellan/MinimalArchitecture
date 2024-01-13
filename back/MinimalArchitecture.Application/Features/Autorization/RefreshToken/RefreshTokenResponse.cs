using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Application.Features.Autorization.RefreshToken
{
    public class RefreshTokenResponse
    {
        public string Token { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
    }
}
