using System.Collections.Immutable;
using Bank.Application.Repositories;
using Bank.Domain.Aggregates.BankAccountAggregate;

namespace Bank.Application.Commands;

public class TransferMoneyCommandHandler : IRequestHandler<TransferMoneyCommand, bool>
{
    private readonly IClientRepository _clientRepository;
    private readonly IBankAccountRepository _bankAccountRepository;
    private readonly ILogger<TransferMoneyCommand> _logger;

    public TransferMoneyCommandHandler(IClientRepository clientRepository, IBankAccountRepository bankAccountRepository,
        ILogger<TransferMoneyCommand> logger)
    {
        _clientRepository = clientRepository ?? throw new ArgumentNullException(nameof(clientRepository));
        _bankAccountRepository = bankAccountRepository ?? throw new ArgumentNullException(nameof(bankAccountRepository));
        _logger = logger ?? throw new ArgumentNullException(nameof(logger));
    }
    public async Task<bool> Handle(TransferMoneyCommand request, CancellationToken cancellationToken)
    {
        var senderBankAccount = await _bankAccountRepository.FindByIdAsync(request.SenderBankCardId);
        var moneyToTransfer = new Money(request.Currency, request.AmountToTransfer);
        var receiver = await _clientRepository.FindByPhoneNumberAsync(request.ReceiverPhoneNumber);
        if (receiver == null) throw new ArgumentNullException(nameof(receiver), "Receiver client not found");

        if (receiver.AccountForReceivingTransfers == null)
            throw new ApplicationException("Receiver doesn't have any accounts");
        var receiverBankAccount = receiver.AccountForReceivingTransfers.Balance.Currency == moneyToTransfer.Currency 
            ? receiver.AccountForReceivingTransfers :
            receiver.BankAccounts.ToImmutableList().Find(x => x.Balance.Currency == moneyToTransfer.Currency);
        if (receiverBankAccount == null) throw new ArgumentNullException(nameof(receiverBankAccount), "Receiver doesn't have required account");
        
        senderBankAccount.BalanceDecrease(moneyToTransfer);
        receiverBankAccount.BalanceIncrease(moneyToTransfer);
        
        _logger.LogInformation("Transfer {@moneyToTransfer} from {@senderBankAccount} to {@receiverBankAccount}", moneyToTransfer, senderBankAccount, receiverBankAccount);

        _bankAccountRepository.Update(senderBankAccount);
        _bankAccountRepository.Update(receiverBankAccount);
        
        return await _bankAccountRepository.UnitOfWork.SaveEntitiesAsync(cancellationToken);
    }
}