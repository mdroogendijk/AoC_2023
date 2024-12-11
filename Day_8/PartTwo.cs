using System.Text.RegularExpressions;

namespace AdventOfCode.DayEight
{
    public class PartTwo
    {
        private readonly record struct Node(string Id, string Left, string Right);

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName).ToArray();
            var dirs = lines[0];
            var nodes = lines[2..]
                .Select(ParseNode)
                .ToDictionary(n => n.Id);

            return NavigateMany(dirs, nodes);            
        }

        private static long NavigateSingle(string dirs, IDictionary<string, Node> nodes)
        {
            return Navigate(dirs, nodes, start: "AAA", stop: id => id == "ZZZ");
        }

        private static long NavigateMany(string dirs, IDictionary<string, Node> nodes)
        {
            var stop = (string id) => id.EndsWith('Z');
            var cycles = nodes.Keys
                .Where(id => id.EndsWith('A'))
                .Select(id => Navigate(dirs, nodes, start: id, stop))
                .ToList();

            return Lcm(cycles);
        }

        private static long Navigate(string dirs, IDictionary<string, Node> nodes, string start, Func<string, bool> stop)
        {
            var i = 0L;
            var pos = nodes[start];

            while (!stop.Invoke(pos.Id))
            {
                pos = dirs[(int)(i++ % dirs.Length)] switch
                {
                    'L' => nodes[pos.Left],
                    'R' => nodes[pos.Right],
                    _ => throw new Exception()
                };
            }

            return i;
        }

        private static Node ParseNode(string line)
        {
            var values = Regex.Matches(line, pattern: "[A-Z1-9]{3}").Select(match => match.Value).ToList();
            return new Node(
                Id: values[0],
                Left: values[1],
                Right: values[2]);
        }
        /*public static IList<string> SelectValues(this MatchCollection matches)
        {
            return matches
                .Select(match => match.Value)
                .ToList();
        }*/

        public static long Lcm(long a, long b)
        {
            return a * b / Gcd(a, b);
        }

        public static long Gcd(long a, long b)
        {
            while (a != 0 && b != 0)
            {
                if (a > b)
                {
                    a %= b;
                }
                else
                {
                    b %= a;
                }
            }

            return a | b;
        }

        public static long Lcm(ICollection<long> numbers)
        {
            return numbers
                .Skip(1)
                .Aggregate(
                    seed: numbers.First(),
                    func: Lcm);
        }
    }
}