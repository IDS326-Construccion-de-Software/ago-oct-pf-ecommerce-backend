using ago_oct_pf_ecommerce_backend.Configuration;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Revenge.Data.Context;
using Revenge.Data.Repositories;
using Revenge.Data.Services;
using Revenge.Infrestructure.Repositories;
using Revenge.Infrestructure.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

builder.Services.ConfigureConnection(configuration);

// Payment Gateway Configuration
builder.Services.AddHttpClient<IPaymentGatewayService, PaymentGatewayService>(client =>
{
    client.BaseAddress = new Uri(builder.Configuration["PaymentGateway:BaseUrl"] ?? "https://cctest.falcon.com.do/");
    client.Timeout = TimeSpan.FromSeconds(30);
});
builder.Services.AddScoped<IPaymentRepository, PaymentRepository>();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowLocalVite",
        policy =>
        {
            policy
                .WithOrigins("http://localhost:5173", "https://localhost:5173")
                .AllowAnyHeader()
                .AllowAnyMethod()
                .AllowCredentials();
        });
});


//configuracion del Auth0
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = "JwtBearer";
    options.DefaultChallengeScheme = "JwtBearer";
})
.AddJwtBearer("JwtBearer", options =>
{
    options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}/";
    options.Audience = builder.Configuration["Auth0:Audience"];
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpClient();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();
app.UseCors("AllowLocalVite");
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();