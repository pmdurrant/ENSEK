using AutoMapper;
using ENSEK.Contracts;
using ENSEK.Entities.DTO;
using ENSEK.Entities.Models;
using ENSEK.Repository;
using ENSEK_API;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Identity.Web;

System.Text.Encoding.RegisterProvider(System.Text.CodePagesEncodingProvider.Instance);
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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
