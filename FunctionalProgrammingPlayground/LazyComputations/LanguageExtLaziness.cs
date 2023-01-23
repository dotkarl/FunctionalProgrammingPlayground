using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static LanguageExt.Prelude;

namespace FunctionalProgrammingPlayground.LazyComputations;
internal class LanguageExtLaziness
{
    public static void Run()
    {
        var opt = Some(123);
        var orElse = opt || Some(456);
        // LanguageExt does not specify an OrElse method,
        // like LaYumba.Functional does.
        // It does provide the same functionality though,
        // by overloading the |-operator.
        // The code above is equivalent to the following:
        // var orElse = opt.OrElse(Some(456));

        // Because of the short circuiting of the ||-operator,
        // there is no need to wrap the second Option
        // in a Func. The right hand is only evaluated
        // when opt is None.
    }
}
