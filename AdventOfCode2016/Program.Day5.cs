using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day5
        {
            private static string CalculateHash(string input)
            {
                var md5 = MD5.Create();
                var inputBytes = Encoding.ASCII.GetBytes(input);
                var hashBytes = md5.ComputeHash(inputBytes);

                return hashBytes.Aggregate("", (current, t) => current + t.ToString("X2"));
            }

            public static string Day5_1()
            {
                string input = Console.ReadLine() ?? "";
                string ret = "";

                for (int i = 0; i < int.MaxValue; i++)
                {
                    string tmp = input + i;
                    string hash = CalculateHash(tmp);

                    if (hash.Substring(0, 5) == "00000") 
                    {
                        ret += hash[5];

                        if (ret.Length == 8)
                        {
                            return ret;
                        }
                    }
                }

                return "";
            }

            public static string Day5_2()
            {
                string input = Console.ReadLine() ?? "";
                var ret = "________".ToCharArray();
                int completed = 0;

                for (int i = 0; i < int.MaxValue; i++)
                {
                    string tmp = input + i;
                    string hash = CalculateHash(tmp);

                    if (hash.Substring(0, 5) == "00000")
                    {
                        if (int.TryParse(hash.Substring(5, 1), out int index) &&
                            index      < 8                                    &&
                            ret[index] == '_')
                        {
                            Console.WriteLine(tmp + " = " + hash);
                            ret[index] = hash[6];
                            completed++;
                        }

                        if (completed == 8)
                        {
                            return new string(ret);
                        }
                    }
                }

                return new string(ret);
            }
        }
    }
}
