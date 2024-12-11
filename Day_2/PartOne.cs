using System.Text.RegularExpressions;

namespace AdventOfCode.DayTwo
{
    public class PartOne
    {
        public static int GetAnswer(string fileName)
        {
            int answer;
            int sumGameIds = 0;

            var lines = File.ReadLines(fileName);

            // Loop over lines
            foreach(string line in lines)
            {
                var game = line.Split(':');

                int gameId = Int32.Parse(game[0].Split(' ')[1]);

                int matches = 0;

                // Remove whitespace and create array of number + colour
                var rounds = game[1].Replace(" ", string.Empty).Split(';');

                foreach (var round in rounds)
                {
                    var cubes = round.Split(',');
                    bool possible = true;

                    // Start with 12 red cubes, 13 green cubes, and 14 blue cubes
                    var gameSet = new Dictionary<string, int>()
                    {
                        {"red", 12 },
                        {"green", 13 },
                        {"blue", 14 },
                    };

                    foreach (string cube in cubes)
                    {
                        var count = Int32.Parse(Regex.Match(cube, @"\d+").Value);

                        var colour = cube.Remove(0, count.ToString().Length);

                        // Exit loop if there not enough cubes of said colour
                        if (!gameSet.ContainsKey(colour) || gameSet[colour] < count)
                        {
                            possible = false;
                        }
                        else
                        {
                            gameSet[colour] = gameSet[colour] - count;
                        }
                    }
                    if (possible)
                    {
                        matches++;
                    }
                }


                if (matches == rounds.Length)
                {
                    //Game could be completed with starting set
                    sumGameIds += gameId;
                }
                else 
                {
                    //Game could not be completed with starting set
                }
            }

            // Answer is total amount of points
            answer = sumGameIds;

            return answer;
        }
    }
}