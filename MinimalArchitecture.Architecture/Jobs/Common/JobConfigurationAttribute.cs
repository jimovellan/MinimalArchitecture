using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Jobs.Common
{

    
    public class JobConfigurationAttribute : Attribute
    {

        public string CronSchedule { get; set; } = TimePeriods.Every24Hours;
        public JobConfigurationAttribute(string cronSchedule = TimePeriods.Every24Hours)
        {
            CronSchedule = cronSchedule;
        }
    }
}
