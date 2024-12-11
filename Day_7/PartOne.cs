using System.ComponentModel.DataAnnotations;
using System.Numerics;
using System.Text.RegularExpressions;

namespace AdventOfCode.DaySeven
{
    public class CardPartOne
    {
        public int Bet;
        public int Type;
        public int Rank;
        public int Value;
    }

    public class PartOne
    {
        public static int CountChars(string source, char toFind)
        {
            return new Regex(Regex.Escape(toFind.ToString())).Matches(source).Count;
        }

        public static int ValueCard(string hand)
        {
            var value = "";

            foreach (var card in hand)
            {
                value += card switch
                {
                    'A' => "14",
                    'K' => "13",
                    'Q' => "12",
                    'J' => "11",
                    'T' => "10",
                    _ => "0" + card.ToString(),
                };
            }

            return Int32.Parse(value);
        }

        public static BigInteger CardValue(string hand, string cardOrder) =>
        new BigInteger(hand.Select(ch => (byte)cardOrder.IndexOf(ch)).Reverse().ToArray());

        public static long GetAnswer(string fileName)
        {
            var lines = File.ReadLines(fileName).ToList();


            /*var cards = new List<char>()
            {
                'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'
            };*/

            var hands = new Dictionary<string, CardPartOne>();
            foreach (var line in lines)
            {
                var seperated = line.Split(' ');
                hands.Add(seperated.First(), new CardPartOne() { Bet = Int32.Parse(seperated.Last()), Type = 0, Rank = 0, Value = ValueCard(seperated.First()) });
            }

            foreach (var hand in hands)
            {
                var cardCounts = new Dictionary<char, int>();

                foreach (var card in hand.Key.AsSpan())
                {
                    if (!cardCounts.ContainsKey(card))
                    {
                        cardCounts.Add(card, CountChars(hand.Key, card));
                    }
                    
                }

                // Get type
                if (cardCounts.Values.Max() == 5)
                {
                    hands[hand.Key].Type = 0;
                }
                else if (cardCounts.Values.Max() == 4)
                {
                    hands[hand.Key].Type = 1;
                }
                else if (cardCounts.Values.Max() == 3 && cardCounts.Values.Any(x => x == 2))
                {
                    hands[hand.Key].Type = 2;
                }
                else if (cardCounts.Values.Max() == 3)
                {
                    hands[hand.Key].Type = 3;
                }
                else if (cardCounts.Values.Where(x => x == 2).Count() == 2)
                {
                    hands[hand.Key].Type = 4;
                }
                else if (cardCounts.Values.Max() == 2)
                {
                    hands[hand.Key].Type = 5;
                }
                else
                {
                    hands[hand.Key].Type = 6;
                }
            }

            var rank = 1;

            var fiveOfAKind = hands.Where(x => x.Value.Type == 0);
            var fourOfAKind = hands.Where(x => x.Value.Type == 1);
            var fullHouse = hands.Where(x => x.Value.Type == 2);
            var threeOfAKind = hands.Where(x => x.Value.Type == 3);
            var twoPairs = hands.Where(x => x.Value.Type == 4);
            var onePair = hands.Where(x => x.Value.Type == 5);
            var highestCard = hands.Where(x => x.Value.Type == 6);

            /*            foreach (var hand in hands) 
                        {
                            hand.Value.Value = ValueCard(hand.Key);
                        }*/

            foreach (var hand in highestCard.OrderBy(x => x.Value.Value))
            {
                hand.Value.Rank = rank;
                rank++;
            }
            foreach (var hand in onePair.OrderBy(x => x.Value.Value))
            {
                hand.Value.Rank = rank;
                rank++;
            }
            foreach (var hand in twoPairs.OrderBy(x => x.Value.Value))
            {
                hand.Value.Rank = rank;
                rank++;
            }
            foreach (var hand in threeOfAKind.OrderBy(x => x.Value.Value))
            {
                hand.Value.Rank = rank;
                rank++;
            }
            foreach (var hand in fullHouse.OrderBy(x => x.Value.Value))
            {
                hand.Value.Rank = rank;
                rank++;
            }
            foreach (var hand in fourOfAKind.OrderBy(x => x.Value.Value))
            {
                hand.Value.Rank = rank;
                rank++;
            }
            foreach (var hand in fiveOfAKind.OrderBy(x => x.Value.Value))
            {
                hand.Value.Rank = rank;
                rank++;
            }

            long answer = 0;

            foreach (var hand in hands)
            {
                answer += hand.Value.Bet * hand.Value.Rank;
            }

            return answer;
        }
    }
}