using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using CleanArch.WorkerService.Services;

namespace CleanArch.WorkerService.Jobs;

public class CryptoDataUpsertJob(CoinGeckoService coinGeckoService, ICryptoAssetRepository repository)
{
    public async Task ExecuteAsync()
    {
        var cryptoData = await coinGeckoService.FetchCryptoDataAsync();
        await repository.UpsertAsync(cryptoData);
    }
}