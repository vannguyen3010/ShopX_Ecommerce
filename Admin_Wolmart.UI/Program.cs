using Admin_Wolmart.UI;
using Admin_Wolmart.UI.Helpers;
using Admin_Wolmart.UI.Services;
using Blazored.LocalStorage;
using Blazored.Toast;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

builder.Services.AddScoped(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    return new HttpClient { BaseAddress = new Uri(apiSettings.BaseUrl) };
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


builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

builder.Services.AddScoped<LocalStorageService>();
builder.Services.AddAuthorizationCore();
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
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

app.Run();
