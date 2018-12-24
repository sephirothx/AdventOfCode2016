using System.IO;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static void Main()
        {
            const string PATH  = @"C:\Users\User\Documents\input.txt";
            var          input = File.ReadAllLines(PATH);

            Day21.Day21_2(input);
        }
    }
}
