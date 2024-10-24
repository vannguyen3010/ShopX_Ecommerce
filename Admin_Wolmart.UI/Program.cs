using Admin_Wolmart.UI;
using Admin_Wolmart.UI.Services;
using Blazored.LocalStorage;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

// Đăng ký cấu hình
builder.Services.Configure<ApiSettings>(builder.Configuration.GetSection("ApiSettings"));

// Khởi tạo HttpClient với URL từ appsettings.json
builder.Services.AddScoped(sp =>
{
    var apiSettings = sp.GetRequiredService<IOptions<ApiSettings>>().Value;
    return new HttpClient { BaseAddress = new Uri(apiSettings.BaseUrl) };
});


builder.Services.AddScoped<ProductServices>();
builder.Services.AddScoped<AccountServices>();
builder.Services.AddScoped<OrderServices>();
builder.Services.AddScoped<CateProductServices>();


// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();


builder.Services.AddBlazoredLocalStorage();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
