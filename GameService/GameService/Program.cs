using System.Reflection;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL;
using DAL.Contracts.IFinder;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Finders;
using DAL.Repositories;
using DAL.UnitOfWork;
using GameService.AutoMapper;
using Microsoft.EntityFrameworkCore;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
ConfigureLogging();
builder.Host.UseSerilog();

builder.Services.AddDbContext<GameStorageContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddAutoMapper(typeof(GameStorageProfile));

builder.Services.AddAutoMapper(typeof(BLL.AutoMapper.GameStorageProfile));

builder.Services.AddTransient(provider => provider.GetRequiredService<GameStorageContext>().GameStorages);
builder.Services.AddTransient<IGameStorageRepository, GameStorageRepository>();
builder.Services.AddTransient<IGameStorageFinder, GameStorageFinder>();
builder.Services.AddTransient<IGameStorageService, GameStorageService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddTransient<IMessageProducer, MessageProducer>();

builder.Services.AddHostedService<BackgroundConsumer>();
builder.Services.AddHostedService<BackgroundConsumerForGameCheck>();
builder.Services.AddSingleton<IGamesHostedService, GamesHostedService>();

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    app.UseExceptionHandler("/Home/Error");
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

void ConfigureLogging()
{
    var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
    var configuration = new ConfigurationBuilder()
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .AddJsonFile(
            $"appsettings.{Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT")}.json",
            optional: true)
        .Build();
     
    Log.Logger = new LoggerConfiguration()
        .Enrich.FromLogContext()
        .Enrich.WithExceptionDetails()
        .WriteTo.Debug()
        .WriteTo.Console()
        .WriteTo.Elasticsearch(ConfigureElasticSink(configuration, environment))
        .Enrich.WithProperty("Environment", environment)
        .ReadFrom.Configuration(configuration)
        .CreateLogger();
}

ElasticsearchSinkOptions ConfigureElasticSink(IConfigurationRoot configuration, string environment)
{
    return new ElasticsearchSinkOptions(new Uri(configuration["ElasticConfiguration:Uri"]))
    {
        AutoRegisterTemplate = true,
        IndexFormat = $"{Assembly.GetExecutingAssembly().GetName().Name.ToLower().Replace(".", "-")}-{environment?.ToLower().Replace(".", "-")}-{DateTime.UtcNow:yyyy-MM}"
    };
}
