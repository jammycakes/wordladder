Solving word ladder puzzles with an evolutionary algorithm
==========================================================

The challenge is to construct a "word ladder" starting with one word and ending
with another in as few steps as possible. The rules are as follows:

 1. Only words found in SOWPODS (the official Scrabble dictionary) are allowed.
 2. Each step can either:
    * add one letter (e.g. SOLE -> STOLE)
    * remove one letter (e.g. STOLE -> SOLE)
    * change one letter (e.g. STOLE -> STONE)
 3. A step can not both add/remove a letter and change a letter
    (so SOLE -> STONE is not allowed)
 4. A step can not move letters from one position to another
    (so SOLE -> SLOE is not allowed)

A brute force approach would take too long here. If there were ten possible
candidates for every step in the "ladder," then the time taken to find the
optimal solution with n steps would be O(10<sup>n</sup>). This wouldn't work
in a reasonable amount of time for anything with more than about five or six
steps.

Instead, I've implemented a simple evolutionary algorithm to tackle the puzzle.
For a simple overview of how evolutionary algorithms work, see this YouTube
video:

 * ["I programmed some creatures. They evolved."](https://www.youtube.com/watch?v=N3tRFayqVtk)

In the evolutionary algorithm I have implemented here, the fitness function is
calculated on the basis of the number of steps for ladders that have reached
their target, or the [Levenshtein distance](https://en.wikipedia.org/wiki/Levenshtein_distance)
between the target and the final step in the ladder for those that have not.
The list of words in the ladder is used as the genome, and new genomes for
candidates in each generation are determined by finding the words at the
start of the ladder that are common to both parents.
