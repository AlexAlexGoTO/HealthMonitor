using MedixineMonitor.Application.Common.Interfaces;
using Microsoft.Extensions.Caching.Memory;

namespace MedixineMonitor.Infrastructure.Caching;

public class MemoryCacheStore : ICacheStore
{
    private readonly IMemoryCache _memoryCache;
    private readonly Dictionary<string, TimeSpan> _expirationConfiguration;

    public MemoryCacheStore(
        IMemoryCache memoryCache,
        Dictionary<string, TimeSpan> expirationConfiguration)
    {
        _memoryCache = memoryCache;

        _expirationConfiguration = expirationConfiguration;
    }

    public void Add<TItem>(TItem item, string key, TimeSpan? expirationTime = null)
    {
        var cachedObjectName = item.GetType().Name;

        TimeSpan timespan;

        if (expirationTime.HasValue)
        {
            timespan = expirationTime.Value;
        }
        else
        {
            timespan = _expirationConfiguration[cachedObjectName];
        }

        this._memoryCache.Set(key, item, timespan);
    }

    public void Add<TItem>(TItem item, string key, DateTime? absoluteExpiration = null)
    {
        DateTimeOffset offset;

        if (absoluteExpiration.HasValue)
        {
            offset = absoluteExpiration.Value;
        }
        else
        {
            offset = DateTimeOffset.MaxValue;
        }

        this._memoryCache.Set(key, item, offset);
    }

    public TItem Get<TItem>(string key) where TItem : class
    {
        if (this._memoryCache.TryGetValue(key, out TItem value))
        {
            return value;
        }

        return null;
    }

    public void Remove<TItem>(string key)
    {
        this._memoryCache.Remove(key);
    }
}
