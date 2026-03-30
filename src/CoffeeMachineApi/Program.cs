
using CoffeeMachineApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddSingleton<IDateTimeProvider, SystemDateTimeProvider>();
builder.Services.AddSingleton<BrewCounter>();
builder.Services.AddSingleton<ICoffeeBrewService, CoffeeBrewService>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Coffee Machine API v1");
});

app.MapControllers();
app.Run();

public partial class Program { }
