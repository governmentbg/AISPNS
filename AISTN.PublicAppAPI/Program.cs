using AISTN.Common;
using AISTN.Data.DataModel;
using AISTN.Data.LogDataModel;
using AISTN.PublicAppAPI.Helper;
using AISTN.Repository;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(typeof(PublicAppAutoMapperProfile));
builder.Services.AddDbContext<AistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), contextLifetime: ServiceLifetime.Transient);
builder.Services.AddDbContext<AistnContextLoggable>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), contextLifetime: ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<LogAistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("LogConnection")));
builder.Services.AddHttpContextAccessor();
builder.Services.AddHttpClient();

// Allow cors all origin requests
var MyAllowAllOrigins = "_myAllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin();
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.WithExposedHeaders("Content-Disposition");
                      });
});

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseCors(MyAllowAllOrigins);


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
