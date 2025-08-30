using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Revenge.Data.Context;

var builder = WebApplication.CreateBuilder(args);

//Configuration
var configuration = new ConfigurationBuilder()
    .AddJsonFile("appsettings.json", optional: false)
    .Build();

var connectionString = builder.Configuration.GetConnectionString("DbConnection");
AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);

builder.Services.AddDbContext<Context>(options => options.UseNpgsql(connectionString));

if (connectionString == null)
{
    Console.WriteLine("Aqui no hay na mio");

} else Console.WriteLine("La conexion existe");

//builder.Services.ConfigureConnection(configuration);
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();