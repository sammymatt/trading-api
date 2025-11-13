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
                return Results.NotFound("Account not found");

            var response = new AccountResponse(account.Id, account.Name, account.Balance);
            return Results.Ok(response);
        });

        app.MapPost("/api/accounts/{accountId}/deposit", async (string accountId, DepositRequest request, IAccountService accountService) =>
        {
            var account = await accountService.Deposit(accountId, request.Amount);
            if (account is null)
                return Results.NotFound("Account not found");
            
            return Results.Ok(new AccountResponse(account.Id, account.Name, account.Balance));
        });
        
        app.MapPost("/api/accounts/{accountId}/withdraw", async (string accountId, WithdrawRequest request, IAccountService accountService) =>
        {
            var account = await accountService.Withdraw(accountId, request.Amount);
            if (account is null)
                return Results.NotFound("Account not found");
            return Results.Ok(new AccountResponse(account.Id, account.Name, account.Balance));
        });
    }
}