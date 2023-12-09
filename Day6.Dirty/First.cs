using System.Text.RegularExpressions;

namespace Day6.Dirty;

public class First
{
    public int Solve(string input)
    {
        var races = new Regex("(\\d+ \\d+)");

        var results = new List<int>();
        foreach (Match race in races.Matches(input))
        {
            var raceSplit = race.Value.Split(" ").Select(int.Parse).ToArray();
            var time = raceSplit[0];
            var distance = raceSplit[1];
            
            results.Add(FindWaysToWin(time, distance));
        }

        return results.Skip(1).Aggregate(results.First(), (current, result) => current * result);
    }

    public int FindWaysToWin(int time, int distance)
    {
        var waysToWin = 0;
        for (int i = 0; i < distance; i++)
        {
            if (i * (time-i) > distance)
            {
                waysToWin++;
            }
        }

        return waysToWin;
    }
}