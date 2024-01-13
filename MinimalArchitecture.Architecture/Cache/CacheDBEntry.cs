using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;
using MinimalArchitecture.Common.Extensions;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace MinimalArchitecture.Architecture.Cache
{
    internal class CacheDBEntry : ICacheEntry
    {
        private readonly dbContextCache ctx;

        private CacheKey cacheKey;

        public CacheDBEntry(dbContextCache ctx, object key)
        {
            ctx.ThrowExceptionIfNull(nameof(ctx));
            this.ctx = ctx;
            cacheKey = ctx.CacheKeys.Where(w => w.Id == (string)key).FirstOrDefault();

            if(cacheKey is null)
            {
                cacheKey = new CacheKey() { Id = (string)key, 
                                            Expired = DateTime.Now.AddMinutes(CacheConfiguration.MinutesToExpire) };
                ctx.CacheKeys.Add(cacheKey);
            }
            
        }
        public DateTimeOffset? AbsoluteExpiration { get; set; }
        public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }

        public IList<IChangeToken> ExpirationTokens { get; } = new List<IChangeToken>();

        public object Key => cacheKey.Id;

        public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks  { get; } = new List<PostEvictionCallbackRegistration>();

        public CacheItemPriority Priority { get ; set; }
        public long? Size { get  ; set ; }
        public TimeSpan? SlidingExpiration { get; set; }
        
        public object? Value
        {
            get
            {
                if (cacheKey.Type is null) return null;

                Type type = Type.GetType(cacheKey.Type);

               return JsonConvert.DeserializeObject(cacheKey.Value, type);

            }
            set
            {
                SaveData(value);
            }
        } 

        public void SaveData(Object? value)
        {

            this.cacheKey.Type = value.GetType().FullName;
            this.cacheKey.Value = value.ToJson();

            ctx.SaveChanges();
        }

        public void Dispose()
        {
            
        }
    }
}
