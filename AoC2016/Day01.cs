namespace AoC2016;

public class Day01 : DayBase, IDay
{
    private readonly IEnumerable<string> _steps;

    public Day01(string filename)
    {
         var line = TextFile(filename);
        _steps = line.Split(", ");
    }

    public Day01() : this("Day01.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(TotalWalkDistance)}: {TotalWalkDistance()}");
        Console.WriteLine($"{nameof(RepeatLocationDistance)}: {RepeatLocationDistance()}");
    }

    public int TotalWalkDistance()
    {
        var x = 0;
        var y = 0;
        var dir = 0;
        foreach (var step in _steps)
        {
            dir = Rotate(dir, step[0]);
            var dist = int.Parse(step[1..]);
            (x, y) = Move(x, y, dir, dist);
        }
        return Distance(x, y);
    }

    public int RepeatLocationDistance()
    {
        var x = 0;
        var y = 0;
        var dir = 0;
        var history = new HashSet<int>();

        foreach (var step in _steps)
        {
            dir = Rotate(dir, step[0]);
            var dist = int.Parse(step[1..]);
            for (var i = 0; i < dist; i++)
            {
                (x, y) = Move(x, y, dir, 1);
                var code = x * 1024 + y;
                if (history.Contains(code))
                    return Distance(x, y);
                history.Add(code);
            }
        }
        throw new Exception("No repeat found");
    }

    private static int Rotate(int currDir, char turn)
        => turn switch
        {
            'L' => (currDir + 3) % 4,
            'R' => (currDir + 1) % 4,
            _ => throw new Exception($"Unexpected direction {turn}")
        };

    private static (int x, int y) Move(int x, int y, int dir, int dist)
    {
        switch (dir)
        {
            case 0:
                y += dist; break;
            case 1:
                x += dist; break;
            case 2:
                y -= dist; break;
            case 3:
                x -= dist; break;
        }
        return (x, y);
    }

    private static int Distance(int x, int y)
        => Math.Abs(x) + Math.Abs(y);
}