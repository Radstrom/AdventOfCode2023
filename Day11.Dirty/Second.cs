using System.Text.RegularExpressions;

namespace Day11.Dirty;

public class Second
{
    public Map MapInst { get; set; }
    public long Solve(string input, int timesLarger)
    {
        var split = input.Split("\n");
        
        MapInst = new Map(split, timesLarger);

        long total = 0;
        for (var i = 0; i < MapInst.Galaxies.Max(x => x.Id); i++)
        {
            for (var j = i+1; j <= MapInst.Galaxies.Max(x => x.Id); j++)
            {
                total += MapInst.GetDistance(i, j);
            }
        }

        return total;
    }

    public class Map
    {
        public Map(string[] input, int timesLarger)
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
                        Y = i+(emptyRowCorrections*timesLarger)-emptyRowCorrections
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
                var emptyColumnsNo = emptyColumns.Count(x => x < galaxy.X);
                galaxy.X += emptyColumnsNo*(timesLarger-1);
            }
        }
        public IList<Galaxy> Galaxies { get; init; } = new List<Galaxy>();

        public long GetDistance(int first, int second)
        {
            var g1 = GetGalaxy(first);
            var g2 = GetGalaxy(second);
            
            return Math.Abs(g2.X-g1.X) + Math.Abs(g2.Y-g1.Y);
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