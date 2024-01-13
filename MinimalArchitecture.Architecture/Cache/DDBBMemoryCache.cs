using Microsoft.Extensions.Caching.Memory;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Cache
{
    public class DDBBMemoryCache : IMemoryCache
    {
        private readonly dbContextCache ctx;

        public DDBBMemoryCache(dbContextCache ctx)
        {
            this.ctx = ctx;
        }

        public ICacheEntry CreateEntry(object key)
        {
            return new CacheDBEntry(ctx,key);
        }

        public void Dispose()
        {
            
        }

        public void Remove(object key)
        {
            var cache = ctx.CacheKeys.FirstOrDefault(f => f.Id == (string)key);
            if (cache != null)
            {
                ctx.CacheKeys.Remove(cache);
                ctx.SaveChanges();
            }
        }

        public bool TryGetValue(object key, out object? value)
        {
             var finded = ctx.CacheKeys.FirstOrDefault(f => f.Id == (string)key && f.Expired >= DateTime.Now);
             if (finded != null)
             {
                value = JsonConvert.DeserializeObject(finded.Value,Type.GetType(finded.Type));
             }
            else
            {
                value = null;
            }
             return value != null;

        }
    }
}
