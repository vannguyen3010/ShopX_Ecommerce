using Blazored.LocalStorage;
using Blazored.Toast;
using Ecommerce.UI;
using Ecommerce.UI.Components;
using Ecommerce.UI.Components.Pages.OtherPage;
using Ecommerce.UI.Services;
using Ecommerce.UI.ViewModels;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

//builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7105/") });

// Đăng ký cấu hình
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Khởi tạo HttpClient với URL từ appsettings.json
builder.Services.AddScoped(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    return new HttpClient { BaseAddress = new Uri(apiSettings.BaseUrl) };
});

builder.Services.AddScoped<AccountServices>();
builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<HomeServices>();
builder.Services.AddScoped<BaseServices>();
builder.Services.AddScoped<IntroduceServices>();
builder.Services.AddScoped<ContactServices>();
builder.Services.AddScoped<CartServices>();
builder.Services.AddScoped<AddressServices>();
builder.Services.AddScoped<ShippingCost>();
builder.Services.AddScoped<OrderServices>();

builder.Services.AddSingleton<AuthState>();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthStateProvider>();
builder.Services.AddAuthorizationCore();

builder.Services.AddBlazoredLocalStorage();
builder.Services.AddBlazoredToast();
builder.Services.AddOptions();
builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}
else
{
    app.UseStatusCodePagesWithReExecute("/error/{0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
