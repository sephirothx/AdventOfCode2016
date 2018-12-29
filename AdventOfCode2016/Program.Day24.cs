using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day24
        {
            private const int X_DIM = 183;
            private const int Y_DIM = 39;
            private const int WALL = -1;

            private static IEnumerable<(int x, int y)> GetNeighbours((int x, int y) cell)
            {
                var ret = new List<(int x, int y)>
                          {
                              (cell.x, cell.y - 1),
                              (cell.x - 1, cell.y),
                              (cell.x + 1, cell.y),
                              (cell.x, cell.y + 1)
                          };

                return ret;
            }

            private static int[,] FindPaths(int[,] map, (int x, int y) destination)
            {
                var current = new HashSet<(int x, int y)> {destination};

                var outputMap = new int[X_DIM, Y_DIM];
                Array.Copy(map, outputMap, map.Length);

                bool noMorePaths = false;

                while (!noMorePaths)
                {
                    var newCurrent = new HashSet<(int x, int y)>();
                    noMorePaths = true;

                    foreach (var i in current)
                    {
                        int value = outputMap[i.x, i.y] < 0
                                        ? 1
                                        : outputMap[i.x, i.y] + 1;
                        var neighbours = GetNeighbours(i);

                        foreach (var cell in neighbours)
                        {
                            if (cell.x >= X_DIM ||
                                cell.x < 0          ||
                                cell.y >= Y_DIM ||
                                cell.y < 0)
                            {
                                continue;
                            }

                            if (!newCurrent.Contains(cell))
                            {
                                newCurrent.Add(cell);
                            }

                            if (outputMap[cell.x, cell.y] > value ||
                                outputMap[cell.x, cell.y] == 0)
                            {
                                outputMap[cell.x, cell.y] = value;
                            }

                            if (outputMap[cell.x, cell.y] < value)
                            {
                                newCurrent.Remove(cell);
                            }
                        }
                    }

                    if (newCurrent.Any())
                    {
                        noMorePaths = false;
                    }

                    current = newCurrent;
                }

                return outputMap;
            }

            private static int[,] GetInput(string[] input)
            {
                var map = new int[X_DIM, Y_DIM];

                for (int y = 0; y < input.Length; y++)
                for (int x = 0; x < input[y].Length; x++)
                {
                    map[x, y] = input[y][x] == '#'
                                    ? WALL
                                    : 0;
                }

                return map;
            }

            public static void Day24_1(string[] input)
            {
                var map = GetInput(input);
            }
        }
    }
}
