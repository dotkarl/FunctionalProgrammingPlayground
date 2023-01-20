using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground.EventSourcing.Queries;
public record AccountStatement(
    int Month,
    int Year,
    decimal StartingBalance,
    decimal EndBalance,
    IEnumerable<Transaction> Transactions);

public record Transaction(
    DateTime Date,
    string Description,
    decimal DebitedAmount = 0m,
    decimal CreditedAmount = 0m);
