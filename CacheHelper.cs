using Microsoft.Extensions.Caching.Memory;
using System;

public class CacheHelper
{
    private readonly IMemoryCache _memoryCache;

    public CacheHelper(IMemoryCache memoryCache)
    {
        _memoryCache = memoryCache;
    }

    public T CachedLong<T>(string cacheKey, Func<T> getItemCallback)
    {
        // Try to get the cached item
        if (_memoryCache.TryGetValue(cacheKey, out T cachedValue))
        {
            return cachedValue;
        }

        // If not in cache, load the item
        var item = getItemCallback();

        // Store it indefinitely (or a very long time)
        _memoryCache.Set(cacheKey, item);

        return item;
    }

    public T Cached<T>(string cacheKey, Func<T> getItemCallback)
    {
        // If in cache, return
        if (_memoryCache.TryGetValue(cacheKey, out T cachedValue))
        {
            return cachedValue;
        }

        // Otherwise, get the item and store it with a 5-minute expiry
        var newItem = getItemCallback();
        _memoryCache.Set(cacheKey, newItem, TimeSpan.FromMinutes(5));
        return newItem;
    }
}
