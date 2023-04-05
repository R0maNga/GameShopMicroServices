using System.Reflection;
using BLL.Services;
using BLL.Services.Interfaces;
using DAL;
using DAL.Contracts.IFinders;
using DAL.Contracts.IRepositories;
using DAL.Contracts.IUnitOfWork;
using DAL.Finders;
using DAL.Repositories;
using DAL.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using OrderService;
using OrderService.Mapper;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;


var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
ConfigureLogging();
builder.Host.UseSerilog();
// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddCors();
builder.Services.AddDbContext<OrderServiceContext>(options =>
    options.UseSqlServer(connection));

builder.Services.AddTransient(provider => provider.GetRequiredService<OrderServiceContext>().Orders);
builder.Services.AddTransient<IOrderRepository, OrderRepository>();
builder.Services.AddTransient<IOrderFinder, OrderFinder>();
builder.Services.AddTransient<IOrderService, BLL.Services.OrderService>();

builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();
builder.Services.AddHostedService<BackGroundConsumerForOrder>();
builder.Services.AddHostedService<BackGroundConsumerForValidationOrders>();
builder.Services.AddSingleton<IOrderHostedService, OrderHostedService>();

builder.Services.AddAutoMapper(typeof(OrderProfile));

builder.Services.AddAutoMapper(typeof(BLL.AutoMapper.OrderProfile));


var app = builder.Build();

app.UseCors(x => x
    .SetIsOriginAllowed(origin => true)
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());

// Configure the HTTP request pipeline.
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
