using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AdventOfCode.DayFive
{
    public class PartTwo
    {
        public static IEnumerable<long> CreateRange(long start, long count)
        {
            var limit = start + count;

            while (start < limit)
            {
                yield return start;
                start++;
            }
        }

        public static long SearchMap(long currentNumber, IEnumerable<Tuple<long, long, int>> maps)
        {
            long newNumber = currentNumber;
            try
            {
                newNumber = maps
                    .Where(x => currentNumber >= x.Item2 && currentNumber <= x.Item2 + x.Item3)
                    .Select(x => x.Item1 + (currentNumber - x.Item2)).First();
            }
            catch
            {
            }
            ;

            return newNumber;
        }

        public static string GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName).ToList();

            var seedRanges = Regex.Split(lines[0], @"\D+").Where(x => !String.IsNullOrWhiteSpace(x));

            var seeds = new List<long>();

            for (var i = 0; i < seedRanges.Count(); i = i + 2)
            {
                seeds.AddRange(CreateRange(Int64.Parse(seedRanges.ElementAt(i)), Int64.Parse(seedRanges.ElementAt(i + 1))));
            }

            var locations = new List<long>();

            // Find all map headers 
            var mapHeaders = Enumerable.Range(0, lines.Count)
                .Skip(1).Where(i => lines[i].Contains(':'))
                .ToList();

            var maps = new List<IEnumerable<Tuple<long, long, int>>>();

            for (var i = 0; i < mapHeaders.Count; i++)
            {
                var firstEntry = mapHeaders[i] + 1;

                var length = 0;

                if (mapHeaders[i] == mapHeaders.Last())
                {
                    length = lines.Count - firstEntry;
                }
                else
                {
                    length = mapHeaders[i + 1] - mapHeaders[i] - 2;
                }
                var map = lines
                    .GetRange(firstEntry, length)
                    .Select(n => new { values = n.Split(' ') })
                    .Select(x => new Tuple<long, long, int>(Int64.Parse(x.values[0]), Int64.Parse(x.values[1]), Int32.Parse(x.values[2])));
                
                maps.Add(map);
            }

            foreach (var seed in seeds)
            {
                var currentNumber = seed;

                foreach (var map in maps) 
                { 
                    var newNumber = SearchMap(currentNumber, map);

                    currentNumber = newNumber;

                    if (map == maps.Last())
                    {
                        locations.Add(currentNumber);
                    }
                }
            }

            // Answer is the lowest location number
            return locations.Min().ToString();
        }
    }
}