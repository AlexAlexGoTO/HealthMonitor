namespace MedixineMonitor.Application.Common.Interfaces;

public interface ICacheStore
{
    void Add<TItem>(TItem item, string key, TimeSpan? expirationTime = null);

    void Add<TItem>(TItem item, string key, DateTime? absoluteExpiration = null);

    TItem Get<TItem>(string key) where TItem : class;

    void Remove<TItem>(string key);
}

