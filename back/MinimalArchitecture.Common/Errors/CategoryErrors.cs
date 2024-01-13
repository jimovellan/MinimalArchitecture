using MinimalArchitecture.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Errors
{
    public class CategoryErrors
    {
        public const string CATEGORY_NOT_FOUND_CODE = "CATNOTFOUND";

        public static DomainError CategoryNotFound => new DomainError(CATEGORY_NOT_FOUND_CODE, Resources.Error.CATEGORY_NOT_FOUND);
    }
}
