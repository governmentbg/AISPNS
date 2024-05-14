using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AISTN.Common.Helper
{
    public class CacheManager
    {
        public IMemoryCache memoryCache;

        public CacheManager(IMemoryCache cache)
        {
            this.memoryCache = cache;
        }

        public T GetOrSet<T>(string cacheKey, Func<T> getItemCallback) where T : class
        {
            T item = memoryCache.Get<T>(cacheKey);


            if (item == null)
            {
                item = getItemCallback();
                if (item != null)
                    memoryCache.Set(cacheKey, item, DateTime.Now.AddMinutes(1));
            }
            return item;
        }

        public void ClearCache(string cacheKey)
        {
            memoryCache.Remove(cacheKey);
        }
    }

    public static class CacheConstants
    {
        public static string CACHEKEY_USERS = "Users";
    }
}
