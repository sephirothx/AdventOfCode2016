using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day10
        {
            public static int Day10_1_2(IEnumerable<string> input)
            {
                var regex = new Regex(@"value (?<value>\d+) goes to bot (?<bot>\d+)|bot (?<source>\d+) gives low to (?<low>(bot|output)) (?<lowval>\d+) and high to (?<high>(bot|output)) (?<highval>\d+)");
                var bots = new Dictionary<int, Action<int>>();
                var outputs = new int[21];

                foreach (string line in input.OrderBy(x => x)) 
                {
                    var match = regex.Match(line);

                    if (match.Groups["value"].Success)
                    {
                        bots[int.Parse(match.Groups["bot"].Value)](int.Parse(match.Groups["value"].Value));
                    }
                    else if (match.Groups["source"].Success)
                    {
                        var vals = new List<int>();
                        var botNum = int.Parse(match.Groups["source"].Value);
                        bots[botNum] = n =>
                                       {
                                           vals.Add(n);
                                           if (vals.Count == 2)
                                           {
                                               if (vals.Min() == 17 &&
                                                   vals.Max() == 61)
                                               {
                                                   Console.WriteLine(botNum);
                                               }

                                               if (match.Groups["low"].Value == "bot")
                                               {
                                                   bots[int.Parse(match.Groups["lowval"].Value)](vals.Min());
                                               }
                                               else
                                               {
                                                   outputs[int.Parse(match.Groups["lowval"].Value)] = vals.Min();
                                               }

                                               if (match.Groups["high"].Value == "bot")
                                               {
                                                   bots[int.Parse(match.Groups["highval"].Value)](vals.Max());
                                               }
                                               else
                                               {
                                                   outputs[int.Parse(match.Groups["highval"].Value)] = vals.Max();
                                               }
                                           }
                                       };
                    }
                }

                return outputs[0] *
                       outputs[1] *
                       outputs[2];
            }
        }
    }
}
