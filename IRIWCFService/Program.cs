using AISTN.Common.Helper.ApiRequestIntercepting;
using AISTN.Data.DataModel;
using IRIWCFService.IRIService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using SoapCore;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;


builder.WebHost.ConfigureKestrel(serverOptions =>
{

    var certificateFullPath = Path.Combine(Directory.GetCurrentDirectory(), configuration.GetValue<string>("GeneralServiceOptions:CertificatePath"));
    var certificatePass = configuration.GetValue<string>("GeneralServiceOptions:CertificatePassword");
    var hostIP = configuration.GetValue<string>("GeneralServiceOptions:HostIPAddress");
    var port = configuration.GetValue<int>("GeneralServiceOptions:Port");

    serverOptions.Listen(System.Net.IPAddress.Parse(hostIP), port, listenOptions =>
    {
        listenOptions.UseHttps(certificateFullPath , certificatePass  );
    });
});



// Configure your services here as before
builder.Services.Configure<SimpleSearchSettings>(options =>
{
    configuration.GetSection("SimpleSearchSettings").Bind(options);
    options.MaxResultHardLimit = options.MaxResultHardLimit == 0 ? 25 : options.MaxResultHardLimit;
    options.MinimalNameSearchStringLength = options.MinimalNameSearchStringLength == 0 ? 4 : options.MinimalNameSearchStringLength;
    options.MinimalBulstatSearchStringLength = options.MinimalBulstatSearchStringLength == 0 ? 4 : options.MinimalBulstatSearchStringLength;
});

builder.Services.AddSoapCore();
builder.Services.AddDbContextFactory<AistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
builder.Services.TryAddSingleton<IRIQueryServiceImplementation>();

var app = builder.Build();

// Middleware and endpoint configuration as before
app.UseMiddleware<RequestResponseSniffingMiddleware>();
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<IRIQueryServiceImplementation>(opt =>
    {
        opt.Path = configuration.GetValue<string>("GeneralServiceOptions:ServicePath");
        opt.SoapSerializer = SoapSerializer.XmlSerializer;
    });
});

app.Run();
