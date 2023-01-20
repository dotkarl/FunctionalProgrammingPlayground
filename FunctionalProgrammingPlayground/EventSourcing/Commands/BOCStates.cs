using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground.EventSourcing.Commands;
public sealed record AccountState(CurrencyCode Currency,
    AccountStatus Status = AccountStatus.Requested,
    decimal Balance = 0m,
    decimal AllowedOverdraft = 0m);
