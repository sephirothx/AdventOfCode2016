using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day4
        {
            private class Room
            {
                private string Name { get; }
                public int ID { get; }
                private string CheckSum { get; }

                public Room(string name, int id, string checkSum)
                {
                    ID       = id;
                    Name     = name;
                    CheckSum = checkSum;
                }

                public bool IsReal()
                {
                    var orderedName = Name.Replace("-", "")
                                          .OrderByDescending(p => Name.Count(c => c == p))
                                          .ThenBy(p => p);
                    string expectedCheckSum = new string(orderedName.Distinct().ToArray()).Substring(0, 5);

                    return CheckSum == expectedCheckSum;
                }

                public string Decrypt()
                {
                    string ret = Name.Aggregate("", (agg, c) => agg + (char)((c - 'a' + ID) % 26 + 'a'));
                    return ret;
                }
            }

            public static int Day4_1(string[] input)
            {
                var regex = new Regex(@"([\w\-]+)-(\d+)\[(\w+)\]");
                var rooms = input.Select(p =>
                                         {
                                             var match = regex.Match(p);
                                             return new Room(match.Groups[1].Value,
                                                             int.Parse(match.Groups[2].Value),
                                                             match.Groups[3].Value);
                                         }).ToArray();
                int ret = rooms.Where(room => room.IsReal()).Sum(room => room.ID);

                return ret;
            }

            public static int Day4_2(string[] input)
            {
                var regex = new Regex(@"([\w\-]+)-(\d+)\[(\w+)\]");
                var rooms = input.Select(p =>
                                         {
                                             var match = regex.Match(p);
                                             return new Room(match.Groups[1].Value,
                                                             int.Parse(match.Groups[2].Value),
                                                             match.Groups[3].Value);
                                         }).ToArray();

                return (from room in rooms
                        where room.Decrypt().Contains("pole")
                        select room.ID).FirstOrDefault();
            }
        }
    }
}
