using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day13
        {
            private const int INPUT = 1362;
            private const int DIMENSION = 100;

            private static IEnumerable<(int x, int y)> GetNeighbours((int x, int y) cell)
            {
                var ret = new List<(int x, int y)>
                          {
                              (cell.x + 1, cell.y),
                              (cell.x - 1, cell.y),
                              (cell.x, cell.y + 1),
                              (cell.x, cell.y - 1)
                          };

                return ret;
            }

            private static int ShortestDistance(bool[,] map, (int x, int y) start, (int x, int y) destination)
            {
                var current  = new Dictionary<(int x, int y), int> {{destination, 0}};
                var mainList = new Dictionary<(int x, int y), int>(current);

                while (!mainList.ContainsKey(start))
                {
                    var newCurrent = new Dictionary<(int x, int y), int>();

                    foreach (var i in current)
                    {
                        int value      = i.Value + 1;
                        var neighbours = GetNeighbours(i.Key);

                        foreach (var cell in neighbours)
                        {
                            if (cell.x >= DIMENSION ||
                                cell.x < 0          ||
                                cell.y >= DIMENSION ||
                                cell.y < 0)
                            {
                                continue;
                            }

                            if (!newCurrent.ContainsKey(cell))
                            {
                                newCurrent.Add(cell, value);
                            }
                            else if (newCurrent[cell] > value)
                            {
                                newCurrent[cell] = value;
                            }

                            if (map[cell.x, cell.y])
                            {
                                newCurrent.Remove(cell);
                            }

                            if (mainList.ContainsKey(cell))
                            {
                                if (mainList[cell] > value)
                                {
                                    mainList.Remove(cell);
                                }
                                else
                                {
                                    newCurrent.Remove(cell);
                                }
                            }
                        }
                    }

                    current = newCurrent;
                    mainList = mainList.Concat(current).ToDictionary(x => x.Key, x => x.Value);
                }

                return mainList[start];
            }

            private static Dictionary<(int x, int y), int> TilesWithinDistance(bool[,] map,
                                                                               (int x, int y) start, 
                                                                               int distance)
            {
                var mainList = new Dictionary<(int x, int y), int>();
                var current = new Dictionary<(int x, int y), int> { { start, 0 } };

                mainList = mainList.Concat(current).ToDictionary(x => x.Key, x => x.Value);

                while (!mainList.ContainsValue(distance))
                {
                    var newCurrent = new Dictionary<(int x, int y), int>();

                    foreach (var i in current)
                    {
                        int value = i.Value + 1;
                        var neighbours = GetNeighbours(i.Key);

                        foreach (var cell in neighbours)
                        {
                            if (cell.x >= DIMENSION ||
                                cell.x < 0 ||
                                cell.y >= DIMENSION ||
                                cell.y < 0)
                            {
                                continue;
                            }

                            if (!newCurrent.ContainsKey(cell))
                            {
                                newCurrent.Add(cell, value);
                            }
                            else if (newCurrent[cell] > value)
                            {
                                newCurrent[cell] = value;
                            }

                            if (map[cell.x, cell.y])
                            {
                                newCurrent.Remove(cell);
                            }

                            if (mainList.ContainsKey(cell))
                            {
                                if (mainList[cell] > value)
                                {
                                    mainList.Remove(cell);
                                }
                                else
                                {
                                    newCurrent.Remove(cell);
                                }
                            }
                        }
                    }

                    current = newCurrent;
                    mainList = mainList.Concat(current).ToDictionary(x => x.Key, x => x.Value);
                }

                return mainList;
            }

            public static int Day13_1()
            {
                var map = new bool[DIMENSION, DIMENSION];

                for (int y = 0; y < DIMENSION; y++)
                {
                    for (int x = 0; x < DIMENSION; x++)
                    {
                        var result = x     * x +
                                     3     * x +
                                     2 * x * y +
                                     y         +
                                     y * y     +
                                     INPUT;

                        string binary = Convert.ToString(result, 2);

                        if (binary.Count(p => p == '1') % 2 != 0)
                        {
                            map[x, y] = true;
                        }

                        if (x == 31 &&
                            y == 39 &&
                            !map[x, y])
                        {
                            Console.Write('O');
                            continue;
                        }
                        Console.Write(map[x, y] ? '#' : ' ');
                    }

                    Console.WriteLine();
                }

                return ShortestDistance(map, (1, 1), (31, 39));
            }

            public static int Day13_2()
            {
                var map = new bool[DIMENSION, DIMENSION];

                for (int y = 0; y < DIMENSION; y++)
                {
                    for (int x = 0; x < DIMENSION; x++)
                    {
                        var result = x * x +
                                     3 * x +
                                     2 * x * y +
                                     y +
                                     y * y +
                                     INPUT;

                        string binary = Convert.ToString(result, 2);

                        if (binary.Count(p => p == '1') % 2 != 0)
                        {
                            map[x, y] = true;
                        }

                        if (x == 31 &&
                            y == 39 &&
                            !map[x, y])
                        {
                            Console.Write('O');
                            continue;
                        }
                        Console.Write(map[x, y] ? '#' : ' ');
                    }

                    Console.WriteLine();
                }

                return TilesWithinDistance(map, (1, 1), 50).Count;
            }
        }
    }
}
