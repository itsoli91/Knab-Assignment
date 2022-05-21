// Refit: The automatic type-safe REST library for .NET Core, Xamarin and .NET.
// Refit turns your REST API into a live interface.

using Refit;

namespace Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository.External;

public interface IExchangeRatesApi
{
    [Get("/exchangerates_data/latest")]
    Task<RatesDto?> GetLatest(
        [Query(CollectionFormat.Csv)] string[] symbols, [Query] string @base);
}