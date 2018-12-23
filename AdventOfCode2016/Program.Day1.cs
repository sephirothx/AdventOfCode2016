using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{
    partial class Program
    {
        public static int Day1_1(string input)
        {
            input = input.Replace(", ", ",");
            var directions = input.Split(',').ToArray();
            var x = 0;
            var y = 0;
            var vectX = 0;
            var vectY = 1;

            foreach (var direction in directions)
            {
                var turn = direction.Substring(0, 1);
                var dist = int.Parse(direction.Substring(1));
                if (turn == "R")
                {
                    var tmp = vectX;
                    vectX = vectY;
                    vectY = -tmp;
                }
                else if (turn == "L")
                {
                    var tmp = vectX;
                    vectX = -vectY;
                    vectY = tmp;
                }

                x += (vectX * dist);
                y += (vectY * dist);
            }

            return Math.Abs(x) + Math.Abs(y);
        }

        public static int Day1_2(string input)
        {
            input = input.Replace(", ", ",");
            var directions = input.Split(',').ToArray();
            var x = 0;
            var y = 0;
            var vectX = 0;
            var vectY = 1;
            var set = new HashSet<string>();

            while (true)
            {
                foreach (var direction in directions)
                {
                    var turn = direction.Substring(0, 1);
                    var dist = int.Parse(direction.Substring(1));

                    if (turn == "R")
                    {
                        var tmp = vectX;
                        vectX = vectY;
                        vectY = -tmp;
                    }
                    else if (turn == "L")
                    {
                        var tmp = vectX;
                        vectX = -vectY;
                        vectY = tmp;
                    }

                    for (int i = 0; i < dist; i++)
                    {
                        var str = $"{x} {y}";

                        if (set.Contains(str))
                            return Math.Abs(x) + Math.Abs(y);
                        else
                            set.Add(str);

                        x += (vectX);
                        y += (vectY);
                    }
                }
            }
        }
    }
}