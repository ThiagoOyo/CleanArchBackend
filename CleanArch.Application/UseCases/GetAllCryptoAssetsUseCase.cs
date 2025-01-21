using CleanArch.Domain.Entities;
using CleanArch.Domain.Repositories;

namespace CleanArch.Application.UseCases;

public class GetAllCryptoAssetsUseCase
{
    private readonly ICryptoAssetRepository _cryptoAssetRepository;

    public GetAllCryptoAssetsUseCase(ICryptoAssetRepository cryptoAssetRepository)
    {
        _cryptoAssetRepository = cryptoAssetRepository;
    }

    public async Task<IEnumerable<CryptoAsset?>> ExecuteAsync()
    {
        return await _cryptoAssetRepository.GetAllAsync();
    }
}