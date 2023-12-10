using MinimalArchitecture.Common.Models;
using MinimalArchitecture.Common.Resources;

namespace MinimalArchitecture.Common.Errors
{
    public static class Common
    {
        /// <summary>
        /// When unexpected error not controlled
        /// </summary>
        /// <returns></returns>
        public static Models.Error UNEXPECTED_ERROR ()=> new Models.Error("E0000000", Resources.Error.ERROR_GENERAL) ;
    }
}
