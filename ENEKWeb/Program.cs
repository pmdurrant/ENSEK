using ENSEK.Contracts;
using ENSEKWeb.Common;
using System.Net;
using System.Security.Cryptography.X509Certificates;
using ENSEK.Repository;
using Microsoft.Extensions.DependencyInjection.Extensions;

var builder = WebApplication.CreateBuilder(args);

var loadpath = Directory.GetCurrentDirectory().ToString() + @"/https/_.officeblox.co.uk-crt.pfx";

try
{
    var cert = new X509Certificate2(loadpath, "Gateway");


    builder.WebHost.UseKestrel(options =>
    {
        options.Listen(IPAddress.Any, 5008);
        options.Listen(IPAddress.Any, 7150,
            listenOptions =>
            {
                listenOptions.UseHttps(cert);
            });
    });

   
}
catch
{
    Console.WriteLine("Certificate load failure");
}
// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();
builder.Services.AddTransient<IStandardHttpClient, StandardHttpClient>();
builder.Services.TryAddSingleton<IHttpContextAccessor, HttpContextAccessor>();
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

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
