namespace Day2.Dirty;

public static class First
{
    public static int Solve(string input)
    {
        var split = input.Split("\n");

        return split.Sum(x => IsPossible(x, 12, 13, 14));

    }

    public static int IsPossible(string game, int red, int green, int blue)
    {
        var split = game.Split(":");
        var gameId = split[0].Split(" ")[1];

        var rounds = split[1].Split(";");

        foreach (var round in rounds)
        {
            var roundSplit = round.Trim().Split(", ");
            foreach (var cubeType in roundSplit)
            {
                var cubeTypeSplit = cubeType.Split(" ");
                var amount = int.Parse(cubeTypeSplit[0]);
                var color = cubeTypeSplit[1];

                if (color == "green" && amount > green)
                {
                    return 0;
                }
                if (color == "blue" && amount > blue)
                {
                    return 0;
                }
                if (color == "red" && amount > red)
                {
                    return 0;
                }
            }
        }
        
        return int.Parse(gameId);
    }
}