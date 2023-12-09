namespace Day8.Dirty;

public class Third
{
    public ulong Solve(string input)
    {
        var split = input.Split("\n\n");
        var instructions = split.ElementAt(0).ToCharArray();

        var nodes = new HashSet<Node>();
        foreach (var row in split.ElementAt(1).Split("\n"))
        {
            var name = row.Substring(0, 3);
            var left = row.Substring(7, 3);
            var right = row.Substring(12, 3);
            
            nodes.Add(new Node { Name = name, Left = left, Right = right});
        }

        var total = 0;
        var currentNodes = nodes.Where(x => x.Name.EndsWith("A")).Select(x => new Node { Name = x.Name, Left = x.Left, Right = x.Right}).ToList();

        var results = new List<ulong>();
        foreach (var cur in currentNodes)
        {
            results.Add(FindFastestRoute(instructions, nodes, cur.Name));
        }

        return Lcmm(results.ToArray());
    }

    public ulong FindFastestRoute(char[] instructions, HashSet<Node> nodes, string startAt)
    {
        ulong total = 0;
        var currentNode = nodes.First(x => x.Name == startAt);
        for (int i = 0; i < instructions.Length; i++)
        {
            total++;
            if (instructions[i] == 'L')
            {
                currentNode = nodes.First(x => x.Name == currentNode.Left);
            }
            
            if (instructions[i] == 'R')
            {
                currentNode = nodes.First(x => x.Name == currentNode.Right);
            }

            if (currentNode.Name.EndsWith('Z'))
            {
                break;
            }

            if (i == instructions.Length - 1)
            {
                i = 0-1;
            }
        }

        return total;
    }

    public class Node
    {
        public required string Name { get; init; }
        public required string Left { get; init; }
        public required string Right { get; init; }
    }
    
    public static ulong Lcmm(ulong[] inputs)
    {
        return Lcm(inputs[0], inputs.Length == 2 ? inputs[1] : Lcmm(inputs.Skip(1).ToArray()));
    }

    public static ulong Lcm(ulong a, ulong b)
    {
        return a * b / Gcd(a, b);
    }

    public static ulong Gcd(ulong a, ulong b)
    {
        while (b != 0)
        {
            var temp = b;
            b = a % b;
            a = temp;
        }
        return a;
    }
}