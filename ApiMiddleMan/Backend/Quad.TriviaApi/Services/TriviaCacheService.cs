using Microsoft.Extensions.Caching.Distributed;
using Quad.TriviaApi.Models;
using System.Text.Json;

namespace Quad.TriviaApi.Services;

public class TriviaCacheService : ITriviaCacheService
{
    private readonly IDistributedCache _cache;
    private readonly IHttpContextAccessor _accessor;

    public TriviaCacheService(IDistributedCache cache, IHttpContextAccessor accessor)
    {
        _cache = cache;
        _accessor = accessor;
    }

    private ISession? Session => _accessor.HttpContext?.Session;
    private string? Key => Session?.GetString(nameof(TriviaData));

    public async Task ClearCache()
    {
        if (string.IsNullOrWhiteSpace(Key)) return;
        await _cache.RemoveAsync(Key);
    }

    public async Task StoreCacheData(TriviaData session, TimeSpan? absoluteExpiration = null)
    {
        if (string.IsNullOrWhiteSpace(Key)) return;
        var options = new DistributedCacheEntryOptions()
        {
            SlidingExpiration = absoluteExpiration ?? TimeSpan.FromMinutes(30),
        };

        var json = JsonSerializer.Serialize(session);
        await _cache.SetStringAsync(Key, json, options);
    }

    public async Task<TriviaData?> GetCacheData()
    {
        if (string.IsNullOrWhiteSpace(Key)) return null;
        var json = await _cache.GetStringAsync(Key);

        if (string.IsNullOrWhiteSpace(json)) return null;
        return JsonSerializer.Deserialize<TriviaData?>(json);
    }
}
