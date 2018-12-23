using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day14
        {
            private const string INPUT = "jlmsuwbz";

            private static string CalculateHash(string input)
            {
                var md5        = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hashBytes  = md5.ComputeHash(inputBytes);

                return hashBytes.Aggregate("", (current, t) => current + t.ToString("x2"));
            }

            private static string CalculateHash2016(string input)
            {
                string ret = CalculateHash(input);
                for (int i = 0; i < 2016; i++)
                {
                    ret = CalculateHash(ret);
                }

                return ret;
            }

            public static int Day14_1()
            {
                var regex3           = new Regex(@"(.)\1\1");
                var regex5           = new Regex(@"(.)\1\1\1\1");
                var threeConsecutive = new List<(string key, int index)>();
                var fiveConsecutive  = new List<(string key, int index)>();

                var count = 0;

                for (int index = 0; index < int.MaxValue; index++)
                {
                    string tmp  = INPUT + index;
                    string hash = CalculateHash(tmp);

                    var match5 = regex5.Match(hash);
                    if (match5.Success)
                    {
                        count++;
                        fiveConsecutive.Add((match5.Groups[1].Value,
                                             index));

                        if (count == 10)
                            break;
                    }

                    var match3 = regex3.Match(hash);
                    if (match3.Success)
                    {
                        threeConsecutive.Add((match3.Groups[1].Value,
                                              index));
                    }
                }

                count = 0;

                foreach (var three in threeConsecutive)
                foreach (var five in fiveConsecutive)
                {
                    if (five.index > three.index + 1000)
                    {
                        break;
                    }

                    if (five.key   == three.key  &&
                        five.index > three.index &&
                        five.index <= three.index + 1000)
                    {
                        count++;

                        if (count == 64)
                        {
                            return three.index;
                        }

                        break;
                    }
                }

                return 0;
            }

            public static int Day14_2()
            {
                var regex3           = new Regex(@"(.)\1\1");
                var regex5           = new Regex(@"(.)\1\1\1\1");
                var threeConsecutive = new List<(string key, int index)>();
                var fiveConsecutive  = new List<(string key, int index)>();

                var count = 0;

                for (int index = 0; index < int.MaxValue; index++)
                {
                    string tmp  = INPUT + index;
                    string hash = CalculateHash2016(tmp);

                    var match5 = regex5.Match(hash);
                    if (match5.Success)
                    {
                        count++;
                        fiveConsecutive.Add((match5.Groups[1].Value,
                                             index));

                        if (count == 15)
                            break;
                    }

                    var match3 = regex3.Match(hash);
                    if (match3.Success)
                    {
                        threeConsecutive.Add((match3.Groups[1].Value,
                                              index));
                    }
                }

                count = 0;

                foreach (var three in threeConsecutive)
                foreach (var five in fiveConsecutive)
                {
                    if (five.index > three.index + 1000)
                    {
                        break;
                    }

                    if (five.key   == three.key  &&
                        five.index > three.index &&
                        five.index <= three.index + 1000)
                    {
                        count++;
                        if (count == 64)
                        {
                            return three.index;
                        }

                        break;
                    }
                }

                return 0;
            }
        }
    }
}
