using Bank.Domain.Aggregates.BankAccountAggregate;

namespace Bank.Application.Commands;

public class AddBankAccountToClientCommandHandler : IRequestHandler<AddBankAccountToClientCommand, bool>
{
    private readonly IClientRepository _clientRepository;
    private readonly ILogger<AddBankAccountToClientCommand> _logger;

    public AddBankAccountToClientCommandHandler(IClientRepository clientRepository, ILogger<AddBankAccountToClientCommand> logger)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    
    public async Task<bool> Handle(AddBankAccountToClientCommand request, CancellationToken cancellationToken)
    {
        var client = await _clientRepository.FindByIdAsync(request.ClientId);
        Currency currency;
        switch (request.Currency.ToUpperInvariant())
        {
            case "RUB": currency = Currency.RUB; 
                break;
            case "USD": currency = Currency.USD; 
                break;
            case "EUR": currency = Currency.EUR; 
                break;
            default : throw new ArgumentException(nameof(request.Currency));
        }

        var balance = new Money(currency, request.Amount);
        var bankAccount = new BankAccount(balance, request.WithdrawalLimit);
        
        _logger.LogInformation("Create bank account: {@bankAccount} for client: {@client}", bankAccount, client);

        _clientRepository.AddBankAccount(client, bankAccount);
        
        return await _clientRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}