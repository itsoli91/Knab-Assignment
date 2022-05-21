using Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository;

namespace Knab.Exchange.Infrastructure.Services;

public interface ICryptoPriceService
{
    Task<CryptoPriceList> GetPrices(string symbol);
}