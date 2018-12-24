using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day21
        {
            private const string INPUT = "abcdefgh";
            private const string OUTPUT = "fbgdceah";

            private enum LeftRight
            {
                Left,
                Right
            }

            private static string SwapPosition(string input, int pos1, int pos2)
            {
                var  buffer = input.ToCharArray();
                char tmp    = buffer[pos1];

                buffer[pos1] = buffer[pos2];
                buffer[pos2] = tmp;

                return new string(buffer);
            }

            private static string SwapLetter(string input, char c1, char c2)
            {
                int pos1 = input.IndexOf(c1);
                int pos2 = input.IndexOf(c2);

                return SwapPosition(input, pos1, pos2);
            }

            private static string Reverse(string input, int pos1, int pos2)
            {
                var buffer = input.ToCharArray();
                Array.Reverse(buffer, pos1, pos2 - pos1 + 1);

                return new string(buffer);
            }

            private static string Rotate(string input, LeftRight side, int steps)
            {
                var buffer = input.ToCharArray();

                for (int step = 0; step < steps; step++)
                {
                    char tmp;
                    switch (side)
                    {
                    case LeftRight.Right:
                        tmp = buffer[buffer.Length - 1];
                        for (int i = buffer.Length - 1; i >= 1; i--)
                        {
                            buffer[i] = buffer[i - 1];
                        }

                        buffer[0] = tmp;
                        break;

                    case LeftRight.Left:
                        tmp = buffer[0];
                        for (int i = 0; i < buffer.Length - 1; i++)
                        {
                            buffer[i] = buffer[i + 1];
                        }

                        buffer[buffer.Length - 1] = tmp;
                        break;

                    default:
                        throw new ArgumentOutOfRangeException(nameof(side), side, null);
                    }
                }

                return new string(buffer);
            }

            private static string RotateLetter(string input, char c)
            {
                int pos = input.IndexOf(c);

                return Rotate(input, LeftRight.Right, 1 + pos + pos / 4);
            }

            private static string Move(string input, int pos1, int pos2)
            {
                char c = input[pos1];
                input = input.Remove(pos1, 1);

                return input.Insert(pos2, c.ToString());
            }

            private static string ScramblePassword(IEnumerable<string> input, string password)
            {
                var regex = new Regex(@"swap (?<mode>\w+) (?<arg1>\d|\w) with \w+ (?<arg2>\d|\w)|rotate (?<side>\w+) (?<steps>\d)|rotate based .*(?<letter>\w)|reverse positions (?<from>\d) .* (?<to>\d)|move position (?<pos1>\d) .* (?<pos2>\d)");
                string output = password;

                foreach (string line in input)
                {
                    var match = regex.Match(line);

                    if (match.Groups["mode"].Success &&
                        match.Groups["mode"].Value == "position")
                    {
                        int pos1 = int.Parse(match.Groups["arg1"].Value);
                        int pos2 = int.Parse(match.Groups["arg2"].Value);
                        output = SwapPosition(output, pos1, pos2);
                        continue;
                    }

                    if (match.Groups["mode"].Success)
                    {
                        char c1 = match.Groups["arg1"].Value.Single();
                        char c2 = match.Groups["arg2"].Value.Single();
                        output = SwapLetter(output, c1, c2);
                        continue;
                    }

                    if (match.Groups["side"].Success)
                    {
                        var side = match.Groups["side"].Value == "left"
                                       ? LeftRight.Left
                                       : LeftRight.Right;
                        int steps = int.Parse(match.Groups["steps"].Value);
                        output = Rotate(output, side, steps);
                        continue;
                    }

                    if (match.Groups["letter"].Success)
                    {
                        char c = match.Groups["letter"].Value.Single();
                        output = RotateLetter(output, c);
                        continue;
                    }

                    if (match.Groups["from"].Success)
                    {
                        int pos1 = int.Parse(match.Groups["from"].Value);
                        int pos2 = int.Parse(match.Groups["to"].Value);
                        output = Reverse(output, pos1, pos2);
                        continue;
                    }

                    if (match.Groups["pos1"].Success)
                    {
                        int pos1 = int.Parse(match.Groups["pos1"].Value);
                        int pos2 = int.Parse(match.Groups["pos2"].Value);
                        output = Move(output, pos1, pos2);
                    }
                }

                return output;
            }

            private static IEnumerable<string> GetPermutations(string list, int length)
            {
                if (length == 1)
                {
                    return list.Select(t => t.ToString());
                }

                return GetPermutations(list, length - 1)
                            .SelectMany(t => list.Where(e => !t.Contains(e)),
                                        (t1, t2) => t1 + t2);
            }

            public static string Day21_1(IEnumerable<string> input)
            {
                return ScramblePassword(input, INPUT);
            }

            public static void Day21_2(string[] input)
            {
                const string LETTERS = "abcdefgh";

                foreach (var permutation in GetPermutations(LETTERS, 8))
                {
                    if (ScramblePassword(input, permutation) == OUTPUT)
                    {
                        Console.WriteLine(permutation);
                        return;
                    }
                }
                
            }
        }
        private static void Main()
        {
            const string PATH  = @"C:\Users\User\Documents\input.txt";
            var          input = File.ReadAllLines(PATH);

            Day21.Day21_2(input);
        }
    }
}
