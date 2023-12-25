using MinimalArchitecture.Common.Resources;
using MinimalArchitecture.Common.Models;

namespace MinimalArchitecture.Common.Errors
{
    public static class LoginErrors
    {
        public const string LOGIN_INVALID_CODE = "LOGININVALID";

        public static DomainError LOGIN_INVALID => new DomainError(LOGIN_INVALID_CODE, Resources.Error.LOGIN_INVALID);
    }
}
