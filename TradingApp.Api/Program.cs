using Infrastructure.persistence;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using TradingApp.Api.endpoints;
using TradingApp.Application;
using TradingApp.Application.interfaces;
using TradingApp.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add OpenAPI services
builder.Services.AddOpenApi();

builder.Services.AddScoped<IAccountService, AccountService>();
builder.Services.AddScoped<IAccountRepository, AccountRepository>();

builder.Services.AddDbContext<TradingDbContext>(options =>
    options.UseInMemoryDatabase("TradingDb"));

var app = builder.Build();


app.MapOpenApi(); // Exposes the OpenAPI document
app.MapScalarApiReference(); // Provides the Scalar UI

app.UseExceptionHandler(exceptionHandlerApp 
    => exceptionHandlerApp.Run(async context 
        => await Results.Problem()
            .ExecuteAsync(context)));

app.RegisterEndpoints();

app.Run();