using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Config
{
    public class JWTSettings
    {
        public JWTSettings()
        {
            
        }
        public string Secret { get; set; } = default!;
        public string Issuer { get; set; } = default!;
        public string Audience { get; set; } = default!;
        public int MinToExpire { get; set; }
    
    }
}
