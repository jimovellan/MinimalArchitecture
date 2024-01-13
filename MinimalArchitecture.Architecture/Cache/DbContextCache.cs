

using Microsoft.EntityFrameworkCore;

namespace MinimalArchitecture.Architecture.Cache
{
    public class dbContextCache:DbContext
    {
        public dbContextCache(DbContextOptions<dbContextCache> options):base(options)
        {
            
        }

        public DbSet<CacheKey> CacheKeys { get; set; }

       
    }
}
