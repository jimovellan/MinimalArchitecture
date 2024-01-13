using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Cache
{
    public class CacheDbContextFactory : IDesignTimeDbContextFactory<dbContextCache>
    {
        public dbContextCache CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<dbContextCache>();
            optionsBuilder.UseSqlServer("Data Source=blog.db");
            return new dbContextCache(optionsBuilder.Options);
        }
    }


  
}
