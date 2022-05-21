using Knab.Exchange.Domain.Crypto;
using Knab.Exchange.Infrastructure.Common.Exceptions;
using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository;
using Knab.Exchange.UnitTests.Mocks;
using Knab.Exchange.UnitTests.Mocks.MemoryCache;

namespace Knab.Exchange.UnitTests.InfrastructureTests
{
    public class CoinMarketCapProviderTests
    {
        [Fact]
        public async Task GetCryptoPriceInUsd_InvalidSymbol_ThrowsException()
        {
            var mockHttpClientWrapper = MockHttpRequests.CoinMarketCapApiBadRequestMock();
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);


            await Assert.ThrowsAsync<GeneralApplicationException>(async () =>
                await provider.GetCryptoPriceInUsd("NOSYMBOL"));
        }

        [Fact]
        public async Task GetCryptoPriceInUsd_NullSymbol_ThrowsException()
        {
            var mockHttpClientWrapper = MockHttpRequests.CoinMarketCapApiBadRequestMock();
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);


            await Assert.ThrowsAsync<GeneralApplicationException>(async () =>
                await provider.GetCryptoPriceInUsd(string.Empty));
        }

        [Fact]
        public async Task GetCryptoPriceInUsd_CacheHit_ReturnsCachedPrice()
        {
            var cachedValue = 1000.0;
            var mockHttpClientWrapper = MockHttpRequests.CoinMarketCapOkMock("ETH", 200.0);
            var mockCacheHit = MockMemoryCache.CacheHitMock(cachedValue);

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetCryptoPriceInUsd("ETH");

            Assert.Equal(cachedValue, result);
        }

        [Fact]
        public async Task GetCryptoPriceInUsd_CacheMiss_ReturnsApiPrice()
        {
            var apiValue = 1000.0;
            var mockHttpClientWrapper = MockHttpRequests.CoinMarketCapOkMock("ETH", apiValue);
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new CoinMarketCapRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetCryptoPriceInUsd("ETH");

            Assert.Equal(apiValue, result);
        }
    }
}