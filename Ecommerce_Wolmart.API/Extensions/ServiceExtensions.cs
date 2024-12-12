using Contracts;
using Entities.Identity;
using LoggerService;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using System.Text;

namespace InventrySystem.Extensions
{
    public static class ServiceExtensions
    {
        public static void ConfigureCors(this IServiceCollection services) =>
            services.AddCors(options =>
            {
                options.AddPolicy("CorsPolicy", builder =>
                    builder.AllowAnyOrigin()
                    .AllowAnyMethod()
                    .AllowAnyHeader());
            });

        public static void ConfigureIISIntegration(this IServiceCollection services) =>
            services.Configure<IISOptions>(options =>
            {

            });

        public static void ConfigureSqlContext(this IServiceCollection services, IConfiguration configuration) =>
            services.AddDbContext<RepositoryContext>(opts =>
                opts.UseSqlServer(configuration.GetConnectionString("sqlConnection"), b => b.MigrationsAssembly("InventrySystem")));

        public static void ConfigureRepositoryManager(this IServiceCollection services) =>
        services.AddScoped<IRepositoryManager, RepositoryManager>();

        public static void ConfigureLoggerService(this IServiceCollection services) =>
        services.AddSingleton<ILoggerManager, LoggerManager>();

        public static void ConfigureIdentity(this IServiceCollection services)
        {
            var builder = services.AddIdentity<User, UserRole>(o =>
            {
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = false;
                o.Password.RequireUppercase = false;
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequiredLength = 7;
                o.User.RequireUniqueEmail = true;

                o.Lockout.AllowedForNewUsers = true;
                o.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(1);
                o.Lockout.MaxFailedAccessAttempts = 3;
            })
            .AddEntityFrameworkStores<RepositoryContext>()
            .AddDefaultTokenProviders();
        }

        //public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        //{
        //    var jwtSettings = configuration.GetSection("JwtSettings");

        //    services.AddAuthentication(opt =>
        //    {
        //        opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        //        opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
        //    })
        //    .AddJwtBearer(options =>
        //    {

        //        options.TokenValidationParameters = new TokenValidationParameters
        //        {
        //            ValidateIssuer = true,
        //            ValidateAudience = true,
        //            ValidateLifetime = true,
        //            ValidateIssuerSigningKey = true,

        //            ValidIssuer = jwtSettings["validIssuer"],
        //            ValidAudience = jwtSettings["validAudience"],
        //            //IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("securityKey"))
        //            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
        //        };

        //    });
        //}

        public static void ConfigureJWT(this IServiceCollection services, IConfiguration configuration)
        {
            var jwtSettings = configuration.GetSection("JwtSettings");

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
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings["validIssuer"],
                    ValidAudience = jwtSettings["validAudience"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings["securityKey"]))
                };
                // Thêm tùy chỉnh Cookie-to-Header
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        var accessToken = context.HttpContext.Request.Cookies["accesstoken_admin"] ?? context.HttpContext.Request.Cookies["accesstoken_user"];
                        if (!string.IsNullOrEmpty(accessToken))
                        {
                            context.Token = accessToken;
                        }
                        return Task.CompletedTask;
                    }
                };

            });
        }

        public static void ConfigureSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "Inventory System API",
                    Version = "v1"
                });

                // Thêm cấu hình cho JWT Bearer Token
                //s.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                //{
                //    In = ParameterLocation.Header,
                //    Description = "Please insert JWT with Bearer into field. Example: Bearer {token}",
                //    Name = "Authorization",
                //    Type = SecuritySchemeType.ApiKey,
                //    Scheme = "Bearer"
                //});


                s.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[] {}
                    }
                });
            });
        }

    }
}
