namespace Day8.Dirty;

public class First
{
    public int Solve(string input)
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
        var currentNode = nodes.First(x => x.Name == "AAA");
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

            if (currentNode.Name == "ZZZ")
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
        public string Name { get; init; }
        public string Left { get; init; }
        public string Right { get; init; }
    }
}