namespace AoC2016;

public class Day01 : DayBase, IDay
{
    internal enum Direction
    {
        North = 0,
        East = 1,
        South = 2,
        West = 3
    };

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
        var dir = Direction.North;
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
        var dir = Direction.North;
        var history = new HashSet<(int, int)>();

        foreach (var step in _steps)
        {
            dir = Rotate(dir, step[0]);
            var dist = int.Parse(step[1..]);
            for (var i = 0; i < dist; i++)
            {
                // Go step-by-step, checking if this specifc spot has been visited
                (x, y) = Move(x, y, dir, 1);
                if (history.Contains((x, y)))
                    return Distance(x, y);
                history.Add((x, y));
            }
        }
        throw new Exception("No repeat found");
    }

    private static Direction Rotate(Direction currDir, char turn)
        => turn switch
        {
            'L' => (Direction)(((int)currDir + 3) % 4),
            'R' => (Direction)(((int)currDir + 1) % 4),
            _ => throw new Exception($"Unexpected turn: {turn}")
        };

    private static (int x, int y) Move(int x, int y, Direction dir, int dist)
        => dir switch
        {
            Direction.North => (x, y + dist),
            Direction.East => (x + dist, y),
            Direction.South => (x, y - dist),
            Direction.West => (x - dist, y),
            _ => throw new Exception($"Unexpected direction: {dir}")
        };

    private static int Distance(int x, int y)
        => Math.Abs(x) + Math.Abs(y);
}