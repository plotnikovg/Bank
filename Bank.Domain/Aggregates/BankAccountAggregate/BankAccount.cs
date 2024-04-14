using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class BankAccount : BaseEntity, IAggregateRoot
{
    public Money Balance { get; private set; }
    public decimal WithdrawalLimit { get; private set; } //Лимит на снятие средств
    public List<BankCard> BankCards { get; private set; }  //Карты
}