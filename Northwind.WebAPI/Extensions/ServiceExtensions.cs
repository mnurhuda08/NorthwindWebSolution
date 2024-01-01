using Northwind.Domain.Base;
using Northwind.Persistence.Base;
using Northwind.Persistence.RepositoryContext;
using Northwind.Services;
using Northwind.Services.Abstraction;
using System.Security.Cryptography;
using System.Text;

namespace Northwind.WebAPI.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) => services.AddCors(options =>
        {
            options.AddPolicy("CorsPolicy",builder => 
            builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader()
            .WithExposedHeaders("X-Pagination")
            );
        });

        //Add IIS configure options deploy to IIS
        public static void ConfigureIISIntegration(this IServiceCollection services) => services.Configure<IISOptions>(options => 
        {
        });

        //create a service once per request
        public static void ConfigureLoggerService(this IServiceCollection services) => services.AddScoped<ILoggerManager, LoggerManager>();

        public static void ConfigureRepositoryManager(this IServiceCollection services) => services.AddScoped<IRepositoryManager, RepositoryManager>();
        
        public static void ConfigureDbContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddSingleton(new AdoDbContext(configuration.GetConnectionString("development")));

       /* public static void ConfigureUtilityService(this IServiceCollection services) => services.AddScoped<IUtilityService, UtilityService>();

        public static void ConfigureServiceManager(this IServiceCollection services) => services.AddScoped<IServiceManager, ServiceManager>();

        public static void ConfigureAuthenticationManager(this IServiceCollection services) => services.AddScoped<IAuthenticationManager, AuthenticationManager>();*/

      /*  public static void ConfigureJWT(this IServiceCollection services,IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");
            var secretKey = configuration.GetSection("SecretKey");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKery = true,
                        ValidIssuer = jwtSettings.GetSection("ValidIssuer").Value,
                        ValidAudience = jwtSettings.GetSection("ValidAudience").Value,
                        IssuerSigninKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey.Value))
                    };
                });
        }*/
    }
}
