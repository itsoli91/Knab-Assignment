using System.Text.Json.Serialization;

namespace Knab.Exchange.Infrastructure.Repositories.ExchangeRateRepository.External;

public class RatesDto
{
    public RatesDto()
    {
    }

    public RatesDto(bool success, int timestamp, string @base, string date, Rates rates)
    {
        Success = success;
        Timestamp = timestamp;
        Base = @base;
        Date = date;
        Rates = rates;
    }
    [JsonPropertyName("success")] public bool Success { get; set; }

    [JsonPropertyName("timestamp")] public int Timestamp { get; set; }

    [JsonPropertyName("base")] public string Base { get; set; } = null!;

    [JsonPropertyName("date")] public string Date { get; set; } = null!;

    [JsonPropertyName("rates")] public Rates Rates { get; set; } = null!;
}