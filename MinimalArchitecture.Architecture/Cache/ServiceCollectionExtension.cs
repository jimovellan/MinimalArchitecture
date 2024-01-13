using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Cache
{
    public static class ServiceCollectionExtension
    {

        public static void AddCacheDistributedSQLSERVER(this IServiceCollection service,string connectionString,int? minutesToExpired = null)
        {
            service.AddDbContext<dbContextCache>(options =>
            {
                options.UseSqlServer(connectionString, options =>
                {
                    options.MigrationsHistoryTable("__CacheMigrationsHistory", "cache");
                    
                });
            }, ServiceLifetime.Scoped);
           
           
            service.AddScoped<IMemoryCache,DDBBMemoryCache>();
            if (minutesToExpired is not null) CacheConfiguration.MinutesToExpire = (int)minutesToExpired;


            service.BuildServiceProvider().GetService<dbContextCache>().Database.Migrate();
        }
    }
}
