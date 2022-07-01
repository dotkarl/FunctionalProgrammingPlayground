# On Options

There are two sets of functions:


1. **Total functions.** Mappings that are defined for *every* element in the domain.

2. **Partial functions.** Mappings that are defined for *some* but not all elements of the domain.


It is unclear what a partial function should return. Options bridge the gap between total and partial functions. By returning an Option with the value of None, all the elements of the domain can be mapped. 


Alternatives to Options include returning `null` or throwing an Exception. This, however, would result in a dishonest function. After all, the method signature does not signal that behaviour. 
