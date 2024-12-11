using System.Text;

namespace AdventOfCode.DayFifteen
{
    public class PartOne
    {
        public static int Hash(string input) => input.Aggregate(0, (x, y) => (((x + y) * 17) % 256));

        public static int GetAnswer(string fileName)
        {
            List<string> puzzleInput = File.ReadAllText(fileName).Split(',').ToList();
            int answer = puzzleInput.Sum(Hash);

            return answer;
        }
    }
}