using MinimalArchitecture.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Errors
{
    public class Token
    {
        public const string EXPIRED_TOKEN_CODE = "TKEXPIRED";
        public const string TOKEN_NOT_VALID_CODE = "TKNOTVALID";
        public static Error ExpiredToken => new Error(EXPIRED_TOKEN_CODE, Resources.Error.TOKEN_EXPIRED);

        public static Error NotValidToken => new Error(TOKEN_NOT_VALID_CODE, Resources.Error.TOKEN_NOT_VALID);
    }
}
