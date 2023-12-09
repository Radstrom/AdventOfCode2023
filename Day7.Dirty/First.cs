using System.Text.RegularExpressions;

namespace Day7.Dirty;

public class First
{
    public static char[] CardValues = {'A', 'K', 'Q', 'J', 'T', '9', '8', '7', '6', '5', '4', '3', '2'};
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
            .ThenByDescending(x => x.CardValues.ElementAt(0))
            .ThenByDescending(x => x.CardValues.ElementAt(1))
            .ThenByDescending(x => x.CardValues.ElementAt(2))
            .ThenByDescending(x => x.CardValues.ElementAt(3))
            .ThenByDescending(x => x.CardValues.ElementAt(4))
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
        if (cardsArray.Distinct().Count() == 1)
        {
            return HandType.FiveOfAKind;
        }
        
        // Four of a kind
        foreach (var card in cardsArray)
        {
            if (cards.Count(x => x == card) == 4)
            {
                return HandType.FourOfAKind;
            }
        }
        
        // Full House
        var threePartFound = false;
        var twoPartFound = false;
        foreach (var card in cardsArray)
        {
            if (cards.Count(x => x == card) == 3)
            {
                threePartFound = true;
            }
            
            if (cards.Count(x => x == card) == 2)
            {
                twoPartFound = true;
            }
        }

        if (threePartFound && twoPartFound)
        {
            return HandType.FullHouse;
        }
        
        // Three
        foreach (var card in cardsArray)
        {
            if (cards.Count(x => x == card) == 3)
            {
                return HandType.ThreeOfAKind;
            }
        }
        
        // Two Pair
        var sorted = cardsArray.OrderBy(x => x);
        var pairsFound = 0;
        for (int i = 0; i < sorted.Count()-1; i++)
        {
            if (sorted.ElementAt(i) == sorted.ElementAt(i + 1))
            {
                pairsFound++;
            }
        }

        if (pairsFound == 2)
        {
            return HandType.TwoPair;
        }
        
        // Pair
        foreach (var card in cardsArray)
        {
            if (cards.Count(x => x == card) == 2)
            {
                return HandType.OnePair;
            }
        }
        
        return HandType.HighCard;
    }

    public class Hand
    {
        public string Cards { get; init; }
        public HandType HandType { get; set; }
        public IEnumerable<int> CardValues => Cards.Select(x => First.CardValues.ToList().IndexOf(x));
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