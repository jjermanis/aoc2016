namespace AoC2016;

public class Day02 : DayBase, IDay
{
    private readonly IEnumerable<string> _lines;

    // Note: SQUARE shape is this. No array needed - this is easy to calculate:
    // 123
    // 456
    // 789

    private readonly List<String> DIAMOND = new List<String>()
    {
        "..1..",
        ".234.",
        "56789",
        ".ABC.",
        "..D.."
    };

    public Day02(string filename)
        => _lines = TextFileLines(filename);

    public Day02() : this("Day02.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(SquareCode)}: {SquareCode()}");
        Console.WriteLine($"{nameof(DiamondCode)}: {DiamondCode()}");
    }

    public int SquareCode()
    {
        var result = 0;
        var (x, y) = (1, 1);
        foreach (var line in _lines)
        {
            foreach (var c in line)
            {
                // Try each move in the line. Only do it if it stays inbounds
                (var nextX, var nextY) = AttemptMove(x, y, c);
                if (nextX >= 0 && nextX <= 2 && nextY >= 0 && nextY <= 2)
                    (x, y) = (nextX, nextY);
            }
            var curr = y * 3 + x + 1;
            result = result * 10 + curr;
        }
        return result;
    }

    public string DiamondCode()
    {
        var result = "";
        var (x, y) = (-2, 0);

        foreach (var line in _lines)
        {
            foreach (var c in line)
            {
                // Try each move in the line. Only do it if it stays inbounds
                (var nextX, var nextY) = AttemptMove(x, y, c);
                if (Math.Abs(nextX) + Math.Abs(nextY) <= 2)
                    (x, y) = (nextX, nextY);
            }
            result += DIAMOND[y + 2][x + 2];
        }
        return result;
    }

    private (int x, int y) AttemptMove(int x, int y, char dir)
        => dir switch
        {
            'U' => (x, y - 1),
            'D' => (x, y + 1),
            'L' => (x - 1, y),
            'R' => (x + 1, y),
            _ => throw new Exception($"Unknow direction: {dir}")
        };
}