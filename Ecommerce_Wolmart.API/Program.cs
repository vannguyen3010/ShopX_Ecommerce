using Contracts;
using Ecommerce_Wolmart.API.JwtFeatures;
using EmailService;
using InventrySystem.Extensions;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using NLog;
using Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

LogManager.Setup().LoadConfigurationFromFile(string.Concat(Directory.GetCurrentDirectory(), "/nlog.config"));

// Add services to the container.
builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.ConfigureRepositoryManager();
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddScoped<JwtHandler>();
builder.Services.ConfigureLoggerService();
builder.Services.ConfigureSwagger();


builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.Configure<DataProtectionTokenProviderOptions>(opt =>
    opt.TokenLifespan = TimeSpan.FromHours(2));

var emailConfig = builder.Configuration.GetSection("EmailConfiguration").Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);

builder.Services.AddScoped<IEmailSender, EmailSender>();
builder.Services.AddScoped<IBannerRepository, BannerRepository>();

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

builder.Services.AddMemoryCache();

builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles; // Prevents cycles
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
    });

// Đăng ký IHttpContextAccessor
builder.Services.AddHttpContextAccessor(); // Khi link vào ảnh thì sẽ tạo ra url ảnh https://localhost:1234/images/image.png

var app = builder.Build();

app.UseHttpsRedirection();

app.UseCors("CorsPolicy");


app.UseSwagger();

app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Inventory System API v1");
    s.DocExpansion(Swashbuckle.AspNetCore.SwaggerUI.DocExpansion.None);
});
app.UseStaticFiles(new StaticFileOptions
{
    FileProvider = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "Img_Repository")),
    RequestPath = "/Img_Repository"
    // https://localhost:1234/Images
});

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
