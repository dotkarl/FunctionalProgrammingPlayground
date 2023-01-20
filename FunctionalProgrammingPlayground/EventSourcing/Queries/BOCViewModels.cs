using LaYumba.Functional;
using static LaYumba.Functional.F;

namespace FunctionalProgrammingPlayground.EventSourcing.Queries;
internal class BOCViewModels
{
    // This function creates a view model based on the events stored.
    // Note that this view model is completely decoupled from the underlying data.
    // Transforming the events stored to a viewmodel involves some work,
    // but you benefit from a very loose coupling, allowing you
    // to stay centered on the user experience.
    public static AccountStatement Create(int month, int year, IEnumerable<Event> events)
    {
        var startOfPeriod = new DateTime(year, month, 1);
        var endOfPeriod = startOfPeriod.AddMonths(1);

        var (eventsBeforePeriod, eventsDuringPeriod) = events
            .TakeWhile(e => endOfPeriod < e.TimeStamp)
            .Partition(e => e.TimeStamp <= startOfPeriod);

        var startingBalance = eventsBeforePeriod.Aggregate(0m, BalanceReducer);
        var endBalance = eventsDuringPeriod.Aggregate(startingBalance, BalanceReducer);

        return new(month, year, startingBalance, endBalance, eventsDuringPeriod.Bind(CreateTransaction));
    }

    private static decimal BalanceReducer(decimal balance, Event evt) =>
        evt switch
        {
            DepositedCash e => balance + e.Amount,
            DebitedTransfer e => balance - e.DebitedAmount,
            _ => balance
        };

    // Some events involve a transaction (e.g. making a transfer),
    // others don't (e.g. update the account status).
    // This method filters out the transaction events.
    private static Option<Transaction> CreateTransaction(Event evt) =>
        evt switch
        {
            DepositedCash e => new Transaction(
                CreditedAmount: e.Amount,
                Description: $"Deposit at {e.BranchId}",
                Date: e.TimeStamp.Date),
            DebitedTransfer e => new Transaction(
                DebitedAmount: e.DebitedAmount,
                Description: $"Transfer to {e.Bic}/{e.Iban}; {e.Reference}",
                Date: e.TimeStamp.Date),
            _ => None
        };

    // Computing a view model can be resource intensive.
    // It involves gathering potentially hundreds or thousands of events
    // and transforming them to fit your needs.
    // In order to mitigate some of the performance drawbacks,
    // view models are often cached, so only a fraction of the events
    // need to be processed.
}
