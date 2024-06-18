using Bank.Domain.Common;

namespace Bank.Domain.Aggregates.BankAccountAggregate;

public class BankCard : BaseEntity
{
    public new string Id { get; set; }
}