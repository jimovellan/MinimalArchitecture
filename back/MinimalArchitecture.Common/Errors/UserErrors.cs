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
        public const string USER_EXIST_PREVIOUSLY_CODE = "USREXIST000";
        public const string USER_EMAIL_EXISTS_PREVIOUSLY_CODE = "USREXIST001";


        public static DomainError UserNotFound => new DomainError(USER_NOT_FOUND_CODE, Resources.Error.USR_NOT_FOUND);
        public static DomainError UserExistsPreviously => new DomainError(USER_EXIST_PREVIOUSLY_CODE, Resources.Error.USR_EXISTS);
        public static DomainError UserEmailExistsPreviously => new DomainError(USER_EMAIL_EXISTS_PREVIOUSLY_CODE, Resources.Error.USR_EMAIL_EXISTS);

    }
}
