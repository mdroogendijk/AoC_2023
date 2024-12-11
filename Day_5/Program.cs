using System.Reflection;

namespace AdventOfCode.DayFive
{
    public class Program
    {
        public static List<string> Main()
        {
            var resultSet = new List<string>();

            // Part selector
            bool inputPartOne = true;
            bool inputPartTwo = true;

            if (inputPartOne)
            {
                string answerPartOne = PartOne.GetAnswer("Day_5/Input/input.txt");
                resultSet.Add($"The answer for the input file in Part 1 = {answerPartOne}");
            }

            if (inputPartTwo)
            {
                var dayInstance = new Day05();

                Console.WriteLine("Part 1");
                var partOneAnswer = dayInstance.Part1();
                Console.WriteLine(partOneAnswer);

                Console.WriteLine("Part 2");
                var partTwoAnswer = dayInstance.Part2();
                Console.WriteLine(partTwoAnswer);
            }

            return resultSet;
        }
    }
}