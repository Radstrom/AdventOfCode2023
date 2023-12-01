namespace Day1.Dirty;

public static class Second
{
    public static int Solve(string input)
    {
        var split = input.Split("\n").Select(x => new string(x));

        var total = 0;
        
        foreach (var row in split)
        {
            var first = "";
            for (int i = 0; i < row.Length; i++)
            {
                first = Find(row[i..]);
                if (first != "")
                {
                    break;
                }
            }
            
            var last = "";
            for (int i = row.Length; i >= 0; i--)
            {
                last = Find(row[i..]);
                if (last != "")
                {
                    break;
                }
            }
            
            var all = new string(new List<char>() { first.Single(), last.Single() }.ToArray());
            var num = int.Parse(all);
            total += num;
        }

        return total;
    }

    public static string Find(string x)
    {
        if (x.StartsWith("zero") || x.StartsWith("0"))
        {
            return "0";
        }
        if (x.StartsWith("one") || x.StartsWith("1"))
        {
            return "1";
        }
        if (x.StartsWith("two") || x.StartsWith("2"))
        {
            return "2";
        }
        if (x.StartsWith("three") || x.StartsWith("3"))
        {
            return "3";
        }
        if (x.StartsWith("four") || x.StartsWith("4"))
        {
            return "4";
        }
        if (x.StartsWith("five") || x.StartsWith("5"))
        {
            return "5";
        }
        if (x.StartsWith("six") || x.StartsWith("6"))
        {
            return "6";
        }
        if (x.StartsWith("seven") || x.StartsWith("7"))
        {
            return "7";
        }
        if (x.StartsWith("eight") || x.StartsWith("8"))
        {
            return "8";
        }
        if (x.StartsWith("nine") || x.StartsWith("9"))
        {
            return "9";
        }

        return "";
    }
}