using System.Text.RegularExpressions;

namespace AdventOfCode.DayEight
{
    public class PartOne
    {
        public static long Lcm(long a, long b) => a * b / Gcd(a, b);
        public static long Gcd(long a, long b) => b == 0 ? a : Gcd(b, a % b);

        public static long Steps(string st, string end, string dirs, Dictionary<string, (string, string)> map)
        {
            var i = 0;
            while (!Regex.IsMatch(st, end))
            {
                var dir = dirs[i % dirs.Length];
                if (dir == 'L')
                {
                    st = map[st].Item1;
                }
                else
                {
                    st = map[st].Item2;
                }
                i++;
            }
            return i;
        }

            public static long GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName).ToList();

            var dirs = lines[0];
            var start = "AAA"; 
            var end = "ZZZ";
            var map = new Dictionary<string, (string, string)>();
            foreach (var line in lines.Skip(2))
            {
                var m = Regex.Matches(line, "[A-Z]+").ToArray();
                map[m[0].Value] = (m[1].Value, m[2].Value);
            }

            var res = 1L;
            foreach (var st in map.Keys)
            {
                if (Regex.IsMatch(st, start))
                {
                    res = Lcm(res, Steps(st, end, dirs, map));
                }
            }
            return res;
        }

            /*
            lines
                .Skip(2)
                .ToList()
                .ForEach(line => elements
                    .Add(line.Split(' ').First()
                        , new Tuple<string, string>
                            (line.Split('(').Last().Split(',').First()
                                , Regex.Replace(line.Split(',').Last(), @"(\s+|@|&|'|\(|\)|<|>|#)", "")
                )));

            var currentElement = "";

            while (!reachedEnd)
            {
                foreach (var instruction in instructions)
                {
                    if (currentElement == "ZZZ")
                    {
                        reachedEnd = true;
                        break;
                    }
                    else if (currentElement == "")
                    {
                        if (instruction == 'L')
                        {
                            currentElement = elements.First().Value.Item1;
                        }
                        else
                        {
                            currentElement = elements.First().Value.Item2;
                        }
                    }
                    else
                    {
                        if (instruction == 'L')
                        {
                            currentElement = elements[currentElement].Item1;
                        }
                        else
                        {
                            currentElement = elements[currentElement].Item2;
                        }
                    }
                    steps++;
                }
        }

            // Answer is the number of steps needed to get to ZZZ
            return steps;*/
    }
}