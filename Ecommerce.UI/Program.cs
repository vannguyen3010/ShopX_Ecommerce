using Ecommerce.UI.Components;
using Ecommerce.UI.Services;

var builder = WebApplication.CreateBuilder(args);


//builder.Services.AddScoped<HttpClient>(x =>
//{
//    var Uri = x.GetRequiredService<NavigationManager>();
//    HttpClient Client = new()
//    {
//        BaseAddress = new Uri("https://localhost:7105/"),
//    };
//    Client.DefaultRequestHeaders.Add("Accept", "application/json");
//    return Client;
//});
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:7105/") });


builder.Services.AddRazorComponents()
        .AddInteractiveServerComponents();
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
