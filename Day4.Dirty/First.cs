namespace Day4.Dirty;

public class First
{
    public int Solve(string input)
    {
        var games = input.Split("\n");

        var total = 0;
        foreach (var game in games)
        {
            var gameSplits = game.Split(":");

            var numberSplit = gameSplits[1].Split("|");
            var winningNumbers = numberSplit[0].Split(" ").Select(x => x.Trim()).Where(x => x != String.Empty).Select(x => int.Parse(x));
            var ownNumbers = numberSplit[1].Split(" ").Select(x => x.Trim()).Where(x => x != String.Empty).Select(x => int.Parse(x));

            var winning = 0;
            var nextWinValue = 1;
            foreach (var ownNumber in ownNumbers)
            {
                if (winningNumbers.Contains(ownNumber))
                {
                    Console.WriteLine($"{gameSplits[0]} {ownNumber} wins! {nextWinValue} points!");
                    winning = nextWinValue;
                    nextWinValue *= 2;
                }
            }

            total += winning;
        }
        return total;
    }
}