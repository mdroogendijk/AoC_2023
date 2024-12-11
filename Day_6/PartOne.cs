using System.Text.RegularExpressions;

namespace AdventOfCode.DaySix
{
    public class PartOne
    {
        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName).ToList();

            var raceTimes = Regex.Split(lines[0], @"\D+").Where(x => !String.IsNullOrWhiteSpace(x)).Select(Int32.Parse).ToList();
            var raceDistances = Regex.Split(lines[1], @"\D+").Where(x => !String.IsNullOrWhiteSpace(x)).Select(Int32.Parse).ToList();

            var combinedWays = new List<int>();

            // Loop over lines
            for (var i = 0; i < raceTimes.Count; i++)
            {
                var ways = 0;

                // Populate speeds and times list, and reverse the times list
                var speeds = Enumerable.Range(1, raceTimes[i] - 1).ToList();

                var times = Enumerable.Range(1, raceTimes[i] - 1).Reverse().ToList();

                for (var y = 0; y < speeds.Count; y++)
                {
                    if (speeds[y] * times[y] > raceDistances[i])
                    {
                        ways++;
                    }
                }

                combinedWays.Add(ways);
            }
  
            var answer = combinedWays.Aggregate((a, x) => a * x);

            // Answer is the number of ways for each race multiplied with eachother
            return answer;
        }
    }
}