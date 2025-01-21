// CleanArch.Infrastructure/DependencyInjection.cs
using CleanArch.Domain.Repositories;
using CleanArch.Infrastructure.Data;
using CleanArch.Infrastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArch.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlServer(connectionString));
        services.AddScoped<ICryptoAssetRepository, CryptoAssetRepository>();
        return services;
    }
}