using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day23
        {
            private static string[][] GetInput(IReadOnlyCollection<string> input)
            {
                var regex        = new Regex(@"(?<opcode>\w+) (?<op1>-?\d+|\w+)\s?(?<op2>-?\d+|\w+)?");
                var instructions = new string[input.Count][];

                int i = 0;
                foreach (string line in input)
                {
                    var    match  = regex.Match(line);
                    string opcode = match.Groups["opcode"].Value;
                    string op1    = match.Groups["op1"].Value;
                    string op2    = match.Groups["op2"].Value;

                    var instruction = new[]
                                      {
                                          opcode,
                                          op1,
                                          op2
                                      };

                    instructions[i] = instruction;

                    i++;
                }

                return instructions;
            }

            private static void ToggleInstruction(IList<string> instruction)
            {
                switch (instruction[0])
                {
                case "inc":
                    instruction[0] = "dec";
                    break;

                case "jnz":
                    instruction[0] = "cpy";
                    break;

                case "tgl":
                case "dec":
                    instruction[0] = "inc";
                    break;

                case "cpy":
                    instruction[0] = "jnz";
                    break;
                }
            }

            public static void Day23_1(IReadOnlyCollection<string> input)
            {
                var instructions = GetInput(input);
                var registers = new Dictionary<string, int>
                                {
                                    {"a", 7},
                                    {"b", 0},
                                    {"c", 0},
                                    {"d", 0}
                                };

                for (int i = 0; i < instructions.Length; i++)
                {
                    var    instruction = instructions[i];
                    string opcode      = instruction[0];
                    string op1         = instruction[1];
                    string op2         = instruction[2];

                    switch (opcode)
                    {
                    case "inc":
                        if (int.TryParse(op1, out int _)) break;

                        registers[op1]++;
                        break;

                    case "dec":
                        if (int.TryParse(op1, out int _)) break;

                        registers[op1]--;
                        break;

                    case "cpy":
                        if (int.TryParse(op2, out int _)) break;

                        if (!int.TryParse(op1, out int cpy))
                        {
                            cpy = registers[op1];
                        }

                        registers[op2] = cpy;
                        break;

                    case "jnz":
                        if (!int.TryParse(op2, out int op))
                        {
                            op = registers[op2];
                        }

                        if (!int.TryParse(op1, out int jnz))
                        {
                            jnz = registers[op1];
                        }

                        i += jnz != 0
                                 ? op - 1
                                 : 0;
                        break;

                    case "tgl":
                        if (!int.TryParse(op1, out int tgl))
                        {
                            tgl = registers[op1];
                        }

                        if (i + tgl >= instructions.Length) break;

                        ToggleInstruction(instructions[i + tgl]);
                        break;
                    }
                }

                Console.WriteLine(registers["a"]);
            }

            public static void Day23_2()
            {
                int Fact(int n) => n == 0 ? 1 : Enumerable.Range(1, n).Aggregate((acc, x) => acc * x);

                Console.WriteLine(Fact(12) + 76 * 80);
            }
        }
    }
}
