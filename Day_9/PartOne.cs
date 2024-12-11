using System.Text.RegularExpressions;

namespace AdventOfCode.DayNine
{
    public class PartOne
    {
        private static List<int[]> FormHistory(string report)
        {
            var initial = report.Split(' ').Where(x => !String.IsNullOrWhiteSpace(x))
                .Select(x => Int32.Parse(x)).ToArray();

            var sequences = new List<int[]> { initial };

            while (sequences[^1].Any(v => v != 0))
            {
                sequences.Add(item: sequences[^1]
                    .Skip(1)
                    .Select((val, i) => val - sequences[^1][i])
                    .ToArray());
            }

            return sequences;
        }

        private static int ExtrapolateForwards(IList<int[]> sequences)
        {
            return sequences
                .Reverse()
                .Skip(1)
                .Aggregate(seed: 0, func: (n, seq) => n + seq[^1]);
        }

        public static int ExtrapolateBackwards(IList<int[]> sequences)
        {
            return sequences
                .Reverse()
                .Skip(1)
                .Aggregate(seed: 0, func: (n, seq) => seq[0] - n);
        }

        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName).ToList();
            var histories = lines.Select(FormHistory);

            var answer = histories.Sum(ExtrapolateForwards);

            // Answer is the combined number of next value for histories
            return answer;
        }
    }
}