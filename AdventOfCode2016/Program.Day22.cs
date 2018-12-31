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
                public (int x, int y) Location { get; set; }
                public int Size { get; set; }
                public int Used { get; set; }
                public int Avail { get; set; }
                public int Percentage { get; set; }
                public bool Goal { get; set; }

                public Node(int x, int y, int size, int used, int avail, int percentage)
                {
                    Location   = (x, y);
                    Size       = size;
                    Used       = used;
                    Avail      = avail;
                    Percentage = percentage;
                    Goal       = false;


                }
            }

            private static Node[,] GetInput(IEnumerable<string> input)
            {
                var regex = new Regex(@"x(?<x>\d+)-y(?<y>\d+)\s*(?<size>\d+)T\s*(?<used>\d+)T\s*(?<avail>\d+)T\s*(?<percentage>\d+)");
                var nodes = new Node[X_DIM, Y_DIM];

                foreach (string line in input)
                {
                    var match = regex.Match(line);

                    int x          = int.Parse(match.Groups["x"].Value);
                    int y          = int.Parse(match.Groups["y"].Value);
                    int size       = int.Parse(match.Groups["size"].Value);
                    int used       = int.Parse(match.Groups["used"].Value);
                    int avail      = int.Parse(match.Groups["avail"].Value);
                    int percentage = int.Parse(match.Groups["percentage"].Value);

                    nodes[x, y] = new Node(x, y, size, used, avail, percentage);
                }

                return nodes;
            }

            private static IEnumerable<Node> GetNeighbors(Node[,] nodes, int x, int y)
            {
                var neighbors = new List<Node>();

                for (int i = x - 1; i <= x + 1; i++)
                for (int j = y - 1; j <= y + 1; j++)
                {
                    if (x < 0      ||
                        y < 0      ||
                        x >= X_DIM ||
                        y >= Y_DIM)
                    {
                        continue;
                    }

                    neighbors.Add(nodes[x, y]);
                }
                
                return neighbors;
            }

            private static List<(Node giver, Node taker)> GetViablePairs(Node[,] nodes)
            {
                var viablePairs = new List<(Node giver, Node taker)>();
                
                foreach (var node in nodes)
                foreach (var neighbor in GetNeighbors(nodes, node.Location.x, node.Location.y))
                {
                    if (node.Used <= neighbor.Avail)
                    {
                        viablePairs.Add((node, neighbor));
                    }
                }

                return viablePairs;
            }

            private static int FindShortestPathLength(Node[,] nodes)
            {
                var tmpNodes = new Node[X_DIM, Y_DIM];
                Array.Copy(nodes, tmpNodes, nodes.Length);

                return 0;
            }

            public static void Day22_1(IEnumerable<string> input)
            {
                var nodes       = GetInput(input);
                int viablePairs = 0;

                for (int i = 0; i < nodes.Length; i++)
                for (int j = i + 1; j < nodes.Length; j++)
                {
                    if (nodes[i / Y_DIM, i % Y_DIM].Used != 0 &&
                        nodes[i / Y_DIM, i % Y_DIM].Used <= nodes[j / Y_DIM, j % Y_DIM].Avail)
                    {
                        viablePairs++;
                    }

                    if (nodes[j / Y_DIM, j % Y_DIM].Used != 0 &&
                        nodes[j / Y_DIM, j % Y_DIM].Used <= nodes[i / Y_DIM, i % Y_DIM].Avail)
                    {
                        viablePairs++;
                    }
                }

                Console.WriteLine(viablePairs);
            }

            public static void Day22_2(IEnumerable<string> input)
            {
                var nodes = GetInput(input);
                nodes[X_DIM - 1, Y_DIM - 1].Goal = true;
                
            }
        }
    }
}
