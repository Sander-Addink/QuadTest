using Quad.TriviaApi.Models;

namespace Quad.TriviaApi.Services;

public interface ITriviaCacheService
{
    Task StoreCacheData(TriviaData session, TimeSpan? absoluteExpiration = null);
    Task<TriviaData?> GetCacheData();
    Task ClearCache();
}
