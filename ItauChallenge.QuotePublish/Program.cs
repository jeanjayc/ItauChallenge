using ItauChallenge.Application.Services;
using ItauChallenge.Infra.Data;
using ItauChallenge.QuotePublish;
using ItauChallenge.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

// Register DbContext (reads connection string "DefaultConnection" from appsettings or environment)
builder.Services.AddDbContext<ItauChallengeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

// Register application services
builder.Services.AddScoped<IQuoteService, QuoteService>();

// Bind Kafka configuration and register producer
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton<IKafkaProducer, KafkaProducer>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
