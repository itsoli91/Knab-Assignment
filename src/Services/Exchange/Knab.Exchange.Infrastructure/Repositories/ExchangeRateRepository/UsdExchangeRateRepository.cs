using Knab.Exchange.Domain.Exchange;
using Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository.External;

namespace Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository;

public class UsdExchangeRateRepository : IExchangeRateRepository
{
    private readonly IMemoryCache _cache;
    private readonly IExchangeRatesApi _exchangeRatesApi;

    public UsdExchangeRateRepository(IMemoryCache cache, IExchangeRatesApi exchangeRatesApi)
    {
        _cache = cache;
        _exchangeRatesApi = exchangeRatesApi;
    }

    public async Task<ExchangeRates> GetExchangeRates()
    {
        const string cacheKey = "ExchangeRates";

        if (!_cache.TryGetValue(cacheKey, out ExchangeRates rates))
        {
            rates = await GetFromExternalProvider();

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(30));

            _cache.Set(cacheKey, rates, cacheEntryOptions);
        }

        return rates;
    }

    private async Task<ExchangeRates> GetFromExternalProvider()
    {
        var rates = await _exchangeRatesApi
            .GetLatest(new[] { "EUR", "BRL", "GBP", "AUD" }, "USD");

        return rates == null
            ? ExchangeRates.Empty
            : new ExchangeRates(1, rates.Rates.EUR, rates.Rates.BRL, rates.Rates.GBP, rates.Rates.AUD);
    }
}