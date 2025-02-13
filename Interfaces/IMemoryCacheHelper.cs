namespace WebApplication1.Interfaces
{
    public interface IMemoryCacheHelper
    {
        T CachedLong<T>(string key, Func<T> getItemCallback);
        T Cached<T>(string key, Func<T> getItemCallback);
    }
}
