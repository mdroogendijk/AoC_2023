using System.Text.RegularExpressions;

namespace AdventOfCode.DayOne
{
    public class PartTwo
    {
        public static int GetAnswer(string fileName)
        {
            int answer;
            var lines = File.ReadLines(fileName);

            answer = Solve(@"\d|one|two|three|four|five|six|seven|eight|nine");

            int Solve(string rx) => (
                from line in lines
                let first = Regex.Match(line, rx)
                let last = Regex.Match(line, rx, RegexOptions.RightToLeft)
                select ParseMatch(first.Value) * 10 + ParseMatch(last.Value)
            ).Sum();

            int ParseMatch(string st) => st switch
            {
                "one" => 1,
                "two" => 2,
                "three" => 3,
                "four" => 4,
                "five" => 5,
                "six" => 6,
                "seven" => 7,
                "eight" => 8,
                "nine" => 9,
                var d => int.Parse(d)
            };

            return answer;
        }
    }
}