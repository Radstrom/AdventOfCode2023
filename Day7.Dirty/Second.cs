using System.Text.RegularExpressions;

namespace Day7.Dirty;

public class Second
{
    private static readonly char[] CardValues = {'A', 'K', 'Q', 'T', '9', '8', '7', '6', '5', '4', '3', '2', 'J'};
    public int Solve(string input)
    {
        var races = new Regex("(\\w+ \\d+)");

        var results = new List<Hand>();
        foreach (Match race in races.Matches(input))
        {
            var raceSplit = race.Value.Split(" ").ToArray();
            results.Add(new Hand{ Cards = raceSplit[0], Bid = int.Parse(raceSplit[1]), HandType = GetHandType(raceSplit[0])});
        }

        var sortedHands = results
            .OrderBy(x => x.HandType)
            .ThenByDescending(x => x.CardValuesList.ElementAt(0))
            .ThenByDescending(x => x.CardValuesList.ElementAt(1))
            .ThenByDescending(x => x.CardValuesList.ElementAt(2))
            .ThenByDescending(x => x.CardValuesList.ElementAt(3))
            .ThenByDescending(x => x.CardValuesList.ElementAt(4))
            .ToList();

        for (int i = 0; i < sortedHands.Count; i++)
        {
            sortedHands[i].Value = sortedHands[i].Bid * (i+1);
        }

        var total = 0;
        foreach (var sortedHand in sortedHands)
        {
            total += sortedHand.Value;
        }

        return total;
    }

    public HandType GetHandType(string cards)
    {
        var cardsArray = cards.ToCharArray().ToArray();
        // Five of a kind
        var cardOtherThenJoker = cardsArray.ToList().Count(x => x != 'J') == 0 ? 'J' : cardsArray.ToList().First(x => x != 'J');
        if ((cardsArray.ElementAt(1) == cardOtherThenJoker || cardsArray.ElementAt(1) == 'J') &&
            (cardsArray.ElementAt(2) == cardOtherThenJoker || cardsArray.ElementAt(2) == 'J') &&
            (cardsArray.ElementAt(3) == cardOtherThenJoker || cardsArray.ElementAt(3) == 'J') &&
            (cardsArray.ElementAt(4) == cardOtherThenJoker || cardsArray.ElementAt(4) == 'J'))
        {
            return HandType.FiveOfAKind;
        }
        
        // Four of a kind
        foreach (var card in cardsArray)
        {
            if (cards.Count(x => x == card || x == 'J') == 4)
            {
                return HandType.FourOfAKind;
            }
        }
        
        // Full House
        foreach (var card in cards.ToCharArray())
        {
            if (cards.Count(x => x == card || x == 'J') == 3)
            {
                if (cards.ToCharArray().Where(x => x != card && x != 'J').Distinct().Count() == 1)
                {
                    return HandType.FullHouse;
                }
            }
        }
        
        // Three
        foreach (var card in cardsArray)
        {
            if (cards.Count(x => x == card || x == 'J') == 3)
            {
                return HandType.ThreeOfAKind;
            }
        }
        
        // Two Pair
        if (FindPair(FindPair(cards)).Length == 1)
        {
            return HandType.TwoPair;
        }
        
        // Pair
        if (FindPair(cards).Length == 3)
        {
            return HandType.OnePair;
        }
        
        return HandType.HighCard;
    }

    public string FindPair(string input)
    {
        foreach (var card in input.ToCharArray())
        {
            if (input.Count(x => x == card || x == 'J') == 2)
            {
                if (input.Count(x => x == card) == 2)
                {
                    return string.Join("", input.Where(y => y != card).ToArray());
                }

                var list = input.ToCharArray().ToList();
                list.Remove('J');
                list.Remove(card);
                return string.Join("", list);
            }
        }

        return input;
    }

    public class Hand
    {
        public string Cards { get; init; }
        public HandType HandType { get; set; }
        public IEnumerable<int> CardValuesList => Cards.Select(x => CardValues.ToList().IndexOf(x));
        public int Bid { get; init; }

        public int Value { get; set; }
    }
    
    public enum HandType {
        HighCard,
        OnePair,
        TwoPair,
        ThreeOfAKind,
        FullHouse,
        FourOfAKind,
        FiveOfAKind
    }
}