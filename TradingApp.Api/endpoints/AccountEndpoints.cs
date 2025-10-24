using TradingApp.Api.dtos;
using TradingApp.Application.interfaces;

namespace TradingApp.Api.endpoints;

public static class AccountEndpoints
{
    public static void RegisterEndpoints(this WebApplication app)
    {
        app.MapPost("/api/accounts/create", async (AccountRequest request, IAccountService accountService) =>
        {
            var result = await accountService.Create(request.Name, request.Amount);
            return result 
                ? Results.Ok("Account created") 
                : Results.BadRequest("Account creation failed");
        });

        app.MapGet("/api/accounts/{name}", async (string name, IAccountService accountService) =>
        {
            var account = await accountService.Retrieve(name);
            if (account is null)
                return Results.NotFound();

            var response = new AccountDto(account.Name, account.Balance);
            return Results.Ok(response);
        });

        // app.MapPost("/api/accounts/deposit", (DepositRequest request, IAccountService accountService) =>
        // {
        //     var result = accountService.Deposit(request.Name, request.Amount);
        //     return result 
        //         ? Results.Ok("Deposit successful") 
        //         : Results.BadRequest("Deposit failed");
        // });
    }
}