// ***********************************************************************
// Assembly         : ENSEKWeb
// Author           : pdurr
// Created          : 04-21-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-22-2023
// ***********************************************************************
// <copyright file="Program.cs" company="ENSEKWeb">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************


using System.Security.Cryptography.X509Certificates;
using ENSEK.Contracts;
using ENSEK.Repository;
using ENSEKWeb.Common;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Net;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var builder = WebApplication.CreateBuilder(args);
var loadpath = Directory.GetCurrentDirectory().ToString() + @"/https/_.officeblox.co.uk-crt.pfx";

try
{
    var cert = new X509Certificate2(loadpath, "Gateway");


    builder.WebHost.UseKestrel(options =>
    { 
        options.Listen(IPAddress.Any, 5008);
        options.Listen(IPAddress.Any, 5009,
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
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.UseHttpsRedirection();
app.Run();
