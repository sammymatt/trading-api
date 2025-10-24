using Infrastructure.persistence;
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

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi(); // Exposes the OpenAPI document
    app.MapScalarApiReference(); // Provides the Scalar UI
}

app.RegisterEndpoints();

app.Run();

public record DepositRequest(string Name, decimal Amount);

