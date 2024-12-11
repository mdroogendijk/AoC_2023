namespace AdventOfCode.DayFour
{
    public class PartOne
    {
        public static int GetAnswer(string fileName)
        {
            int totalScore = 0;            

            var lines = File.ReadLines(fileName);

            // Loop over lines
            foreach (string line in lines)
            {
                // Split into int array with start and end sections
                var winningNumbers = line.Substring(line.IndexOf(':'), line.IndexOf('|') - line.IndexOf(':')).Split(' ');
                var myNumbers = line.Substring(line.IndexOf('|')).Split(' ');
               
                // Select winning numbers and not empty strings
                var matching = myNumbers.Where(x => winningNumbers.Contains(x) && !String.IsNullOrWhiteSpace(x));
                var score = 1;

                if (matching.Any() && matching.Count() > 1) 
                {
                    for (var i = 2; i <= matching.Count();i++)
                    {
                        score *= 2;
                    }
                    totalScore += score;
                }
                else if (matching.Any())
                {
                    totalScore += score;
                }
            }

            // Answer is the number of pairs having fully overlapping sections
            return totalScore;
        }
    }
}