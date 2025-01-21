// CleanArch.Infrastructure/Repositories/CryptoAssetRepository.cs
using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;
using CleanArch.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace CleanArch.Infrastructure.Repositories;

public class CryptoAssetRepository(AppDbContext dbContext) : ICryptoAssetRepository
{
    public async Task<IEnumerable<CryptoAsset?>> GetAllAsync()
    {
        return await dbContext.CryptoAssets.ToListAsync();
    }

    public async Task<CryptoAsset?> GetByIdAsync(string id)
    {
        return await dbContext.CryptoAssets.FindAsync(id);
    }

    public async Task UpsertAsync(IEnumerable<CryptoAsset?> assets)
    {
        foreach (var asset in assets)
        {
            if (asset == null) continue;

            var existing = await dbContext.CryptoAssets.FindAsync(asset.Id);
            if (existing == null)
            {
                dbContext.CryptoAssets.Add(asset);
                continue; 
            }

            existing.Symbol = asset.Symbol;
            existing.Name = asset.Name;
            existing.CurrentPrice = asset.CurrentPrice;
            existing.MarketCap = asset.MarketCap;
            existing.LastUpdated = asset.LastUpdated;
        }

        await dbContext.SaveChangesAsync();
    }

}