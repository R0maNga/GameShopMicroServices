using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using BLL.Services;
using BLL.Services.Interfaces;
using BLL.Utils;
using DAL;
using DAL.Contracts.Finders;
using DAL.Contracts.Repositories;
using DAL.Contracts.UnitOfWork;
using DAL.Finders;
using DAL.Repositories;
using DAL.UnitOfWork;
using MainService.AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using Serilog.Exceptions;
using Serilog.Sinks.Elasticsearch;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;
using UserRefreshTokenProfile = BLL.AutoMapper.UserRefreshTokenProfile;

var builder = WebApplication.CreateBuilder(args);
var connection = builder.Configuration["ConnectionStrings:DefaultConnection"];
ConfigureLogging();
builder.Host.UseSerilog();
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();
// Add services to the container.
builder.Services.AddCors();
builder.Services.AddDbContext<MainServiceContext>(options =>
    options.UseSqlServer(connection));



IGetTokenBytes getTokenBytes = new GetTokenBytes(builder.Configuration);
TokenValidationParameters tokenValidation = new TokenValidationParameters
{
    IssuerSigningKey = new SymmetricSecurityKey(getTokenBytes.GetTokeBytes()),
    ValidateLifetime = true,
    ValidateAudience = false,
    ValidateIssuer = false,
    ClockSkew = TimeSpan.Zero
};
builder.Services.AddSingleton(tokenValidation);

builder.Services.AddAuthentication(authOptions =>
    {
        authOptions.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        authOptions.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(jwtOptions =>
    {
        jwtOptions.TokenValidationParameters = tokenValidation;
        jwtOptions.Events = new JwtBearerEvents();
        jwtOptions.Events.OnTokenValidated = async (context) =>
        {
            var ipAdress = context.Request.HttpContext.Connection.RemoteIpAddress.ToString();
            
        };
    });
builder.Services.AddEndpointsApiExplorer();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient(provider => provider.GetRequiredService<MainServiceContext>().UserRefreshTokens);
builder.Services.AddTransient<ITokenService, TokenService>();
builder.Services.AddTransient<ITokenRepository, TokenRepository>();
builder.Services.AddTransient<ITokenFinder, TokenFinder>();
builder.Services.AddTransient<IGetTokenBytes, GetTokenBytes>();
builder.Services.AddTransient(provider => provider.GetRequiredService<MainServiceContext>().UserEntities);

builder.Services.AddTransient<IUserRepository, UserRepository>();
builder.Services.AddTransient<IUserFinder, UserFinder>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IUnitOfWork, UnitOfWork>();



builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddAutoMapper(typeof(UserRefreshTokenProfile), typeof(RefreshTokenProfile), typeof(MainService.AutoMapper.UserRefreshTokenProfile));
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
app.UseAuthentication();
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