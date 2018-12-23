using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace AdventOfCode2016
{
    partial class Program
    {
        private static class Day20
        {
            private static (bool overlap, (uint, uint)? first, (uint, uint)? second)
                OverlapIntervals((uint min, uint max) valid,
                                 (uint min, uint max) banned)
            {
                if (valid.min > banned.max ||
                    valid.max < banned.min)
                {
                    return (false, null, null);
                }

                var first = valid.min < banned.min
                                ? (valid.min, banned.min - 1)
                                : ((uint, uint)?)null;

                var second = valid.max > banned.max
                                 ? (banned.max + 1, valid.max)
                                 : ((uint, uint)?)null;

                return (true, first, second);
            }

            private static List<(uint min, uint max)> FilterIPs(IEnumerable<(uint min, uint max)> bannedIPs,
                                                                List<(uint min, uint max)> validIPs)
            {
                foreach (var banned in bannedIPs)
                {
                    var newValidIPs = new List<(uint min, uint max)>(validIPs);

                    foreach (var valid in validIPs)
                    {
                        bool          overlap;
                        (uint, uint)? first;
                        (uint, uint)? second;

                        (overlap, first, second) = OverlapIntervals(valid, banned);

                        if (overlap)
                        {
                            newValidIPs.Remove(valid);
                            if (first  != null) newValidIPs.Add(first.Value);
                            if (second != null) newValidIPs.Add(second.Value);
                        }
                    }

                    validIPs = newValidIPs;
                }

                return validIPs;
            }

            public static uint Day20_1(IEnumerable<string> input)
            {
                var regex = new Regex(@"(\d+)-(\d+)");

                var bannedIPs = input.Select(line => regex.Match(line))
                                     .Select(match => (min: uint.Parse(match.Groups[1].Value),
                                                       max: uint.Parse(match.Groups[2].Value)))
                                     .ToList();

                var validIPs = new List<(uint min, uint max)>
                               {
                                   (uint.MinValue, uint.MaxValue)
                               };

                validIPs = FilterIPs(bannedIPs, validIPs);

                return validIPs.Min(interval => interval.min);
            }

            public static long Day20_2(IEnumerable<string> input)
            {
                var regex = new Regex(@"(\d+)-(\d+)");

                var bannedIPs = input.Select(line => regex.Match(line))
                                     .Select(match => (min: uint.Parse(match.Groups[1].Value),
                                                       max: uint.Parse(match.Groups[2].Value)))
                                     .ToList();

                var validIPs = new List<(uint min, uint max)>
                               {
                                   (uint.MinValue, uint.MaxValue)
                               };

                validIPs = FilterIPs(bannedIPs, validIPs);

                return validIPs.Sum(interval => interval.max - interval.min + 1);
            }
        }
    }
}
