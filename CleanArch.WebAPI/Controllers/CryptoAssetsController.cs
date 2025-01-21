using CleanArch.Application.UseCases;
using CleanArch.Domain.Entities;
using Microsoft.AspNetCore.Mvc;

namespace CleanArch.WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class CryptoAssetsController(
    GetCryptoAssetUseCase getCryptoAssetUseCase,
    GetAllCryptoAssetsUseCase getAllCryptoAssetsUseCase)
    : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(string id)
    {
        var result = await getCryptoAssetUseCase.ExecuteAsync(id);
        if (result == null)
        {
            return NotFound($"Crypto asset with ID '{id}' not found.");
        }
        return Ok(result);
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var results = await getAllCryptoAssetsUseCase.ExecuteAsync();
        return Ok(results);
    }
}