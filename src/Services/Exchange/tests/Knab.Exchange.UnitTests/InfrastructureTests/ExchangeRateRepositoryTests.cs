using Knab.Exchange.Domain.Exchange;
using Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository;
using Knab.Exchange.UnitTests.Mocks;
using Knab.Exchange.UnitTests.Mocks.MemoryCache;

namespace Knab.Exchange.UnitTests.InfrastructureTests
{
    public class ExchangeRateRepositoryTests
    {
        [Fact]
        public async Task GetExchangeRates_CacheHit_ReturnsCachedList()
        {
            var cachedRates = new ExchangeRates(1, 1.1, 1.2, 1.3, 1.4);
            var mockHttpClientWrapper = MockHttpRequests.ExchangeRatesApiEmptyMock();
            var mockCacheHit = MockMemoryCache.CacheHitMock(cachedRates);

            var provider = new UsdExchangeRateRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetExchangeRates();

            Assert.Equal(cachedRates.Usd, result.Usd);
            Assert.Equal(cachedRates.Eur, result.Eur);
            Assert.Equal(cachedRates.Brl, result.Brl);
            Assert.Equal(cachedRates.Gbp, result.Gbp);
            Assert.Equal(cachedRates.Aud, result.Aud);
        }

        [Fact]
        public async Task GetExchangeRates_CacheMiss_ReturnsApiPrices()
        {
            var apiRates = new ExchangeRates(1, 1.1, 1.2, 1.3, 1.4);
            var mockHttpClientWrapper = MockHttpRequests.ExchangeRatesApiOkMock(apiRates);
            var mockCacheHit = MockMemoryCache.CacheMissMock();

            var provider = new UsdExchangeRateRepository(mockCacheHit, mockHttpClientWrapper.Object);
            var result = await provider.GetExchangeRates();

            Assert.Equal(apiRates.Usd, result.Usd);
            Assert.Equal(apiRates.Eur, result.Eur);
            Assert.Equal(apiRates.Brl, result.Brl);
            Assert.Equal(apiRates.Gbp, result.Gbp);
            Assert.Equal(apiRates.Aud, result.Aud);
        }
    }
}