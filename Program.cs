using LocalizationApp.LocalizationService;
using LocalizationApp.Middleware;
using LocalizationApp.Models;
using Microsoft.Extensions.Caching.Memory;
using System.Globalization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews()
                .AddViewLocalization();

builder.Services.AddSingleton<MemoryCache>();
builder.Services.AddDbContext<SystemContext>();
builder.Services.Configure<RequestLocalizationOptions>(options =>
{

    CultureInfo[] cultures = new CultureInfo[]
    {
        new("tr-TR"),
        new("en-US"),
        new("fr-FR")
    };

    options.DefaultRequestCulture = new(cultures[0]);
    options.SupportedCultures = cultures;
    options.SupportedUICultures = cultures;
});
builder.Services.AddScoped<LocalizationMiddleware>();
builder.Services.AddTransient<ILocalizationService, LocalizationService>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();


app.UseRequestLocalization();
app.UseMiddleware<LocalizationMiddleware>();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
