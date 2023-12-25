using MinimalArchitecture.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Errors
{
    public class TokenErrors
    {
        public const string EXPIRED_TOKEN_CODE = "TKEXPIRED";
        public const string TOKEN_NOT_VALID_CODE = "TKNOTVALID";
        public static DomainError ExpiredToken => new DomainError(EXPIRED_TOKEN_CODE, Resources.Error.TOKEN_EXPIRED);

        public static DomainError NotValidToken => new DomainError(TOKEN_NOT_VALID_CODE, Resources.Error.TOKEN_NOT_VALID);
    }
}
