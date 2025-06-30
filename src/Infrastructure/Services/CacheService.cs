using Application.Commons.Constants;
using Application.Commons.Interfaces;
using LazyCache;

namespace Infrastructure.Services;

public class CacheService : ICacheService
{
    private readonly IAppCache _cache;

    public CacheService(IAppCache cache)
    {
        _cache = cache;
    }
    
    public T Get<T>(string value) => _cache.Get<T>(value);

    public T GetOrAddDaily<T>(string value, T item)
    {
        var result = _cache.GetOrAdd(value, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromHours(6);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);

            return item;
        });

        return result;
    }

    public void Remove(string value)
    {
        _cache.Remove(value);
    }
    
    public string Template(string key)
    {
        var result = _cache.GetOrAdd(key, entry =>
        {
            entry.SlidingExpiration = TimeSpan.FromDays(1);
            entry.AbsoluteExpirationRelativeToNow = TimeSpan.FromDays(1);
            
            return key switch
            {
                nameof(TemplateConstant.TemplateActivation) => File.ReadAllText($"{Directory.GetCurrentDirectory()}/Templates/activation.html"),
                nameof(TemplateConstant.TemplateLocked) => File.ReadAllText($"{Directory.GetCurrentDirectory()}/Templates/locked.html"),
                nameof(TemplateConstant.TemplateWelcome) => File.ReadAllText($"{Directory.GetCurrentDirectory()}/Templates/welcome.html"),
                _ => null
            };
        });

        return result;
    }
}