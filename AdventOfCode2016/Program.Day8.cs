using System;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day8
        {
            private class Display
            {
                private const int WIDTH = 50;
                private const int HEIGTH = 6;

                private bool[,] _pixels { get; set; }

                public Display()
                {
                    _pixels = new bool[WIDTH, HEIGTH];
                }

                public void Rect(int x, int y)
                {
                    for (int i = 0; i < x; i++)
                    for (int j = 0; j < y; j++)
                    {
                        _pixels[i, j] = true;
                    }
                }

                public void RotateRow(int y, int amount)
                {
                    var tmpRow = new bool[WIDTH];

                    for (int i = 0; i < WIDTH; i++)
                    {
                        tmpRow[i] = _pixels[i, y];
                    }

                    for (int i = 0; i < WIDTH; i++)
                    {
                        _pixels[(i + amount) % WIDTH, y] = tmpRow[i];
                    }
                }

                public void RotateColumn(int x, int amount)
                {
                    var tmpCol = new bool[HEIGTH];

                    for (int i = 0; i < HEIGTH; i++)
                    {
                        tmpCol[i] = _pixels[x, i];
                    }

                    for (int i = 0; i < HEIGTH; i++)
                    {
                        _pixels[x, (i + amount) % HEIGTH] = tmpCol[i];
                    }
                }

                public int LitPixels()
                {
                    return _pixels.Cast<bool>().Count(pixel => pixel);
                }

                public void Print()
                {
                    for (int i = 0; i < HEIGTH; i++)
                    {
                        for (int j = 0; j < WIDTH; j++)
                        {
                            Console.Write(_pixels[j, i] ? "#" : " ");
                        }

                        Console.WriteLine();
                    }
                }
            }

            public static int Day8_1(string[] input)
            {
                var display = new Display();

                var comRegex = new Regex(@"\D+");
                var opRegex  = new Regex(@"(\d+)\D+?(\d+)");

                foreach (string s in input)
                {
                    string command = comRegex.Match(s).Value;
                    int    op1     = int.Parse(opRegex.Match(s).Groups[1].Value);
                    int    op2     = int.Parse(opRegex.Match(s).Groups[2].Value);

                    switch (command)
                    {
                        case "rect ":
                            display.Rect(op1, op2);
                            break;

                        case "rotate column x=":
                            display.RotateColumn(op1, op2);
                            break;

                        case "rotate row y=":
                            display.RotateRow(op1, op2);
                            break;
                    }
                }

                return display.LitPixels();
            }

            public static void Day8_2(string[] input)
            {
                var display = new Display();

                var comRegex = new Regex(@"\D+");
                var opRegex  = new Regex(@"(\d+)\D+?(\d+)");

                foreach (string s in input)
                {
                    string command = comRegex.Match(s).Value;
                    int    op1     = int.Parse(opRegex.Match(s).Groups[1].Value);
                    int    op2     = int.Parse(opRegex.Match(s).Groups[2].Value);

                    switch (command)
                    {
                        case "rect ":
                            display.Rect(op1, op2);
                            break;

                        case "rotate column x=":
                            display.RotateColumn(op1, op2);
                            break;

                        case "rotate row y=":
                            display.RotateRow(op1, op2);
                            break;
                    }

                    Thread.Sleep(7);
                    Console.Clear();
                    display.Print();
                }
            }
        }
    }
}
