using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;

var builder = WebApplication.CreateBuilder(args);

//Configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");

builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

//builder.Services.ConfigureConnection(configuration);
builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
