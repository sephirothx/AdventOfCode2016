using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day16
        {
            private const string INPUT = "11101000110010100";

            private static string FlipAndReverse(string input)
            {
                return input.Aggregate("", (agg, c) => (c == '0' ? "1" : "0") + agg);
            }

            private static string CalculateCheckSum(string data)
            {
                string checksum = data;

                while (checksum.Length % 2 == 0)
                {
                    string tmp = "";

                    for (int i = 0; i < checksum.Length; i += 2)
                    {
                        tmp += checksum[i] == checksum[i + 1]
                                   ? "1"
                                   : "0";
                    }

                    checksum = tmp;
                } 

                return checksum;
            }

            public static string Day16_1()
            {
                const int MAX_ALLOWED_DATA = 272;

                string data = INPUT;

                while (data.Length < MAX_ALLOWED_DATA)
                {
                    data = $"{data}0{FlipAndReverse(data)}";
                }

                data = data.Substring(0, MAX_ALLOWED_DATA);
                return CalculateCheckSum(data);
            }

            private static IEnumerable<bool> FlipAndReverse(IEnumerable<bool> input)
            {
                return input.ToArray()
                            .Reverse()
                            .Select(item => !item);
            }

            private static IEnumerable<bool> CalculateCheckSum(IEnumerable<bool> data)
            {
                var checksum = data.Select(item => item)
                                   .ToList();

                while (checksum.Count % 2 == 0)
                {
                    var tmp = new List<bool>();
                    for (int i = 0; i < checksum.Count; i += 2)
                    {
                        tmp.Add(checksum[i] == checksum[i + 1]);
                    }

                    checksum = tmp.Select(item => item)
                                  .ToList();
                }

                return checksum;
            }

            public static string Day16_2()
            {
                const int MAX_ALLOWED_DATA = 35651584;

                var data = new List<bool>();
                foreach (char c in INPUT)
                {
                    data.Add(c == '1');
                }

                while (data.Count < MAX_ALLOWED_DATA)
                {
                    var second = FlipAndReverse(data);
                    data.Add(false);
                    data = data.Concat(second)
                               .ToList();
                }

                data = data.Take(MAX_ALLOWED_DATA).ToList();
                var checksum = CalculateCheckSum(data);

                return checksum.Aggregate("", (agg, b) => agg + (b ? "1" : "0"));
            }
        }
    }
}
