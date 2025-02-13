using Microsoft.Extensions.Caching.Memory;
using System;
using WebApplication1.Interfaces;

namespace WebApplication1.Helpers
{
    public class MemoryCacheHelper : IMemoryCacheHelper
    {
        private readonly IMemoryCache _memoryCache;

        public MemoryCacheHelper(IMemoryCache memoryCache)
        {
            _memoryCache = memoryCache;
        }

        public T CachedLong<T>(string key, Func<T> getItemCallback)
        {
            if (_memoryCache.TryGetValue(key, out T cachedValue))
            {
                return cachedValue;
            }
            T item = getItemCallback();
            _memoryCache.Set(key, item); // No expiration set
            return item;
        }

        public T Cached<T>(string key, Func<T> getItemCallback)
        {
            if (_memoryCache.TryGetValue(key, out T cachedValue))
            {
                return cachedValue;
            }
            T item = getItemCallback();
            _memoryCache.Set(key, item, TimeSpan.FromMinutes(5));
            return item;
        }
    }

}

    

