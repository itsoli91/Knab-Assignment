using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Primitives;

namespace Knab.Exchange.UnitTests.Mocks.MemoryCache;

/// <summary>
/// MockCacheEntry Mock.
/// This is a concrete implementation of ICacheEntry.
/// This class can not be mocked using 'Moq' library because the code uses extension methods which extend the actual ICacheEntry interface
/// </summary>
public class MockCacheEntry : ICacheEntry
{
    public void Dispose()
    {
    }

    //Extension method mock
    public void SetOoptions(MemoryCacheEntryOptions options)
    {

    }

    public DateTimeOffset? AbsoluteExpiration { get; set; }
    public TimeSpan? AbsoluteExpirationRelativeToNow { get; set; }
    public IList<IChangeToken> ExpirationTokens { get; }
    public object Key { get; }
    public IList<PostEvictionCallbackRegistration> PostEvictionCallbacks { get; }
    public CacheItemPriority Priority { get; set; }
    public long? Size { get; set; }
    public TimeSpan? SlidingExpiration { get; set; }
    public object Value { get; set; }
}