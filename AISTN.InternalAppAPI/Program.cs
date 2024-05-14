using AISTN.Common;
using AISTN.Common.Helper;
using AISTN.Data.DataModel;
using AISTN.Data.LogDataModel;
using AISTN.InternalAppAPI.Helper;
using AISTN.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Reflection;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
ConfigurationManager configuration = builder.Configuration;

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();

// To use swagger login
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc("v1", new OpenApiInfo { Title = "AISTN API", Version = "v1" });
    option.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = "Please enter a valid token",
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        Scheme = "Bearer"
    });
    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id="Bearer"
                }
            },
            new string[]{}
        }
    });
});

// Adding Authentication
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;

})
// Adding Jwt Bearer
.AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateLifetime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = configuration["JWT:ValidAudience"],
        ValidIssuer = configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]))
    };
});

builder.Services.AddAutoMapper(typeof(InternalAppAutoMapperProfile));
builder.Services.AddDbContext<AistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), contextLifetime: ServiceLifetime.Transient);
builder.Services.AddDbContext<AistnContextLoggable>(x => x.UseSqlServer(configuration.GetConnectionString("DefaultConnection")), contextLifetime: ServiceLifetime.Transient);
builder.Services.AddDbContextFactory<LogAistnContext>(x => x.UseSqlServer(configuration.GetConnectionString("LogConnection")));
builder.Services.AddHttpContextAccessor();

// Allow cors all origin requests
var MyAllowAllOrigins = "_myAllowAllOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowAllOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().WithOrigins(configuration.GetSection("CorsOriginAllowed").Get<string[]>());
                          policy.AllowAnyHeader();
                          policy.AllowAnyMethod();
                          policy.WithExposedHeaders("Content-Disposition");
                          policy.AllowCredentials();
                      });
});

// read LDAP Configuration  
builder.Services.Configure<LdapConfig>(builder.Configuration.GetSection("LdapSettings"));

var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services, Assembly.GetExecutingAssembly());

var app = builder.Build();

app.UseCors(MyAllowAllOrigins);

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.ConfigObject.AdditionalItems.Add("persistAuthorization", "true");
    });
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
