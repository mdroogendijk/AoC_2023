namespace AdventOfCode.DayOne
{
    public class PartTwoIncorrect
    {
        public static int GetAnswer(string fileName)
        {
            int answer;
            int calibrationValue = 0;
            var allCalibrationValues = new List<int>();

            var spelledOut = new Dictionary<string, int>()
                {
                    { "one",1 },
                    { "two",2 },
                    { "three",3 },
                    { "four",4},
                    { "five",5},
                    {"six",6},
                    {"seven",7},
                    { "eight",8},
                    { "nine",9}
                };

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
                    int first = 0;
                    int last = 0;
                    var foundSpelledOut = new Dictionary<int, int>();

                    foreach (var word in spelledOut)
                    {
                        if (line.Contains(word.Key))
                        {
                            // Add first occurrence
                            foundSpelledOut.Add(line.IndexOf(word.Key), word.Value);
                            // Add last occurrence if not the same index as the first
                            if (!foundSpelledOut.ContainsKey(line.IndexOf(word.Key)))
                            {
                                foundSpelledOut.Add(line.LastIndexOf(word.Key), word.Value);
                            }
                        }
                    }

                    if (foundSpelledOut.Any() && digits.Any() && line.IndexOf(digits.First()) > foundSpelledOut.Select(x => x.Key).Min())
                    {
                        first = foundSpelledOut[foundSpelledOut.Select(x => x.Key).Min()];
                    }
                    else if (!digits.Any())
                    {
                        first = foundSpelledOut[foundSpelledOut.Select(x => x.Key).Min()];
                    }
                    else
                    {
                        first = (int)Char.GetNumericValue(digits.First()); 
                    }

                    if (foundSpelledOut.Any() && digits.Any() && line.LastIndexOf(digits.Last()) < foundSpelledOut.Select(x => x.Key).Max())
                    {
                        last = foundSpelledOut[foundSpelledOut.Select(x => x.Key).Max()];
                    }
                    else if (!digits.Any())
                    {
                        last = foundSpelledOut[foundSpelledOut.Select(x => x.Key).Max()];
                    }
                    else
                    {
                        last = (int)Char.GetNumericValue(digits.Last()); 
                    }
                    calibrationValue = Int32.Parse(String.Concat(first, last));

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