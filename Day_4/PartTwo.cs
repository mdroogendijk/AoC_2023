namespace AdventOfCode.DayFour
{
    public class PartTwo
    {
        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName);

            var cards = new Dictionary<int, int>();

            // Loop over lines to initiate dictionary
            for (var i = 0; i < lines.Count(); i++)
            {
                cards.Add(i, 1);
            }

            var counts = lines.Select(_ => 1).ToArray();

            // Loop over lines
            for (var i = 0; i < lines.Count(); i++)
            { 
                var line = lines.ElementAt(i);

                // Split into int array with start and end sections
                var winningNumbers = line.Substring(line.IndexOf(':'), line.IndexOf('|') - line.IndexOf(':')).Split(' ');
                var myNumbers = line.Substring(line.IndexOf('|')).Split(' ');

                // Select winning numbers and not empty strings
                var matching = myNumbers.Where(x => winningNumbers.Contains(x) && !String.IsNullOrWhiteSpace(x));

                var (card, count) = (cards[i], counts[i]);
                for (var j = 0; j < matching.Count(); j++)
                {
                    counts[i + j + 1] += count;
                }                
            }

            // Answer is the number of scratch cards at the end
            return counts.Sum();
        }
    }
}