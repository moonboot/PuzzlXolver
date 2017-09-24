# PuzzlXolver
Brute force solver for a particular kind of "puzzle crosswords".

Given a simple matrix layout with an initial anchor word:

[A] | [R] | [C]
-- | -- | --
[ ] |     | [ ]
[ ] | [ ] | [ ]

and three unused words "ABE", "EAR" and "CAR", the solution is:

[A] | [R] | [C]
-- | -- | --
[B] |     | [A]
[E] | [A] | [R]
