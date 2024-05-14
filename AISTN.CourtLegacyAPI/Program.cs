using AISTN.Common.Helper;
using AISTN.Common.Helper.ApiRequestIntercepting;
using AISTN.CourtLegacyAPI.Services;
using AISTN.Data.DataModel;
using AISTN.Data.LogDataModel;
using AISTN.Repository;
using AspNetCoreRateLimit;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

//Rate limiting
builder.Services.AddMemoryCache();
builder.Services.AddSingleton<IProcessingStrategy, AsyncKeyLockProcessingStrategy>();
builder.Services.Configure<IpRateLimitOptions>(configuration.GetSection("IpRateLimiting"));
builder.Services.Configure<IpRateLimitPolicies>(configuration.GetSection("IpRateLimitPolicies"));
builder.Services.Configure<ProcessingSettings>(configuration.GetSection("ApiRequestProcessingSettings"));
builder.Services.AddSingleton<IIpPolicyStore, MemoryCacheIpPolicyStore>();
builder.Services.AddSingleton<IRateLimitCounterStore, MemoryCacheRateLimitCounterStore>();
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// Add services to the container.
builder.Services.AddSingleton<CacheManager>();
builder.Services.AddTransient<LegacyCaseImportService>();
builder.Services.AddTransient<ExceptionLogger>();
builder.Services.AddTransient<ApiRequestLogger>();
builder.Services.AddHttpContextAccessor();

builder.Services.AddDbContext<AistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<AistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
builder.Services.AddDbContext<AistnContextLoggable>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<AistnContextLoggable>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
builder.Services.AddDbContext<LogAistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("LogConnection")), ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<LogAistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("LogConnection")), ServiceLifetime.Transient);


builder.Services.AddControllers(options => options.Filters.Add(typeof(HttpResponseExceptionFilter)))
                .AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseIpRateLimiting();

app.UseMiddleware<InterceptingMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
