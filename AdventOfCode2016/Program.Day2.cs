namespace AdventOfCode2016
{
    partial class Program
    {
        public static string Day2_1(string[] input)
        {
            var matrix = new[,]
                         {
                             {1, 4, 7},
                             {1, 5, 8},
                             {1, 6, 9}
                         };
            var x = 1;
            var y = 1;

            var ret = "";

            foreach (var s in input)
            {
                foreach (var c in s)
                {
                    switch (c)
                    {
                        case 'R':
                            x = x < 2 ? x + 1 : x;
                            break;
                        case 'L':
                            x = x > 0 ? x - 1 : x;
                            break;
                        case 'U':
                            y = y > 0 ? y - 1 : y;
                            break;
                        case 'D':
                            y = y < 2 ? y + 1 : y;
                            break;
                    }
                }

                ret += matrix[x, y];
            }

            return ret;
        }

        public static string Day2_2(string[] input)
        {
            var matrix = new[,]
                         {
                             {"-", "-", "5", "-", "-"},
                             {"-", "2", "6", "A", "-"},
                             {"1", "3", "7", "B", "D"},
                             {"-", "4", "8", "C", "-"},
                             {"-", "-", "9", "-", "-"}
                         };
            var x = 0;
            var y = 2;

            var ret = "";

            foreach (var s in input)
            {
                foreach (var c in s)
                {
                    switch (c)
                    {
                        case 'R':
                            x = (x < 4 && matrix[x + 1, y] != "-") ? x + 1 : x;
                            break;
                        case 'L':
                            x = (x > 0 && matrix[x - 1, y] != "-") ? x - 1 : x;
                            break;
                        case 'U':
                            y = (y > 0 && matrix[x, y - 1] != "-") ? y - 1 : y;
                            break;
                        case 'D':
                            y = (y < 4 && matrix[x, y + 1] != "-") ? y + 1 : y;
                            break;
                    }
                }

                ret += matrix[x, y];
            }

            return ret;
        }
    }
}