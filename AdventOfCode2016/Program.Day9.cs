using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day9
        {
            private static string DecompressV1(string input)
            {
                var    regex        = new Regex(@"\((\d+)x(\d+)\)");
                string decompressed = "";

                while (regex.IsMatch(input))
                {
                    var match     = regex.Match(input);
                    int seqLength = int.Parse(match.Groups[1].Value);
                    int repeat    = int.Parse(match.Groups[2].Value);
                    int count     = match.Index + match.Length + seqLength;

                    decompressed += input.Substring(0, match.Index);

                    string seq = input.Substring(match.Index + match.Length, seqLength);

                    for (int i = 0; i < repeat; i++)
                    {
                        decompressed += seq;
                    }

                    input = input.Substring(count);
                }

                decompressed += input;
                return decompressed;
            }

            private static string DecompressV2(string input)
            {
                string decompressed = input;
                string tmp;

                do
                {
                    tmp          = decompressed;
                    decompressed = DecompressV1(tmp);
                } while (decompressed != tmp);

                return decompressed;
            }

            private static long DecompressV2Length(string input)
            {
                var  regex = new Regex(@"\((\d+)x(\d+)\)");
                long ret   = 0;

                while (regex.IsMatch(input))
                {
                    var match     = regex.Match(input);
                    int seqLength = int.Parse(match.Groups[1].Value);
                    int repeat    = int.Parse(match.Groups[2].Value);
                    int count     = match.Index + match.Length + seqLength;

                    ret += match.Index +
                           DecompressV2Length(input.Substring(match.Index + match.Length,
                                                              seqLength)) *
                           repeat;

                    input = input.Substring(count);
                }

                ret += input.Length;

                return ret;
            }

            public static int Day9_1(string input)
            {
                string decompressed = DecompressV1(input);

                return decompressed.Length;
            }

            public static long Day9_2(string input)
            {
                return DecompressV2Length(input);
            }
        }
    }
}
