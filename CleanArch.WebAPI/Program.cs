using CleanArch.Application.UseCases;
using CleanArch.Infrastructure;
using CleanArch.WebAPI.Middleware;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(
    builder.Configuration.GetConnectionString("DefaultConnection") ??
        throw new InvalidOperationException("Connection string 'DefaultConnection' is not configured.")
);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin", policy =>
    {
        policy.WithOrigins("http://localhost:5173") 
            .AllowAnyHeader()                    
            .AllowAnyMethod();                   
    });
});

builder.Services.AddControllers();
builder.Services.AddScoped<GetCryptoAssetUseCase>();
builder.Services.AddScoped<GetAllCryptoAssetsUseCase>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("AllowSpecificOrigin");

app.UseMiddleware<ApiKeyMiddleware>();


app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();