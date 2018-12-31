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

            Console.WriteLine(DateTime.Now.TimeOfDay);
            Day25.Day25_1(input);
            Console.WriteLine(DateTime.Now.TimeOfDay);
        }
    }
}
