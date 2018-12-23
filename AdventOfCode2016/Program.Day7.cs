using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day7
        {
            private class IP
            {
                private string _supernet { get; }
                private string _hypernet { get; }

                public IP(string sequence, string hypernet)
                {
                    _supernet = sequence;
                    _hypernet = hypernet;
                }

                public bool SupportsTLS()
                {
                    return HasABBA(_supernet) && !HasABBA(_hypernet);
                }

                public bool SupportsSSL()
                {
                    var aba = GetAbaList(_supernet);
                    var bab = GetAbaHashSet(_hypernet);

                    return aba.Any(s => bab.Contains(MakeBAB(s)));
                }

                private static bool HasABBA(string input)
                {
                    for (int i = 0; i < input.Length - 3; i++)
                    {
                        if (input[i]     == input[i + 3] &&
                            input[i + 1] == input[i + 2] &&
                            input[i]     != input[i + 1])
                        {
                            return true;
                        }
                    }

                    return false;
                }

                private static HashSet<string> GetAbaHashSet(string input)
                {
                    var tmp = new HashSet<string>();
                    for (int i = 0; i < input.Length - 2; i++)
                    {
                        if (input[i] == input[i + 2] &&
                            input[i] != input[i + 1])
                        {
                            tmp.Add(input.Substring(i, 3));
                        }
                    }

                    return tmp;
                }

                private static IEnumerable<string> GetAbaList(string input)
                {
                    var tmp = new List<string>();
                    for (int i = 0; i < input.Length - 2; i++)
                    {
                        if (input[i] == input[i + 2] &&
                            input[i] != input[i + 1])
                        {
                            tmp.Add(input.Substring(i, 3));
                        }
                    }

                    return tmp;
                }

                private static string MakeBAB(string input)
                {
                    return input[1].ToString() +
                           input[0]            +
                           input[1];
                }
            }

            public static int Day7_1(IEnumerable<string> input)
            {
                var regex = new Regex(@"[^[\]]+");
                var IPs   = new List<IP>();
                int ret   = 0;

                foreach (string ip in input)
                {
                    var    matches  = regex.Matches(ip);
                    string sequence = "";
                    string hypernet = "";
                    int    i        = 0;

                    foreach (Match match in matches)
                    {
                        if (i++ % 2 == 0)
                        {
                            sequence += match.Value + ".";
                        }
                        else
                        {
                            hypernet += match.Value + ".";
                        }
                    }

                    IPs.Add(new IP(sequence, hypernet));
                }

                foreach (var ip in IPs)
                {
                    ret += ip.SupportsTLS()
                               ? 1
                               : 0;
                }

                return ret;
            }

            public static int Day7_2(IEnumerable<string> input)
            {
                var regex = new Regex(@"[^[\]]+");
                var IPs   = new List<IP>();
                int ret   = 0;

                foreach (string ip in input)
                {
                    var    matches  = regex.Matches(ip);
                    string sequence = "";
                    string hypernet = "";
                    int    i        = 0;

                    foreach (Match match in matches)
                    {
                        if (i++ % 2 == 0)
                        {
                            sequence += match.Value + ".";
                        }
                        else
                        {
                            hypernet += match.Value + ".";
                        }
                    }

                    IPs.Add(new IP(sequence, hypernet));
                }

                foreach (var ip in IPs)
                {
                    ret += ip.SupportsSSL()
                               ? 1
                               : 0;
                }

                return ret;
            }
        }
    }
}
