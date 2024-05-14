using AISTN.CommercialRegIntegrator;
using AISTN.CommercialRegIntegrator.Services;
using AISTN.Data.DataModel;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = Host.CreateApplicationBuilder(args);
ConfigurationManager configuration = builder.Configuration;

Log.Logger = new LoggerConfiguration()
    .ReadFrom.Configuration(configuration)
    .CreateLogger();

builder.Logging.ClearProviders(); // Clear default providers
builder.Logging.AddSerilog();

Log.Information("Starting UP");

builder.Services.AddTransient<ICommercialRegisterImporterService, CommercialRegisterImporterService>();
builder.Services.AddDbContextFactory<AistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
builder.Services.Configure<FolderSettings>(configuration.GetSection("Folders"));
builder.Services.AddHostedService<Worker>();


var host = builder.Build();
host.Run();
