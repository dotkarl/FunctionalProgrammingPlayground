using LaYumba.Functional;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.EventSourcing;
public static class Account
{
    // Create a new AccountState.
    // This is a special case. This applies an Event,
    // but there is no AccountState yet to apply it to.
    // The signature of this method is:
    // Event -> AccountState
    public static AccountState Create(CreatedAccount evt) =>
        new(Currency: evt.Currency, Status: AccountStatus.Active);

    // Apply an event to the AccountState,
    // i.e. calculate a new AccountState
    // based on the existing AccountState plus the event.
    // The signature of this method is:
    // AccountState -> Event -> AccountState
    public static AccountState Apply(this AccountState acc, Event evt) =>
        evt switch
        {
            DepositedCash e => acc with { Balance = acc.Balance + e.Amount },
            DebitedTransfer e => acc with { Balance = acc.Balance - e.DebitedAmount },
            FrozeAccount => acc with { Status = AccountStatus.Frozen },
            _ => throw new InvalidOperationException()
        };

    // Compute an account's state from its event history.
    // It is assumed that the sequence of events is ordered by occurence.
    public static Option<AccountState> From(IEnumerable<Event> history) =>
        history.Match(
            // When no events are found, the Account doesn't exist
            Empty: () => None,
            // There are two types of event: Created and all other,
            // i.e. the head and its tail (cf. linked list).
            Otherwise: (created, otherEvents) =>
                Some(otherEvents.Aggregate(
                    // Use the Created event as the seed, 
                    // this contains the AccountState in its original state.
                    Create((CreatedAccount)created), 
                    // Apply all other events to the AccountState.
                    (state, evt) => state.Apply(evt))));
    // This code shows the current state of the AccountState.
    // If you'd want to see the its state at any point in the past,
    // you'd use the same function, but only include events
    // prior to the desired date.
}
