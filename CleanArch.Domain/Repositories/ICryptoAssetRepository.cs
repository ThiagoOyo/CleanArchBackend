using CleanArch.Domain.Entities;

namespace CleanArch.Domain.Repositories;

public interface ICryptoAssetRepository
{
    Task<CryptoAsset?> GetByIdAsync(string id);
    Task<IEnumerable<CryptoAsset?>> GetAllAsync();
    Task UpsertAsync(IEnumerable<CryptoAsset?> assets);
}