using System.Security.Cryptography;
using System.Text.RegularExpressions;

namespace AdventOfCode.DayFive
{
    public class PartOne
    {
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

            var seeds = Regex.Split(lines[0], @"\D+").Where(x => !String.IsNullOrWhiteSpace(x));

            var locations = new List<long>();

            // Find all map headers 
            var mapHeaders = Enumerable.Range(0, lines.Count)
                .Skip(1).Where(i => lines[i].Contains(':'))
                .ToList();

            foreach (var seed in seeds)
            {
                var currentNumber = Int64.Parse(seed);

                for (var i = 0; i < mapHeaders.Count; i++)
                {
                    var firstEntry = mapHeaders[i] +1;

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
                        //.SkipWhile(y => y.values.All(s => String.IsNullOrWhiteSpace(s)))
                        .Select(x => new Tuple<long, long, int>(Int64.Parse(x.values[0]), Int64.Parse(x.values[1]), Int32.Parse(x.values[2])));

                    var newNumber = SearchMap(currentNumber, map);

                    currentNumber = newNumber;

                    if (mapHeaders[i] == mapHeaders.Last())
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