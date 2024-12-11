namespace AdventOfCode.DayOne
{
    public class PartOne
    {
        public static int GetAnswer(string fileName)
        {
            int answer;
            int calibrationValue = 0;
            var allCalibrationValues = new List<int>();

            var lines = File.ReadLines(fileName);
            int linesCount = lines.Count();

            // Loop over lines
            for (int count = 0; count < linesCount; count++)
            {
                string line = lines.ElementAt(count);

                // Get calibration value for line
                if (!string.IsNullOrWhiteSpace(line))
                {
                    var digits = line.Where(Char.IsDigit);

                    calibrationValue = Int32.Parse(String.Concat(Char.GetNumericValue(digits.First()), Char.GetNumericValue(digits.Last())));
                    allCalibrationValues.Add(calibrationValue);
                }

                // Last line in file
                if ((count + 1) == linesCount)
                {
                    
                }
            }

            // Select sum of values from the list
            answer = allCalibrationValues.Sum();

            return answer;
        }
    }
}