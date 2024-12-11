using System.Runtime.ExceptionServices;

namespace AdventOfCode.DayThree
{
    public class Number
    {
        public int number;
        public int lineNumber;
        public int firstIndex;
        public int lastIndex;
    }

    public class Symbol
    {
        public char symbol;
        public int lineNumber;
        public int index;
    }

    public class PartOne
    {
        // Method to check whether position is valid or not
        public static bool IsAdjacent(int numberFirstIndex, int numberLastIndex, int numberLineNumber, int symbolIndex, int symbolLineNumber)
        {
            if ((Math.Abs(numberFirstIndex - symbolIndex) <= 1 &&
                Math.Abs(numberLineNumber - symbolLineNumber) <= 1) ||
                (Math.Abs(numberLastIndex - symbolIndex) <= 1 &&
                Math.Abs(numberLineNumber - symbolLineNumber) <= 1)
                )
            {
                return true;
            }
            return false;
        }

        public static int GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName);

            var numbers = new List<Number>();

            var symbols = new List<Symbol>();

            // Loop over lines
            for(int j = 0; j < lines.Count(); j++)
            {
                ReadOnlySpan<char> line = lines.ElementAt(j);

                bool startNewNumber = true;
                string number = string.Empty;

                // Loop over characters in line
                for (int i=0; i<line.Length; i++)
                {
                    char c = line[i];

                    // Get new part number
                    if (Char.IsDigit(c))
                    {
                        number += c;
                        startNewNumber = false;

                        // If last character in line
                        if (i == line.Length - 1)
                        {
                            // Add number including positions
                            numbers.Add(new Number()
                            {
                                number = Int32.Parse(number),
                                lineNumber = j,
                                firstIndex = i - number.Length,
                                lastIndex = i - 1
                            });
                            number = String.Empty;
                        }
                    }
                    else 
                    {   
                        if (!String.IsNullOrWhiteSpace(number))
                        {
                            // Add number including positions
                            numbers.Add(new Number()
                            {
                                number = Int32.Parse(number),
                                lineNumber = j,
                                firstIndex = i - number.Length,
                                lastIndex = i - 1
                            });
                            number = String.Empty;
                            startNewNumber |= true;
                        }

                        // Add symbol including position
                        if (c != '.')
                        {
                            symbols.Add(new Symbol()
                            {
                                symbol = c,
                                lineNumber = j,
                                index = i,
                            });
                        }
                    }
                }
            }

            // Sum of all numbers
            int sumNumbers = 0;

            foreach (var number in numbers)
            {
                foreach (var symbol in symbols)
                {
                    var checkAdjacent = IsAdjacent(number.firstIndex, number.lastIndex, number.lineNumber, symbol.index, symbol.lineNumber);
                    
                    if (checkAdjacent)
                    {
                        // Remove number from sum
                        sumNumbers = sumNumbers + number.number;
                        break;
                    }
                }
            }
            // Answer is the sum of numbers not having adjacent symbols
            return sumNumbers;
        }
    }
}