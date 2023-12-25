using MinimalArchitecture.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Errors
{
    public static class UserErrors
    {
        public const string USER_NOT_FOUND_CODE = "USRNOTFOUND";


        public static DomainError UserNotFound => new DomainError(USER_NOT_FOUND_CODE, Resources.Error.USR_NOT_FOUND);

    }
}
