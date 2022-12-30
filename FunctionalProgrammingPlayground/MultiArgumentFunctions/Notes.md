﻿# On multi-argument functions


*Effectful* types are types that an an "effect" to some other type. For instance, `Option` adds the effect of optionality, `Exceptional` the effect of exception handling, and `IEnumerable` the effect of aggregation to whatever type they wrap. (Other effects include state, laziness and asynchrony.)


Core functions like `Map` and `Bind` allow you to work with effectful types -- but only in a limited way. Both functions only take unary functions as their second parameter (`T -> R`). 


This raises the question: how do we integrate multi-argument functions in our workflow? There are two possible approaches:

- **The applicative approach.** This uses the core function `Apply` to an elevated type.

- **The monadic approach.**