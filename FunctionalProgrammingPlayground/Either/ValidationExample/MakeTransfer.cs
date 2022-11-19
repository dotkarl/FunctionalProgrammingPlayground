using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground.Either.ValidationExample;

public abstract record Command(DateTime Timestamp);

public record class MakeTransfer(
    Guid DebitedAccountId,
    string Beneficiary,
    string Iban,
    string Bic,
    DateTime Date,
    decimal Amount,
    string Reference,
    DateTime Timestamp = default) : Command(Timestamp)
{
    internal static MakeTransfer Dummy =>
        new(default, 
            default,
            default, 
            default, 
            default, 
            default, 
            default);
}
