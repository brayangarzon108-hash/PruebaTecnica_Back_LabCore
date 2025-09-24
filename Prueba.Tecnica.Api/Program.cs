using DbContexto;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using sst_core.Core;
using sst_database.sst_database.DbCore;
using System.Text;
using Tenkuscore.Core;



var logger = NLog.LogManager.Setup().LoadConfigurationFromFile().GetCurrentClassLogger();
logger.Error("Inicio la ejecución");

try
{

    // Add services to the container.
    var builder = WebApplication.CreateBuilder(args);

    // Configuración de cookies seguras
    builder.Services.ConfigureApplicationCookie(options =>
    {
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.HttpOnly = true;
        options.Cookie.SameSite = SameSiteMode.Strict;
    });


    builder.Services.AddControllers();

    // Logger File
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    var connectionString = builder.Configuration.GetConnectionString("Database");
    builder.Services.AddDbContext<DatabaseContext>(options => options.UseSqlServer(connectionString));

    #region AddTransient 
    builder.Services.AddTransient<IManagementCore, ManagementCore>();

    builder.Services.AddTransient<ICityCore, CityCore>();
    builder.Services.AddTransient<ICityRepository, CityRepository>();

    builder.Services.AddTransient<IPatientsCore, PatientsCore>();
    builder.Services.AddTransient<IPatientsRepository, PatientsRepository>();

    #endregion

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen();

    // Add CORS support
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("MyAllowAllHeadersPolicy", policy =>
        {
            policy.WithOrigins("*")
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
    });

    builder.Services.AddControllers();


    //Auth
    builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
    {

        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });

    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
    builder.Services.AddSwaggerGen(c =>
    {
        //c.EnableAnnotations();
        c.SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "API Services",
            Version = "v.1.0.10",
            Description = "Web Api",
            TermsOfService = new Uri("https://www.itsense.com.co"),
            Contact = new OpenApiContact
            {
                Name = "Contáctanos - Website",
                Url = new Uri("https://www.itsense.com.co")
            },
            License = new OpenApiLicense
            {
                Name = "Licencia",
                Url = new Uri("https://www.itsense.com.co")
            }
        });
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter a valid token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "Bearer"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
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
                Array.Empty<string>()
            }
        });


    });

    var services = builder.Services;

    var app = builder.Build();

    app.UseCors("MyAllowAllHeadersPolicy");


    app.Use(async (context, next) =>
    {
        context.Response.Headers.Add("X-Frame-Options", "DENY");
        context.Response.Headers.Add("Content-Security-Policy", "frame-ancestors 'none'");
        await next();
    });

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
}
catch (Exception e)
{
    logger.Error(e, "Error [500] Internal Server Error: Se detuvo el sistema por el siguiente error inesperado: " + e.Message);
    throw;
}
finally
{
    NLog.LogManager.Shutdown();
}

