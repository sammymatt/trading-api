namespace TradingApp.Api.dtos;

public record DepositResponse(Guid Id, string Name, decimal Balance, string Message = "Deposit successful");