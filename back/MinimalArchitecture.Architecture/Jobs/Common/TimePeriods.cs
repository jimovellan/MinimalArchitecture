using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Jobs.Common
{
    public static class TimePeriods
    {
        public const string EveryMinute = "0 * * * * ?";
        public const string Every5Minutes = "0 */5 * * * ?";
        public const string Every10Minutes = "0 */10 * * * ?";
        public const string Every30Minutes = "0 */30 * * * ?";
        public const string EveryHour = "0 0/1 * * * ?";
        public const string Every2Hours = "0 0/2 * * * ?";
        public const string Every3Hours = "0 0/3 * * * ?";
        public const string Every4Hours = "0 0/4 * * * ?";
        public const string Every6Hours = "0 0/6 * * * ?";
        public const string Every12Hours = "0 0/12 * * * ?";
        public const string Every24Hours = "0 0 * * * ?";
        public const string EveryHalfMinute = "0/2 * * * * ?";
        public const string Every30Seconds = "0/30 * * * * ?";
        public const string Every15Seconds = "0/15 * * * * ?";
        public const string Every10Seconds = "0/10 * * * * ?";
    }
}
