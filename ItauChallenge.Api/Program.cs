using ItauChallenge.Application.Services;
using ItauChallenge.Infra.Data;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddOpenApi();

// Add services to the container.
builder.Services.AddControllers();
// Registrar DbContext (configure connection string "DefaultConnection" em appsettings.json)
builder.Services.AddDbContext<ItauChallengeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Registrar services da camada Application
builder.Services.AddScoped<IInvestorService, InvestorService>();
builder.Services.AddScoped<IFinancialAssetService, FinancialAssetService>();
builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.AddScoped<IPositionService, PositionService>();
builder.Services.AddScoped<IOperationService, OperationService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();

    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
