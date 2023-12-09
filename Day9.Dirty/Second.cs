using System.Text.RegularExpressions;

namespace Day9.Dirty;

public class Second
{
    public long Solve(string input)
    {
        var split = input.Split("\n");

        var regex = new Regex("-?\\d+");

        var total = new List<long>();
        foreach (var row in split)
        {
            total.Add(FindNextNumber(regex.Matches(row).Select(x => int.Parse(x.Value)).ToArray().Reverse().ToArray()));
        }

        return total.Aggregate((curr, item) => curr+item);
    }

    public int FindNextNumber(int[] numbers)
    {
        var evolutions = new List<int[]> {numbers};
        
        while(evolutions[evolutions.Count-1].Any(x => x != 0))
        {
            evolutions.Add(FindDifferences(evolutions.Last()));
        }

        var lastNumbersFound = new List<int> { 0 };
        for (var i = evolutions.Count-2; i >= 0; i--)
        {
            lastNumbersFound.Add(evolutions[i].Last()-lastNumbersFound.Last());
        }
        
        return lastNumbersFound.Last();
    }

    public int[] FindDifferences(int[] numbers)
    {
        var differences = new List<int>();
        for (var i = 0; i < numbers.Length-1; i++)
        {
            differences.Add(0-(numbers.ElementAt(i+1) - numbers.ElementAt(i)));
        }

        return differences.ToArray();
    }
}