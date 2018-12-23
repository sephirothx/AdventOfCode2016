using System.Collections.Generic;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day19
        {
            private const int INPUT = 3017957;

            public static int Day19_1()
            {
                var elves = new LinkedList<int>();
                for (int i = 0; i < INPUT; i++)
                {
                    elves.AddLast(i + 1);
                }

                var elf = elves.First;

                while (elves.Count != 1)
                {
                    elves.Remove(elf.Next ?? elves.First);
                    elf = elf.Next ?? elves.First;
                }

                return elf.Value;
            }

            public static int Day19_2()
            {
                var elves          = new LinkedList<int>();
                var nextEliminated = elves.AddFirst(1);

                for (int i = 1; i < INPUT; i++)
                {
                    elves.AddLast(i + 1);
                    if (i % 2 != 0)
                    {
                        nextEliminated = nextEliminated.Next ?? elves.First;
                    }
                }

                while (elves.Count != 1)
                {
                    var tmp = elves.Count % 2 == 0
                                  ? nextEliminated
                                  : nextEliminated.Next ?? elves.First;
                    tmp = tmp.Next ?? elves.First;

                    elves.Remove(nextEliminated);

                    nextEliminated = tmp;
                }

                return elves.First.Value;
            }
        }
    }
}
