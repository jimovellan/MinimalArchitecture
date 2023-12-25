using MinimalArchitecture.Common.Models;

namespace MinimalArchitecture.Common.Errors
{
    public static class Common
    {
        public const string UNEXPECTED_ERROR_CODE = "E00000000";
        /// <summary>
        /// When unexpected error not controlled
        /// </summary>
        /// <returns></returns>
        public static DomainError UNEXPECTED_ERROR ()=> new DomainError(UNEXPECTED_ERROR_CODE, Resources.Error.ERROR_GENERAL) ;
    }
}
