using System.Text.Json.Serialization;

namespace Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository.External;

public class Rates
{
    public Rates()
    {
    }

    public Rates(double eur, double brl, double gbp, double aud)
    {
        EUR = eur;
        BRL = brl;
        GBP = gbp;
        AUD = aud;
    }
    [JsonPropertyName("EUR")] public double EUR { get; set; }

    [JsonPropertyName("BRL")] public double BRL { get; set; }

    [JsonPropertyName("GBP")] public double GBP { get; set; }

    [JsonPropertyName("AUD")] public double AUD { get; set; }
}