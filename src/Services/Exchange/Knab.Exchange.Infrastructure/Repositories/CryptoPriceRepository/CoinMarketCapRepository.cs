using Knab.Exchange.Domain.Crypto;
using Knab.Exchange.Infrastructure.Common.Configurations;
using Knab.Exchange.Infrastructure.Common.Exceptions;
using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository.External;
using Refit;

namespace Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository;

public class CoinMarketCapRepository : ICryptoPriceRepository
{
    private readonly IMemoryCache _cache;
    private readonly ICoinMarketCapApi _coinMarketCapApi;

    public CoinMarketCapRepository(IMemoryCache cache, ICoinMarketCapApi coinMarketCapApi)
    {
        _cache = cache;
        _coinMarketCapApi = coinMarketCapApi;
    }

    public async Task<double> GetCryptoPriceInUsd(string symbol)
    {
        if (string.IsNullOrWhiteSpace(symbol))
            throw new GeneralApplicationException("Invalid Symbol.");

        symbol = symbol.ToUpper();
        var cacheKey = $"CryptoRate-{symbol}";

        if (!_cache.TryGetValue(cacheKey, out double rate))
        {
            rate = await GetFromExternalProvider(symbol);

            var cacheEntryOptions = new MemoryCacheEntryOptions()
                .SetSlidingExpiration(TimeSpan.FromMinutes(1));

            _cache.Set(cacheKey, rate, cacheEntryOptions);
        }

        return rate;
    }

    private async Task<double> GetFromExternalProvider(string symbol)
    {
        var response = await _coinMarketCapApi.GetLatestQuotes("USD", symbol);


        if (!response.IsSuccessStatusCode)
        {
            if (response.StatusCode == HttpStatusCode.BadRequest)
                throw new GeneralApplicationException("Invalid Symbol.");

            return 0;
        }

        var responseText = await response.Content.ReadAsStringAsync();
        var price = Regex.Match(responseText, "\"price\":[0-9]+(\\.[0-9]*)?").Value;
        if (string.IsNullOrWhiteSpace(price))
            return 0;

        return double.Parse(price.Split(":")[1].Trim());
    }
}