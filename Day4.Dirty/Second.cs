namespace Day4.Dirty;

public class Second
{
    public static int Total = 0;
    public static string[] Games;
    public int Solve(string input)
    {
        var games = input.Split("\n");

        Games = games;
        CheckGames(0, Games.Length);

        return Total;
    }

    public void CheckGames(int Skip, int Take)
    {
        var gameNo = 0;
        foreach (var game in Games.Skip(Skip).Take(Take))
        {
            Total++;
            var gameSplits = game.Split(":");

            var numberSplit = gameSplits[1].Split("|");
            var winningNumbers = numberSplit[0].Split(" ").Select(x => x.Trim()).Where(x => x != String.Empty).Select(x => int.Parse(x));
            var ownNumbers = numberSplit[1].Split(" ").Select(x => x.Trim()).Where(x => x != String.Empty).Select(x => int.Parse(x));
            
            var winning = 0;
            foreach (var ownNumber in ownNumbers)
            {
                if (winningNumbers.Contains(ownNumber))
                {
                    winning++;
                }
            }

            gameNo++;
            if (winning > 0)
            {
                CheckGames(gameNo+Skip, winning);
            }
        }
    }
}