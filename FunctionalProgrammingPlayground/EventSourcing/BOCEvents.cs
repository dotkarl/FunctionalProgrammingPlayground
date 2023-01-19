using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground.EventSourcing;
public abstract record Event(Guid EntityId, DateTime TimeStamp);

public record CreatedAccount(Guid EntityId, DateTime TimeStamp, CurrencyCode Currency) : 
    Event(EntityId, TimeStamp);

public record FrozeAccount(Guid EntityId, DateTime TimeStamp) : 
    Event(EntityId, TimeStamp);

public record DepositedCash(Guid EntityId, DateTime TimeStamp, decimal Amount, Guid BranchId) : 
    Event(EntityId, TimeStamp);

public record DebitedTransfer(Guid EntityId, DateTime TimeStamp,
    string Beneficiary, string Iban, string Bic,
    decimal DebitedAmount, string Reference) :
    Event(EntityId, TimeStamp);
