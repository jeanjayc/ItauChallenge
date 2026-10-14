using ItauChallenge.QuoteConsumer;
using ItauChallenge.Application.Services;
using ItauChallenge.Infra.Data;
using ItauChallenge.Messaging;
using Microsoft.EntityFrameworkCore;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddDbContext<ItauChallengeContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddScoped<IQuoteService, QuoteService>();
builder.Services.Configure<KafkaOptions>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton<IKafkaConsumer, KafkaConsumer>();
builder.Services.AddHostedService<Worker>();

var host = builder.Build();
host.Run();
