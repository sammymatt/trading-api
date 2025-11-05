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

            var response = new AccountResponse(account.Id, account.Name, account.Balance);
            return Results.Ok(response);
        });

        app.MapPost("/api/accounts/{accountId}/deposit", async (string accountId, DepositRequest request, IAccountService accountService) =>
        {
            var account = await accountService.Deposit(accountId, request.Amount);
            return Results.Ok(new AccountResponse(account.Id, account.Name, account.Balance));
        });
        
        app.MapPost("/api/accounts/{accountId}/withdraw", async (string accountId, WithdrawRequest request, IAccountService accountService) =>
        {
            var account = await accountService.Withdraw(accountId, request.Amount);
            return Results.Ok(new AccountResponse(account.Id, account.Name, account.Balance));
        });
    }


    public static void Test()
    {
        int[] nums = new int[4];
        nums[0] = 1;
        nums[1] = 2;
        nums[2] = 3;
        nums[3] = 4;
        for (int i = 0; i < nums.Length; i++)
        {
            Console.WriteLine(nums[i]);
        }

        List<string> list = new List<string>();
        list.Add("one");
        list.Add("two");
        list.Remove("one");
        
        Dictionary<int, string> dict = new Dictionary<int, string>();
        dict.Add(1, "one");
        
        HashSet<string> hashSet = new HashSet<string>();
        hashSet.Add("one");
        hashSet.Add("two");
    }
}