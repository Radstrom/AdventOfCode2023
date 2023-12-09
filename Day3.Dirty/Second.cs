using System.Text.RegularExpressions;

namespace Day3.Dirty;

public static class Second
{
    public static int Solve(string input, Map map)
    {
        var split = input.Split("\n");
        
        map.Parse(split);
        
        var hits = map.FindHits(split).ToList();

        var values = hits.Select(x => GetHitValue(x.X, x.Y, map.Items));
        
        return values.Sum();
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
            for (var rowNo = 0; rowNo < input.Count; rowNo++)
            {
                var regex = new Regex("\\*");

                foreach (Match rowMatch in regex.Matches(input.ElementAt(rowNo)))
                {
                    hits.Add(new Coordinates
                    {
                        X = rowMatch.Index,
                        Y = rowNo
                    });
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
            return hits.Any(hit => IsHit(hit.X, hit.Y));
        }

        public bool IsHit(int x, int y)
        {
            if (x >= LeftLimit && x <= RightLimit && y >= UpperLimit && y <= LowerLimit)
            {
                return true;
            }

            return false;
        }
        public int Value { get; init; }
    }

    public static int GetHitValue(int x, int y, IEnumerable<Item> items)
    {
        var hitItems = items.Where(item => item.IsHit(x, y)).ToList();

        return hitItems.Count == 2 
            ? hitItems.Select(item => item.Value).Aggregate((curr, item) => curr * item) 
            : 0;
    }

    public class Coordinates
    {
        public required int X { get; init; }
        public required int Y { get; init; }
    }
}