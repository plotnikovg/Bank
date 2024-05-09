namespace Bank.Application.Commands;

public class AddBankAccountToClientCommand : IRequest<bool>
{
    public Guid ClientId { get; init; }
    public string Currency { get; init; }
    public decimal Amount { get; init; }
    public decimal WithdrawalLimit { get; init; }

    public AddBankAccountToClientCommand(Guid clientId, string currency, decimal amount, decimal withdrawalLimit)
    {
        ClientId = clientId;
        Currency = currency;
        Amount = amount;
        WithdrawalLimit = withdrawalLimit;
    }
}