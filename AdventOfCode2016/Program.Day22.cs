using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day22
        {
            private const int X_DIM = 38;
            private const int Y_DIM = 24;

            private class Node
            {
                public int Used { get; }
                public int Avail { get; }

                public Node(int used, int avail)
                {
                    Used  = used;
                    Avail = avail;
                }
            }

            private static Node[,] GetInput(IEnumerable<string> input)
            {
                var regex = new Regex(@"x(?<x>\d+)-y(?<y>\d+)\s*(?<size>\d+)T\s*(?<used>\d+)T\s*(?<avail>\d+)T\s*(?<percentage>\d+)");
                var nodes = new Node[X_DIM, Y_DIM];

                foreach (string line in input)
                {
                    var match = regex.Match(line);

                    int x     = int.Parse(match.Groups["x"].Value);
                    int y     = int.Parse(match.Groups["y"].Value);
                    int used  = int.Parse(match.Groups["used"].Value);
                    int avail = int.Parse(match.Groups["avail"].Value);

                    nodes[x, y] = new Node(used, avail);
                }

                return nodes;
            }

            public static void Day22_1(IEnumerable<string> input)
            {
                var nodes       = GetInput(input);
                int viablePairs = 0;

                foreach (var node in nodes)
                foreach (var node1 in nodes)
                {
                    if (node      != node1 &&
                        node.Used != 0     &&
                        node.Used <= node1.Avail)
                    {
                        viablePairs++;
                    }
                }

                Console.WriteLine(viablePairs);
            }

            public static void Day22_2(IEnumerable<string> input)
            {
                var nodes = GetInput(input);

                for (int y = 0; y < Y_DIM; y++)
                {
                    for (int x = 0; x < X_DIM; x++)
                    {
                        Console.Write($"{nodes[x, y].Used}-{nodes[x, y].Avail} ");
                    }

                    Console.WriteLine();
                }

                Console.WriteLine("NOW COUNT");
            }
        }
    }
}
