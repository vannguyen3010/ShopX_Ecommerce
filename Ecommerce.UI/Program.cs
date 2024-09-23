using Ecommerce.UI.Components;
using Ecommerce.UI.Services;
using Microsoft.AspNetCore.Components;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// Đăng ký Razor Components với hỗ trợ tương tác
builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();

// Đọc URL API từ appsettings.Development.json
//var apiBaseUrl = builder.Configuration.GetValue<string>("ApiSettings:ApiUrl");

//builder.Services.AddHttpClient("ServerAPI", client =>
//{
//    client.BaseAddress = new Uri(apiBaseUrl);
//});
//builder.Services.AddHttpClient("WebApiClient", client =>
//{
//    client.BaseAddress = new Uri("https://localhost:7105/"); // Đặt URL trỏ đến Web API
//});

builder.Services.AddScoped<HttpClient>(x =>
{
    var Uri = x.GetRequiredService<NavigationManager>();
    HttpClient Client = new()
    {
        BaseAddress = new Uri("https://localhost:7105/"),
    };
    Client.DefaultRequestHeaders.Add("Accept", "application/json");
    return Client;
});


// Đăng ký AccountService
builder.Services.AddScoped<AccountService>();

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
