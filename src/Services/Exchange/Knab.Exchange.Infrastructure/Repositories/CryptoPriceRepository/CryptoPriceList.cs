using Knab.Exchange.Domain.Exchange;

namespace Knab.Exchange.Infrastructure.Repositories.CryptoPriceRepository;

public class CryptoPriceList
{
    public CryptoPriceList(string symbol, double usd, double eur, double brl, double gbp, double aud)
    {
        Symbol = symbol;
        Usd = usd;
        Eur = eur;
        Brl = brl;
        Gbp = gbp;
        Aud = aud;
    }

    public static CryptoPriceList FromExchangeRates(string symbol, double usdPrice, ExchangeRates rates)
    {
        return new CryptoPriceList(symbol.ToUpper(),
            usdPrice,
            usdPrice * rates.Eur,
            usdPrice * rates.Brl,
            usdPrice * rates.Gbp,
            usdPrice * rates.Aud);
    }

    public string Symbol { get; init; }

    public double Usd { get; init; }
    public double Eur { get; init; }
    public double Brl { get; init; }
    public double Gbp { get; init; }
    public double Aud { get; init; }
}