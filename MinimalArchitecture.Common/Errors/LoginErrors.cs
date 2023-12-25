using MinimalArchitecture.Common.Models;
using MinimalArchitecture.Common.Resources;

namespace MinimalArchitecture.Common.Errors
{
    public static class LoginErrors
    {
        public const string LOGIN_INVALID_CODE = "LOGININVALID";

        public static DomainError LOGIN_INVALID => new DomainError(LOGIN_INVALID_CODE, Error.LOGIN_INVALID);
    }
}
