using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day15
        {
            private static int WhenToPressButton(IReadOnlyList<(int positions, int current)> discs)
            {
                int i;
                for (i = 0; i < int.MaxValue; i++)
                {
                    var capsuleFalls = true;

                    for (int j = 0; j < discs.Count; j++)
                    {
                        (int positions, int current) = discs[j];
                        if ((i + j + 1 + current) % positions != 0)
                        {
                            capsuleFalls = false;
                            break;
                        }
                    }

                    if (capsuleFalls)
                    {
                        break;
                    }
                }

                return i;
            }

            private static void GetInput(IEnumerable<string> input, IList<(int positions, int current)> discs)
            {
                var regex = new Regex(@"Disc #(\d+) has (\d+) positions; at time=0, it is at position (\d+)\.");

                foreach (string line in input)
                {
                    var match = regex.Match(line);

                    discs[int.Parse(match.Groups[1].Value) - 1] = (int.Parse(match.Groups[2].Value),
                                                                   int.Parse(match.Groups[3].Value));
                }
            }

            public static int Day15_1(IReadOnlyCollection<string> input)
            {
                var discs = new (int positions, int current)[input.Count];
                GetInput(input, discs);

                return WhenToPressButton(discs);
            }

            public static int Day15_2(IReadOnlyCollection<string> input)
            {
                var discs = new (int positions, int current)[input.Count + 1];
                GetInput(input, discs);

                discs[discs.Length - 1] = (11, 0);

                return WhenToPressButton(discs);
            }
        }
    }
}
