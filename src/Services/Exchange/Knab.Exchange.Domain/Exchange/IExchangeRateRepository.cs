namespace Knab.Exchange.Domain.Exchange;

public interface IExchangeRateRepository
{
    Task<ExchangeRates> GetExchangeRates();
}