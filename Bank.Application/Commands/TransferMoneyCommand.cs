namespace Bank.Application.Commands;
/// <summary>
/// Команда перевода денег со счёта на счёт. Счёт оправителя определяется по идентификатору, получателя - по номеру телефона.
/// </summary>
/// <param name="SenderBankAccountId">Обязательный параметр</param>
public class TransferMoneyCommand : IRequest<bool>
{
    public Guid SenderBankAccountId { get; init; }
    public string Currency { get; init; }
    public decimal ReceiverBankAccountAmount { get; init; }
    public string ReceiverPhoneNumber { get; init; }
    public decimal AmountToTransfer { get; init; }

    public TransferMoneyCommand(string senderBankAccountId, string currency, decimal receiverBankAccountAmount,
        string receiverPhoneNumber, decimal amountToTransfer)
    {
        SenderBankAccountId = Guid.Parse(senderBankAccountId);
        Currency = currency;
        ReceiverBankAccountAmount = receiverBankAccountAmount;
        ReceiverPhoneNumber = receiverPhoneNumber;
        AmountToTransfer = amountToTransfer;
    }
}