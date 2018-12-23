using System;
using System.IO;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static void Main()
        {
            const string PATH  = @"C:\Users\Stefano\Documents\input.txt";
            var          input = File.ReadAllLines(PATH);

            Console.WriteLine(Day20.Day20_2(input));
        }
    }
}
