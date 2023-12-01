namespace Day1.Dirty;

public static class First
{
    public static int Solve(string input)
    {
        var split = input.Split("\n");

        var total = 0;
        foreach (var row in split)
        {
            var nums = row.Where(x => x >= 48 && x <= 57);
            var all = new string(new List<char>() { nums.First(), nums.Last() }.ToArray());
            var num = int.Parse(all);
            total += num;
        }

        return total;
    }
}