using System.Text.RegularExpressions;

namespace Day11.Dirty;

public class First
{
    public long Solve(string input)
    {
        var split = input.Split("\n");
        
        var map = new Map(split);

        var total = 0;
        for (var i = 0; i < map.Galaxies.Max(x => x.Id); i++)
        {
            for (int j = i+1; j <= map.Galaxies.Max(x => x.Id); j++)
            {
                total += map.GetDistance(i, j);
            }
        }

        return total;
    }

    public class Map
    {
        public Map(string[] input)
        {
            var regex = new Regex("([#])");
            var id = 0;
            var emptyRowCorrections = 0;
            for (var i = 0; i < input[0].Length; i++)
            {
                foreach (Match match in regex.Matches(input[i]))
                { 
                    Galaxies.Add(new Galaxy
                    {
                        Id = id++,
                        X = match.Index,
                        Y = i+emptyRowCorrections
                    });
                }

                if (regex.Matches(input[i]).Count == 0)
                {
                    emptyRowCorrections++;
                }
            }

            var emptyColumns = Enumerable.Range(0, input[0].Length).Where(x => Galaxies.All(y => y.X != x)).ToList();
            foreach (var galaxy in Galaxies)
            {
                galaxy.X += emptyColumns.Count(x => x < galaxy.X);
            }
        }
        public IList<Galaxy> Galaxies { get; init; } = new List<Galaxy>();

        public int GetDistance(int first, int second)
        {
            var g1 = GetGalaxy(first);
            var g2 = GetGalaxy(second);
            
            return Math.Abs(g2.X-g1.X) + (g2.Y-g1.Y);
        }

        public Galaxy GetGalaxy(int id)
        {
            return Galaxies.Single(x => x.Id == id);
        }
    }
    
    public class Galaxy
    {
        public int X { get; set; }
        public int Y { get; init; }
        public int Id { get; init; }
    }
}