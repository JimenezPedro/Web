using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services
builder.Services.AddRazorPages();

// Para usar session
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

// HttpClientFactory
builder.Services.AddHttpClient();

var app = builder.Build();

// Middleware
app.UseStaticFiles();
app.UseRouting();
app.UseSession(); // << importante

app.MapRazorPages();

app.Run();
