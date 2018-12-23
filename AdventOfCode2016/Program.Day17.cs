using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day17
        {
            private const string INPUT = "ioramepc";

            private const int UP = 0;
            private const int DOWN = 1;
            private const int LEFT = 2;
            private const int RIGHT = 3;

            private static int BestSoFar = int.MaxValue;

            private static string CalculateHash(string input)
            {
                var md5        = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hashBytes  = md5.ComputeHash(inputBytes);

                return hashBytes.Aggregate("", (current, t) => current + t.ToString("x2"));
            }

            private static string FindShortestPath(int x, int y, string input)
            {
                if (x == 3 &&
                    y == 3)
                {
                    BestSoFar = Math.Min(input.Length, BestSoFar);
                    return input;
                }

                if (input.Length >= BestSoFar)
                {
                    return string.Empty;
                }

                string ret    = string.Empty;
                int    length = int.MaxValue;
                string hash   = CalculateHash(input);

                if (x           < 3    &&
                    hash[RIGHT] >= 'b' &&
                    hash[RIGHT] <= 'f')
                {
                    string tmp = FindShortestPath(x + 1, y, input + "R");
                    if (tmp        != string.Empty &&
                        tmp.Length < length)
                    {
                        ret    = tmp;
                        length = tmp.Length;
                    }
                }

                if (y          < 3    &&
                    hash[DOWN] >= 'b' &&
                    hash[DOWN] <= 'f')
                {
                    string tmp = FindShortestPath(x, y + 1, input + "D");
                    if (tmp        != string.Empty &&
                        tmp.Length < length)
                    {
                        ret    = tmp;
                        length = tmp.Length;
                    }
                }

                if (y        > 0    &&
                    hash[UP] >= 'b' &&
                    hash[UP] <= 'f')
                {
                    string tmp = FindShortestPath(x, y - 1, input + "U");
                    if (tmp        != string.Empty &&
                        tmp.Length < length)
                    {
                        ret    = tmp;
                        length = tmp.Length;
                    }
                }

                if (x          > 0    &&
                    hash[LEFT] >= 'b' &&
                    hash[LEFT] <= 'f')
                {
                    string tmp = FindShortestPath(x - 1, y, input + "L");
                    if (tmp        != string.Empty &&
                        tmp.Length < length)
                    {
                        ret = tmp;
                    }
                }

                return ret;
            }

            private static int FindLongestPathLength(int x, int y, string input, int length)
            {
                if (x == 3 &&
                    y == 3)
                {
                    return length;
                }

                int    ret  = int.MinValue;
                string hash = CalculateHash(input);

                if (x           < 3    &&
                    hash[RIGHT] >= 'b' &&
                    hash[RIGHT] <= 'f')
                {
                    ret = FindLongestPathLength(x + 1, y, input + "R", length + 1);
                }

                if (y          < 3    &&
                    hash[DOWN] >= 'b' &&
                    hash[DOWN] <= 'f')
                {
                    int tmp = FindLongestPathLength(x, y + 1, input + "D", length + 1);
                    ret = Math.Max(tmp, ret);
                }

                if (y        > 0    &&
                    hash[UP] >= 'b' &&
                    hash[UP] <= 'f')
                {
                    int tmp = FindLongestPathLength(x, y - 1, input + "U", length + 1);
                    ret = Math.Max(tmp, ret);
                }

                if (x          > 0    &&
                    hash[LEFT] >= 'b' &&
                    hash[LEFT] <= 'f')
                {
                    int tmp = FindLongestPathLength(x - 1, y, input + "L", length + 1);
                    ret = Math.Max(tmp, ret);
                }

                return ret;
            }

            public static string Day17_1()
            {
                return FindShortestPath(0, 0, INPUT);
            }

            public static int Day17_2()
            {
                return FindLongestPathLength(0, 0, INPUT, 0);
            }
        }
    }
}
