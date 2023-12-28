using MinimalArchitecture.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Errors
{
    public static class TagErrors
    {
        public const string TAG_NO_FOUND_CODE = "TAGNOFOUND";
        public const string TAG_NO_UPDATE_CODE = "TAGNOUPD0";
        public const string TAG_NO_INSERT_CODE = "TAGNOUPD1";
        public const string TAG_NO_DELETE_CODE = "TAGNODLT0";
        public const string TAG_DUPLICATE_CODE = "TAGDUPLICATE";

        public static DomainError TAG_NO_FOUND => new DomainError(TAG_NO_FOUND_CODE, Resources.Error.TAG_NO_FOUND);
        public static DomainError TAG_NO_UPDATED => new DomainError(TAG_NO_UPDATE_CODE, Resources.Error.TAG_NO_UPDATE);
        public static DomainError TAG_NO_INSERTED => new DomainError(TAG_NO_INSERT_CODE, Resources.Error.TAG_NO_INSERT);
        public static DomainError TAG_NO_DELETED => new DomainError(TAG_NO_DELETE_CODE, Resources.Error.TAG_NO_DELETE);
        public static DomainError TAG_DUPLICATE => new DomainError(TAG_DUPLICATE_CODE, Resources.Error.TAG_EXIST_PREVIOUS);
    }
}
