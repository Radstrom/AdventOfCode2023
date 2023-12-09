using System.Text.RegularExpressions;

namespace Day6.Dirty;

public class Second
{
    public int Solve(string input)
    {
        var races = new Regex("(\\d+ \\d+)");

        var results = new List<int>();
        foreach (Match race in races.Matches(input))
        {
            var raceSplit = race.Value.Split(" ").Select(ulong.Parse).ToArray();
            var time = raceSplit[0];
            var distance = raceSplit[1];
            
            results.Add(FindWaysToWin(time, distance));
        }
        
        return results.Skip(1).Aggregate(results.First(), (current, result) => current * result);
    }

    public int FindWaysToWin(ulong time, ulong distance)
    {
        var waysToWin = 0;
        for (ulong i = 0; i < distance; i++)
        {
            if (i * (time-i) > distance)
            {
                waysToWin++;
            } else if(waysToWin > 1)
            {
                Console.WriteLine("End " + i);
                break;
            }

            if (waysToWin == 1)
            {
                Console.WriteLine("Start " + i);
            }
        }

        return waysToWin;
    }
}