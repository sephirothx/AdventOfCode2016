using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        public static int Day3_1(string[] input)
        {
            var triangles = new List<int[]>();
            foreach (var s in input)
            {
                var matches = Regex.Matches(s, @"(\d+)");
                var tmp = new int[3];
                var i = 0;
                foreach (Match match in matches)
                {
                    tmp[i++] = int.Parse(match.Value);
                }

                triangles.Add(tmp);
            }

            var realTriangles = triangles.Where(p => Math.Abs(p[0] - p[1]) < p[2] && p[0] + p[1] > p[2]);
            var ret = realTriangles.Count();

            return ret;
        }

        public static int Day3_2(string[] input)
        {
            var triangles = new List<int[]>();
            int i = 0,
                j = 0;
            var tmp = new int[3][];

            foreach (var s in input)
            {
                var matches = Regex.Matches(s, @"(\d+)");
                foreach (Match match in matches)
                {
                    if (j == 0)
                        tmp[i] = new int[3];
                    tmp[i++][j] = int.Parse(match.Value);
                }

                i = 0;
                j = (++j) % 3;

                if (j != 0)
                    continue;
                foreach (var tr in tmp)
                {
                    triangles.Add(tr);
                }

                tmp = new int[3][];
            }

            var realTriangles = triangles.Where(p => Math.Abs(p[0] - p[1]) < p[2] && p[0] + p[1] > p[2]);
            var ret = realTriangles.Count();

            return ret;
        }
    }
}