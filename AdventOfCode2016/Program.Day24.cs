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
                            if (cell.x >= X_DIM ||
                                cell.x < 0      ||
                                cell.y >= Y_DIM ||
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

            private static bool[,] GetInput(IReadOnlyList<string> input)
            {
                var map = new bool[X_DIM, Y_DIM];

                for (int y = 0; y < Y_DIM; y++)
                for (int x = 0; x < X_DIM; x++)
                {
                    map[x, y] = input[y][x] == '#';
                }

                return map;
            }

            private static Dictionary<int, (int x, int y)> GetLocations(IReadOnlyList<string> input)
            {
                var locations = new Dictionary<int, (int, int)>();

                for (int y = 0; y < Y_DIM; y++)
                for (int x = 0; x < X_DIM; x++)
                {
                    if (input[y][x] != '#' &&
                        input[y][x] != '.')
                    {
                        locations.Add(input[y][x] - '0', (x, y));
                    }
                }

                return locations;
            }

            private static int[,] GetDistanceMatrix(IReadOnlyDictionary<int, (int x, int y)> locations, bool[,] map)
            {
                int count = locations.Count;
                var distances = new int[count, count];

                for (int i = 0; i < count; i++)
                for (int j = i + 1; j < count; j++)
                {
                    distances[i, j] = distances[j, i] = ShortestDistance(map, locations[i], locations[j]);
                }

                return distances;
            }

            private static (int distance, string path) GetShortestPathLength(
                IReadOnlyDictionary<int, (int x, int y)> locations,
                int[,] distances,
                int currentLocation,
                string path,
                bool part2 = false)
            {
                (int distance, string path) ret = (0, path);

                if (path.Length == locations.Count * 2 - 1) 
                {
                    return part2
                               ? (distances[currentLocation, 0], $"{path},0")
                               : ret;
                }
                
                foreach (var location in locations)
                {
                    if (location.Key == currentLocation ||
                        path.Split(',').Select(int.Parse).ToList().Contains(location.Key))
                    {
                        continue;
                    }

                    var tmp = GetShortestPathLength(locations,
                                                    distances,
                                                    location.Key,
                                                    $"{path},{location.Key}",
                                                    part2);

                    int distance = distances[currentLocation, location.Key] + tmp.distance;

                    if (ret.distance == 0 ||
                        ret.distance > distance)
                    {
                        ret = (distance, tmp.path);
                    }
                }

                return ret;
            }

            public static void Day24_1(IReadOnlyList<string> input)
            {
                var map       = GetInput(input);
                var locations = GetLocations(input);
                var distances = GetDistanceMatrix(locations, map);

                Console.WriteLine(GetShortestPathLength(locations, distances, 0, "0"));
            }

            public static void Day24_2(IReadOnlyList<string> input)
            {
                var map       = GetInput(input);
                var locations = GetLocations(input);
                var distances = GetDistanceMatrix(locations, map);

                Console.WriteLine(GetShortestPathLength(locations, distances, 0, "0", true));
            }
        }
    }
}
