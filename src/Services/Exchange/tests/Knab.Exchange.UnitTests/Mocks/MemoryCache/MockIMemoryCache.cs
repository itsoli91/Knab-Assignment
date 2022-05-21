using Microsoft.Extensions.Caching.Memory;

namespace Knab.Exchange.UnitTests.Mocks.MemoryCache
{
    /// <summary>
    /// MemoryCache Mock.
    /// This is a concrete implementation of IMemoryCache.
    /// This class can not be mocked using 'Moq' library because the code uses extension methods which extend the actual IMemoryCache interface
    /// </summary>
    public class MockMemoryCache : IMemoryCache
    {
        private readonly bool _isMiss;
        private readonly object _returnValue;

        private MockMemoryCache(bool isMiss, object returnValue)
        {
            _isMiss = isMiss;
            _returnValue = returnValue;
        }

        public static MockMemoryCache CacheMissMock()
        {
            return new MockMemoryCache(true, null);
        }

        public static MockMemoryCache CacheHitMock(object returnValue)
        {
            return new MockMemoryCache(false, returnValue);
        }

        public void Dispose()
        {
        }

        public ICacheEntry CreateEntry(object key)
        {
            return new MockCacheEntry();
        }

        public void Remove(object key)
        {
            
        }

        public bool TryGetValue(object key, out object value)
        {
            value = _returnValue;

            return !_isMiss;
        }

        //Extension method mock
        public double Set()
        {
            return 0;
        }
    }
}