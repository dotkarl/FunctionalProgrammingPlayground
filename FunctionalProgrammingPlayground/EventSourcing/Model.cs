using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FunctionalProgrammingPlayground.EventSourcing;
public class CurrencyCode { }

public enum AccountStatus 
{
    Requested,
    Active,
    Frozen
}