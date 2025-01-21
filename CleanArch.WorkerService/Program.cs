using CleanArch.Infrastructure;
using CleanArch.WorkerService.Jobs;
using CleanArch.WorkerService.Services;
using Hangfire;
using Hangfire.SqlServer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = Host.CreateDefaultBuilder(args);

builder.ConfigureServices((context, services) =>
{
    var connectionString = Environment.GetEnvironmentVariable("CONNECTION_STRING") 
                           ?? context.Configuration.GetConnectionString("DefaultConnection")
                           ?? throw new InvalidOperationException("Connection string is not configured in environment variables or appsettings.json.");

    services.AddInfrastructure(connectionString);
    services.AddHttpClient<CoinGeckoService>();
    services.AddTransient<CryptoDataUpsertJob>();

    services.AddHangfire(config =>
        config.UseSqlServerStorage(connectionString));
    services.AddHangfireServer();
    
    services.AddSingleton<IRecurringJobManager, RecurringJobManager>();
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();
    var job = scope.ServiceProvider.GetRequiredService<CryptoDataUpsertJob>();

    recurringJobManager.AddOrUpdate(
        "CryptoDataUpsertJob",
        () => job.ExecuteAsync(),
        Cron.Hourly
    );

    await job.ExecuteAsync();
}


app.Run();