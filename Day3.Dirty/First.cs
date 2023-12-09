using System.Text.RegularExpressions;

namespace Day3.Dirty;

public static class First
{
    public static int Solve(string input, Map map)
    {
        var badSymbols = input.Where(x => x != '.' && !(x >= 48 && x <= 57) && x != '\n').Distinct().ToList();
        
        map.Parse(FixInput(input, badSymbols, '.'));
        
        var hits = map.FindHits(FixInput(input, badSymbols, '*')).ToList();

        var hitItems = map.Items.Where(x => x.IsHit(hits)).ToList();
        
        return hitItems.Sum(x => x.Value);
    }

    public static IList<string> FixInput(string input, IEnumerable<char> badSymbols, char replaceWith)
    {
        var normalized = badSymbols.Aggregate(input, (current, badSymbol) => current.Replace(badSymbol, replaceWith));
        var splitToRows = normalized.Split("\n");
        return splitToRows.Select(x => x).ToList();
    }
}

public class Map
{
    public IList<Item> Items { get; init; } = new List<Item>();
    public void Parse(IList<string> input)
    {
        for (var rowNo = 0; rowNo < input.Count; rowNo++)
        {
            var regex = new Regex("\\d+");

            foreach (Match rowMatch in regex.Matches(input.ElementAt(rowNo)))
            {
                Items.Add(new Item
                {
                    UpperLeft = new Coordinates { X = rowMatch.Index-1, Y = rowNo-1}, 
                    UpperRight = new Coordinates { X = rowMatch.Index+rowMatch.Length, Y = rowNo-1}, 
                    LowerLeft = new Coordinates { X = rowMatch.Index-1, Y = rowNo+1}, 
                    LowerRight = new Coordinates { X = rowMatch.Index+rowMatch.Length, Y = rowNo+1}, 
                    Value = int.Parse(rowMatch.Value)
                });
            }
        }
    }

    public IEnumerable<Coordinates> FindHits(IList<string> input)
    {
        var hits = new List<Coordinates>();
        for (var i = 0; i < input.Count; i++)
        {
            for (var j = 0; j < input[i].Length; j++)
            {
                if (input[i][j] == '*')
                {
                    hits.Add(new Coordinates { X = j, Y = i});
                }
            }
        }

        return hits;
    }
}

public class Item
{
    public Coordinates UpperLeft { get; init; }
    public Coordinates UpperRight { get; init; }
    public Coordinates LowerLeft { get; init; }
    public Coordinates LowerRight { get; init; }
    public int UpperLimit => UpperLeft.Y;
    public int RightLimit => UpperRight.X;
    public int LowerLimit => LowerLeft.Y;
    public int LeftLimit => LowerLeft.X;
    public bool IsHit(IEnumerable<Coordinates> hits)
    {
        foreach (var hit in hits)
        {
            if (hit.X >= LeftLimit && hit.X <= RightLimit && hit.Y >= UpperLimit && hit.Y <= LowerLimit)
            {
                return true;
            }
        }
        
        return false;
    }
    public int Value { get; init; }
}

public class Coordinates
{
    public required int X { get; init; }
    public required int Y { get; init; }
}