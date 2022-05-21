namespace Knab.Exchange.Domain.Crypto;

public interface ICryptoPriceRepository
{
    Task<double> GetCryptoPriceInUsd(string symbol);
}