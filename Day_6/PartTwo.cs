using System.Text.RegularExpressions;

namespace AdventOfCode.DaySix
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

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName).ToList();

            var raceTime = Int64.Parse(Regex.Split(lines[0], @"\D+").Aggregate((x, y) => x + y));
            var raceDistance = Int64.Parse(Regex.Split(lines[1], @"\D+").Aggregate((x, y) => x + y));

            long ways = 0;

            // Populate speeds and times list, and reverse the times list
            var speeds = CreateRange(1, raceTime - 1).ToList();

            var times = CreateRange(1, raceTime - 1).Reverse().ToList();

            for (var y = 0; y < speeds.Count; y++)
            {
                if (speeds[y] * times[y] > raceDistance)
                {
                    ways++;
                }
            }

            // Answer is the number of ways for each race multiplied with eachother
            return ways;
        }
    }
}