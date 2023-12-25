using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Common.Models
{
    public class DomainError
    {
        public string Code { get; private set; }
        public string Description { get; private set; }

        public DomainError(string code, string description)
        {
            Code = code;
            Description = description;
        }

    }
}
