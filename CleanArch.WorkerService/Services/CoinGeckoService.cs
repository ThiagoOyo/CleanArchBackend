using System.Net.Http.Json;
using System.Text.Json;
using CleanArch.Domain.Entities;

namespace CleanArch.WorkerService.Services;

public class CoinGeckoService
{
    private readonly HttpClient _httpClient;

    public CoinGeckoService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add("User-Agent", "CleanArchWorker/1.0");
    }

    public async Task<List<CryptoAsset>> FetchCryptoDataAsync()
    {
        var response = await _httpClient.GetStringAsync("https://api.coingecko.com/api/v3/coins/markets?vs_currency=usd");
        
        var cryptoAssets = JsonSerializer.Deserialize<List<CryptoAsset>>(response, new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        });

        return cryptoAssets ?? [];
    }
}