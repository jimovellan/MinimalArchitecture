using MinimalArchitecture.Entities.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Entities.Authorization.Models
{
    public class Token:BaseEntity
    {
        public int Id { get; set; }
        public string AsociatedToken { get; set; } = default!;
        public string RefreshToken { get; set; } = default!;
        public DateTime ExpirationTime { get; set; }
        public bool Active { get; set; }

        public User User { get; set; }
    }
}
