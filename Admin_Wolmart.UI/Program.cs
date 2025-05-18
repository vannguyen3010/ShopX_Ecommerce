using Admin_Wolmart.UI;
using Admin_Wolmart.UI.Helpers;
using Admin_Wolmart.UI.Services;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.CookiePolicy;
using Microsoft.Extensions.Options;
using System.Net;

var builder = WebApplication.CreateBuilder(args);

// Cấu hình AppSettings
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

//var handler = new HttpClientHandler
//{
//    UseCookies = true,
//    CookieContainer = new CookieContainer()
//};

//builder.Services.AddSingleton(handler); // Add vào DI

//builder.Services.AddScoped(sp =>
//{
//    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
//    var handler = new HttpClientHandler
//    {
//        UseCookies = true,
//        CookieContainer = new CookieContainer()
//    };

//    return new HttpClient(handler)
//    {
//        BaseAddress = new Uri(apiSettings.BaseUrl)
//    };
//});

// Đăng ký HttpClientHandler và HttpClient với vòng đời Scoped 
//builder.Services.AddScoped<HttpClientHandler>(sp =>
//{
//    return new HttpClientHandler
//    {
//        UseCookies = true,
//        CookieContainer = new CookieContainer()
//    };
//});

//builder.Services.AddScoped(sp =>
//{
//    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
//    var handler = sp.GetRequiredService<HttpClientHandler>();

//    return new HttpClient(handler)
//    {
//        BaseAddress = new Uri(apiSettings.BaseUrl)
//    };
//});

builder.Services.AddSingleton(new CookieContainer());

builder.Services.AddScoped<HttpClientHandler>(sp =>
{
    var container = sp.GetRequiredService<CookieContainer>();

    return new HttpClientHandler
    {
        UseCookies = true,
        CookieContainer = container
    };
});

builder.Services.AddScoped(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    var handler = sp.GetRequiredService<HttpClientHandler>();

    return new HttpClient(handler)
    {
        BaseAddress = new Uri(apiSettings.BaseUrl)
    };
});


builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.Cookie.Name = "accesstoken_admin";
        options.LoginPath = "/account/login"; // Đường dẫn khi chưa đăng nhập
        options.AccessDeniedPath = "/account/access-denied"; // Đường dẫn khi bị từ chối quyền truy cập
        options.Cookie.HttpOnly = true;
        options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
        options.Cookie.SameSite = SameSiteMode.None;
    });

builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<AccountServices>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<CateProductServices>();
builder.Services.AddScoped<CateIntroduceServices>();
builder.Services.AddScoped<IntroduceServices>();
builder.Services.AddScoped<ContactServices>();
builder.Services.AddScoped<BannerServices>();
builder.Services.AddScoped<BannerProductServices>();
builder.Services.AddScoped<PaymentServices>();
builder.Services.AddScoped<ProfileServices>();
builder.Services.AddScoped<ShippingCostServices>();

builder.Services.AddScoped<CustomAuthenticationStateProvider>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddRazorComponents().AddInteractiveServerComponents();
builder.Services.AddBlazoredToast();

builder.Services.AddAuthorizationCore();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// // Middleware
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();


// Cấu hình CookiePolicy
app.UseCookiePolicy(new CookiePolicyOptions
{
    MinimumSameSitePolicy = SameSiteMode.None,
    HttpOnly = HttpOnlyPolicy.Always,
    Secure = CookieSecurePolicy.Always
});

app.UseAuthentication();
app.UseAuthorization();

app.Run();
