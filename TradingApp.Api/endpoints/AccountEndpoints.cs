using TradingApp.Api.dtos;
using TradingApp.Application.interfaces;

namespace TradingApp.Api.endpoints;

public static class AccountEndpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.MapPost("/api/accounts", async (AccountRequest request, IAccountService accountService) =>
        {
            var account = await accountService.Create(request.Name, request.Amount);
            return Results.Created($"/api/accounts/{account.Id}", account);
        });

        app.MapGet("/api/accounts/{name}", async (string name, IAccountService accountService) =>
        {
            var account = await accountService.Retrieve(name);
            if (account is null)
                return Results.NotFound();

            var response = new AccountDto(account.Name, account.Balance);
            return Results.Ok(response);
        });

        app.MapPost("/api/accounts/deposit", async (DepositRequest request, IAccountService accountService) =>
        {
            var account = await accountService.Deposit(request.Name, request.Amount);
            return Results.Ok(new DepositResponse(account.Id, account.Name, account.Balance));
        });
    }
}