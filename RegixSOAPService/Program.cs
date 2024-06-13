using Microsoft.Extensions.DependencyInjection.Extensions;
using SoapCore;

using AISTN.Common.Helper;
using AISTN.Data.DataModel;

using Microsoft.EntityFrameworkCore;
using AISTN.Common.Helper.ApiRequestIntercepting;
using RegixSOAPService;
using NPOI.XSSF.UserModel.Helpers;


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
        listenOptions.UseHttps(certificateFullPath, certificatePass);
    });
});

builder.Services.AddHttpContextAccessor();
builder.Services.AddSoapCore();
builder.Services.AddDbContextFactory<AistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), ServiceLifetime.Transient);
builder.Services.TryAddSingleton<ISPNImplementation>();


var app = builder.Build();

app.UseMiddleware<RegixSOAPService.RegixSoapRequestResponseSniffingMiddleware>();
app.UseRouting();


app.UseEndpoints(endpoints =>
{
    endpoints.UseSoapEndpoint<ISPNImplementation>(opt =>
    {
        
        opt.Path = configuration.GetValue<string>("GeneralServiceOptions:ServicePath");
        opt.SoapSerializer = SoapSerializer.XmlSerializer ;

    });
});


//use this to generate new password if needed
var password = "tralalalala";
var salt = RegixSOAPService.PasswordHelper.GenerateSalt();
var hashedpassword = RegixSOAPService.PasswordHelper.GenerateSaltedHash(password, salt);

Console.WriteLine("password: " + password);
Console.WriteLine("salt: " + salt);
Console.WriteLine("hashed password: " + hashedpassword);


app.Run();

