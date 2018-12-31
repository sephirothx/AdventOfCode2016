using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day25
        {
            public static void Day25_1(IReadOnlyList<string> input)
            {
                var regex = new Regex(@"(?<opcode>\w+) (?<op1>-?\d+|\w+)\s?(?<op2>-?\d+|\w+)?");

                for (int i = 0; i < int.MaxValue; i++)
                {
                    int previousValue = -1;

                    var registers = new Dictionary<string, int>
                                    {
                                        {"a", i},
                                        {"b", 0},
                                        {"c", 0},
                                        {"d", 0}
                                    };

                    int timeout = 0;

                    for (int index = 0; index < input.Count; index++)
                    {
                        string line   = input[index];
                        var    match  = regex.Match(line);
                        string opcode = match.Groups["opcode"].Value;
                        string op1    = match.Groups["op1"].Value;
                        string op2    = match.Groups["op2"].Value;

                        switch (opcode)
                        {
                        case "inc":
                            registers[op1]++;
                            break;

                        case "dec":
                            registers[op1]--;
                            break;

                        case "cpy":
                            if (int.TryParse(op1, out int cpy))
                            {
                                registers[op2] = cpy;
                            }
                            else
                            {
                                registers[op2] = registers[op1];
                            }

                            break;

                        case "jnz":
                            if (int.TryParse(op1, out int jnz))
                            {
                                index += jnz != 0
                                             ? int.Parse(op2) - 1
                                             : 0;
                            }
                            else
                            {
                                index += registers[op1] != 0
                                             ? int.Parse(op2) - 1
                                             : 0;
                            }

                            break;

                        case "out":
                            if (previousValue == registers[op1])
                            {
                                index = input.Count;
                            }

                            previousValue = registers[op1];

                            if (timeout++ == 10)
                            {
                                Console.WriteLine(i);
                                return;
                            }

                            break;
                        }
                    }
                }
            }
        }
    }
}
