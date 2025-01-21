// CleanArch.Domain/Entities/CryptoAsset.cs

using System.Text.Json.Serialization;

namespace CleanArch.Domain.Entities;

public class CryptoAsset
{
    public string Id { get; init; } = string.Empty;
    public string Symbol { get; set; } = string.Empty;
    public string Name { get; set; } = string.Empty;
    [JsonPropertyName("current_price")]
    public decimal CurrentPrice { get; set; }
    [JsonPropertyName("market_cap")]
    public long MarketCap { get; set; }
    [JsonPropertyName("last_updated")]
    public DateTime LastUpdated { get; set; }
}