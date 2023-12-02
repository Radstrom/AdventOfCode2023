namespace Day2.Dirty;

public static class Second
{
    public static int Solve(string input)
    {
        var split = input.Split("\n");

        return split.Sum(x => IsPossible(x));

    }

    public static int IsPossible(string game)
    {
        var split = game.Split(":");

        var rounds = split[1].Split(";");

        var lowestRed = 0;
        var lowestBlue = 0;
        var lowestGreen = 0;

        foreach (var round in rounds)
        {
            var roundSplit = round.Trim().Split(", ");
            foreach (var cubeType in roundSplit)
            {
                var cubeTypeSplit = cubeType.Split(" ");
                var amount = int.Parse(cubeTypeSplit[0]);
                var color = cubeTypeSplit[1];

                if (color == "green" && amount > lowestGreen)
                {
                    lowestGreen = amount;
                }
                if (color == "blue" && amount > lowestBlue)
                {
                    lowestBlue = amount;
                }
                if (color == "red" && amount > lowestRed)
                {
                    lowestRed = amount;
                }
            }
        }

        return lowestBlue * lowestGreen * lowestRed;
    }
}