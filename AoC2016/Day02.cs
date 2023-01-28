namespace AoC2016;

public class Day02 : DayBase, IDay
{
    private readonly IEnumerable<string> _lines;

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
        var x = 1;
        var y = 1;
        foreach (var line in _lines)
        {
            foreach (var c in line)
            {
                switch (c)
                {
                    case 'U':
                        if (y > 0)
                            y--;
                        break;
                    case 'D':
                        if (y < 2)
                            y++;
                        break;
                    case 'L':
                        if (x > 0)
                            x--;
                        break;
                    case 'R':
                        if (x < 2)
                            x++;
                        break;
                    default:
                        throw new Exception($"Unknow direction: {c}");
                }
            }
            var curr = y * 3 + x + 1;
            result = result * 10 + curr;
        }
        return result;
    }

    public string DiamondCode()
    {
        var result = "";
        var x = -2;
        var y = 0;
        foreach (var line in _lines)
        {
            foreach (var c in line)
            {
                (int nextX, int nextY) = c switch
                {
                    'U' => (x, y-1),
                    'D' => (x, y+1),
                    'L' => (x-1, y),
                    'R' => (x+1, y),
                    _ => throw new Exception($"Unknow direction: {c}")
                };
                if (Math.Abs(nextX) + Math.Abs(nextY) <= 2)
                    (x, y) = (nextX, nextY);
            }
            result += DIAMOND[y + 2][x + 2];
        }
        return result;
    }
}