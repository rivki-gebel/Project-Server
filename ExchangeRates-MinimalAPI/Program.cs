
using Microsoft.AspNetCore.Mvc;
using Microsoft.OpenApi.Models;
using Project_Server;
using Project_Server.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ExchangeRates API", Version = "v1" });
});

builder.Services.AddSingleton<Service>();


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        builder =>
        {
            builder.AllowAnyOrigin()
                   .AllowAnyMethod()
                   .AllowAnyHeader();
        });
});

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API v1");
        options.RoutePrefix = string.Empty;
    });
}

app.UseCors("AllowAll");


app.MapGet("/", () => "Hello World!");

app.MapGet("/currencyExchange/currencies", GetAllCurrencies);

app.MapGet("/currencyExchange/exchangeRates/{baseCurrency}", GetExchangeRates);

async Task GetExchangeRates(HttpContext context, Service service,string baseCurrency)
{
    Conversion conversion = await service.GetExchangeRates(baseCurrency);
    if (conversion == null)
        await context.Response.WriteAsJsonAsync($"ERROR-conversion is not found");
    await context.Response.WriteAsJsonAsync(conversion);
}

async Task GetAllCurrencies(HttpContext context, Service service)
{
    List<Currency> list = await service.GetCurrenciesList();
    if (list == null)
        await context.Response.WriteAsJsonAsync($"ERROR-list is not found");
    await context.Response.WriteAsJsonAsync(list);
}

app.Run();
