namespace Knab.Exchange.Infrastructure.Common.Configurations;

public class AppSettings
{
    public string AppName { get; set; } = null!;
    public int MajorVersion { get; set; }
    public int MinorVersion { get; set; }
   
    public string CoinMarketCapApiBaseUrl { get; set; } = null!;
    public string CoinMarketCapApiKey { get; set; } = null!;
    public string ExchangeRatesApiBaseUrl { get; set; } = null!;
    public string ExchangeRatesApiKey { get; set; } = null!;
}