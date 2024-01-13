using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Cache
{
    [Table(name:"CacheKey",Schema ="cache")]
    public class CacheKey
    {
        [Key]
        public string Id { get; set; } = string.Empty;
        public string? Value { get; set; }
        public string Type { get; set; } = string.Empty;
        public DateTime Expired { get; set; }
    }
}
