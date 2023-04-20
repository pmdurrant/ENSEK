// ***********************************************************************
// Assembly         : ENSEK-API
// Author           : pdurr
// Created          : 04-12-2023
//
// Last Modified By : pdurr
// Last Modified On : 04-15-2023
// ***********************************************************************
// <copyright file="Program.cs" company="ENSEK-API">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************

using System.Net;
using System.Security.Cryptography.X509Certificates;
using ENSEK.Contracts;
using ENSEK.Repository;
using ENSEK_API;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Identity.Web;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var builder = WebApplication.CreateBuilder(args);


var loadpath = Directory.GetCurrentDirectory().ToString() + @"/https/_.officeblox.co.uk-crt.pfx";

try
{
    var cert = new X509Certificate2(loadpath, "Gateway");

builder.WebHost.UseKestrel(options =>
{
    options.Listen(IPAddress.Any, 5002);
    options.Listen(IPAddress.Any, 5004,
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

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddAutoMapper(typeof(MappingProfile));
// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<IMeterReadingRepository, MeterReadingRepository>();
var conbuilder = new ConfigurationBuilder()
    .AddJsonFile($"appsettings.json", true, true);

var config = conbuilder.Build();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
