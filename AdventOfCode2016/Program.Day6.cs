using System.Collections.Generic;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day6
        {
            private const int NUM_CHAR       =  8;
            private const int NUM_DICTIONARY = 26;

            public static string Day6_1(IEnumerable<string> input)
            {
                var occurrences = new int[NUM_CHAR, NUM_DICTIONARY];
                var ret = "";

                foreach (string s in input)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        occurrences[i, s[i] - 'a']++;
                    }
                }

                for (int i = 0; i < NUM_CHAR; i++)
                {
                    (int maxOccurrences, char character) decodedChar= (int.MinValue, '\0');
                    for (int j = 0; j < NUM_DICTIONARY; j++)
                    {
                        decodedChar = occurrences[i, j] > decodedChar.maxOccurrences
                                          ? (occurrences[i, j], (char)('a' + j))
                                          : decodedChar;

                    }

                    ret += decodedChar.character;
                }

                return ret;
            }

            public static string Day6_2(IEnumerable<string> input)
            {
                var occurrences = new int[NUM_CHAR, NUM_DICTIONARY];
                var ret = "";

                foreach (string s in input)
                {
                    for (int i = 0; i < s.Length; i++)
                    {
                        occurrences[i, s[i] - 'a']++;
                    }
                }

                for (int i = 0; i < NUM_CHAR; i++)
                {
                    (int minOccurrences, char character) decodedChar = (int.MaxValue, '\0');
                    for (int j = 0; j < NUM_DICTIONARY; j++)
                    {
                        decodedChar = occurrences[i, j] < decodedChar.minOccurrences
                                          ? (occurrences[i, j], (char)('a' + j))
                                          : decodedChar;

                    }

                    ret += decodedChar.character;
                }

                return ret;
            }
        }
    }
}
