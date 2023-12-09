namespace Day8.Dirty;

public class Second
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
        var currentNodes = nodes.Where(x => x.Name.EndsWith("A")).Select(x => new Node { Name = x.Name, Left = x.Left, Right = x.Right}).ToList();
        for (int i = 0; i < instructions.Length; i++)
        {
            total++;
            
            currentNodes = currentNodes.Select(currentNode =>
            {
                var foundNode = nodes.First(x => x.Name == (instructions[i] == 'L' ? currentNode.Left : currentNode.Right));
                return new Node
                {
                    Name = foundNode.Name,
                    Left = foundNode.Left,
                    Right = foundNode.Right
                };
            }).ToList();

            if (currentNodes.TrueForAll(x => x.Name.EndsWith('Z')))
            {
                break;
            }

            if (total % 100 == 0)
            {
                Console.WriteLine(" ");
            }

            if (i == instructions.Length - 1)
            {
                i = -1;
            }
        }

        return total;
    }

    public class Node
    {
        public string Name { get; set; }
        public string Left { get; set; }
        public string Right { get; set; }
    }
}