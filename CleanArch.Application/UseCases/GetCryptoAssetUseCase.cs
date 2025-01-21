using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;

namespace CleanArch.Application.UseCases;

public class GetCryptoAssetUseCase
{
    private readonly ICryptoAssetRepository _cryptoAssetRepository;

    public GetCryptoAssetUseCase(ICryptoAssetRepository cryptoAssetRepository)
    {
        _cryptoAssetRepository = cryptoAssetRepository;
    }

    public async Task<CryptoAsset?> ExecuteAsync(string id)
    {
        return await _cryptoAssetRepository.GetByIdAsync(id);
    }
}