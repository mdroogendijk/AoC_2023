namespace AdventOfCode
{
    public class Program
    {
        public static void Main()
        {
            // Day selector
            int day = 15;

            // Solutions to run for selected day
            var resultSet = day switch
            {
                1 => DayOne.Program.Main(),
                2 => DayTwo.Program.Main(),
                3 => DayThree.Program.Main(),
                4 => DayFour.Program.Main(),
                5 => DayFive.Program.Main(),
                6 => DaySix.Program.Main(),
                7 => DaySeven.Program.Main(),
                8 => DayEight.Program.Main(),
                9 => DayNine.Program.Main(),
                15 => DayFifteen.Program.Main(),
                _ => new List<string>()
            };

            Console.WriteLine($"The results of day {day}:");

            // Write results
            if (resultSet.Any())
            {
                foreach (string result in resultSet) Console.WriteLine(result);
            }
        }
    }
}