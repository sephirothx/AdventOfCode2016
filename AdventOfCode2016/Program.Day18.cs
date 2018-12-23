using System.Linq;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day18
        {
            private const string INPUT = "^.^^^..^^...^.^..^^^" +
                                         "^^.....^...^^^..^^^^" +
                                         ".^^.^^^^^^^^.^^.^^^^" +
                                         "...^^...^^^^.^.^..^^" +
                                         "..^..^.^^.^.^.......";

            private const int ROWS = 40;

            private static (string row, int count) DetermineNextRow(string row)
            {
                string ret   = string.Empty;
                int    count = 0;

                row = "." + row + ".";

                for (int i = 0; i < (row.Length - 2); i++)
                {
                    string tmp = row.Substring(i, 3);

                    if (tmp == "^^." ||
                        tmp == "^.." ||
                        tmp == ".^^" ||
                        tmp == "..^")
                    {
                        ret += "^";
                    }
                    else
                    {
                        count++;
                        ret += ".";
                    }
                }

                return (ret, count);
            }

            public static int Day18_1()
            {
                string row   = INPUT;
                int    count = row.Count(c => c == '.');

                for (int i = 1; i < ROWS; i++)
                {
                    int tmp;
                    (row, tmp) =  DetermineNextRow(row);
                    count      += tmp;
                }

                return count;
            }

            public static int Day18_2()
            {
                string row   = INPUT;
                int    count = row.Count(c => c == '.');

                for (int i = 1; i < 400000; i++)
                {
                    int tmp;
                    (row, tmp) =  DetermineNextRow(row);
                    count      += tmp;
                }

                return count;
            }
        }
    }
}
