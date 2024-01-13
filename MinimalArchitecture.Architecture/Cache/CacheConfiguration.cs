using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Cache
{
    public static class CacheConfiguration
    {
        private static int? _minutesToExpire = null;

        private static int DEFAULT_MINUTES_TO_EXPIRE = 60;
        public static int MinutesToExpire { get => _minutesToExpire is null ? DEFAULT_MINUTES_TO_EXPIRE : (int)_minutesToExpire; 
                                            set => _minutesToExpire = value; }
    }
}
