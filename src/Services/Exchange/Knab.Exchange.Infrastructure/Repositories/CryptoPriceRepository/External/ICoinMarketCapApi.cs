// Refit: The automatic type-safe REST library for .NET Core, Xamarin and .NET.
// Refit turns your REST API into a live interface.

using Refit;

namespace Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository.External;

public interface ICoinMarketCapApi
{
    [Get("/cryptocurrency/quotes/latest")]
    Task<HttpResponseMessage> GetLatestQuotes([Query] string convert, [Query] string symbol);
}