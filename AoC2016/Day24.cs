namespace AoC2016;

public class Day24 : DayBase, IDay
{
    // TODO review code. A different approach was originally used. Maybe some
    // code and members are no longer relevant.
    // TODO Refactor between Part 1 and 2. They are near identical.

    private readonly HashSet<(int X, int Y)> _path;
    private readonly Dictionary<int, (int X, int Y)> _targets;

    private readonly List<(int dx, int dy)> _NEXT_DELTAS = new()
        { (-1, 0), (1, 0), (0, -1), (0, 1) };

    public Day24(string filename)
    {
        _path = new HashSet<(int X, int Y)>();
        _targets = new Dictionary<int, (int X, int Y)>();
        var lines = TextFileStringList(filename);
        for (var y = 0; y < lines.Count; y++)
            for (var x = 0; x < lines[y].Length; x++)
            {
                var curr = lines[y][x];
                if (curr != '#')
                {
                    _path.Add((x, y));
                    if (curr != '.')
                        _targets[curr - '0'] = (x, y);
                }
            }
    }

    public Day24() : this("Day24.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(ShortestVisitAllDucts)}: {ShortestVisitAllDucts()}");
        Console.WriteLine($"{nameof(ShortestVisitRoundtrip)}: {ShortestVisitRoundtrip()}");
    }

    public int ShortestVisitAllDucts()
    {
        var distances = new Dictionary<(int, int), int>();
        for (var a = 0; a < _targets.Keys.Count; a++)
            for (var b = 0; b < _targets.Keys.Count; b++)
                if (a != b)
                    distances[(a, b)] = TargetDistance(a, b);

        var result = int.MaxValue;
        var targets = "";
        foreach (var key in _targets.Keys)
            if (key != 0)
                targets += key.ToString();
        var perms = AllPermutations(targets);
        foreach (var perm in perms)
        {
            var currPath = "0" + perm;
            var currDistance = 0;
            for (var x=0; x < perm.Length; x++)
            {
                currDistance += distances[(currPath[x]-'0', currPath[x+1]-'0')];
            }
            result = Math.Min(result, currDistance);
        }
        return result;
    }

    public int ShortestVisitRoundtrip()
    {
        var distances = new Dictionary<(int, int), int>();
        for (var a = 0; a < _targets.Keys.Count; a++)
            for (var b = 0; b < _targets.Keys.Count; b++)
                if (a != b)
                    distances[(a, b)] = TargetDistance(a, b);

        var result = int.MaxValue;
        var targets = "";
        foreach (var key in _targets.Keys)
            if (key != 0)
                targets += key.ToString();
        var perms = AllPermutations(targets);
        foreach (var perm in perms)
        {
            var currPath = "0" + perm + "0";
            var currDistance = 0;
            for (var x = 0; x < perm.Length + 1; x++)
            {
                currDistance += distances[(currPath[x] - '0', currPath[x + 1] - '0')];
            }
            result = Math.Min(result, currDistance);
        }
        return result;
    }

    private int TargetDistance(int source, int dest)
    {
        var startLoc = _targets[source];
        var (endX, endY) = _targets[dest];

        var queue = new Queue<((int X, int Y), int Distance)>();
        var visited = new HashSet<(int x, int y)>() { startLoc };
        queue.Enqueue((startLoc, 0));
        while (queue.Count > 0)
        {
            var (currLoc, currMoves) = queue.Dequeue();
            currMoves++;

            foreach (var (dx, dy) in _NEXT_DELTAS)
            {
                var newCoord = (currLoc.X + dx, currLoc.Y + dy);
                if (_path.Contains(newCoord) && !visited.Contains(newCoord))
                {
                    visited.Add(newCoord);
                    if (newCoord.Item1 == endX && newCoord.Item2 == endY)
                        return currMoves;
                    queue.Enqueue((newCoord, currMoves));
                }
            }
        }
        throw new Exception("No path found");
    }

    static IList<string> AllPermutations(string val)
    {
        var result = new List<string>();
        Permute(val, 0, val.Length - 1, result);
        return result;
    }

    private static void Permute(String str,
        int l, int r, IList<string> result)
    {
        if (l == r)
            result.Add(str);
        else
        {
            for (int i = l; i <= r; i++)
            {
                var option = Swap(str, l, i);
                Permute(option, l + 1, r, result);
            }
        }
    }
    public static String Swap(String val,
                        int i1, int i2)
    {
        var list = new List<char>(val);
        (list[i1], list[i2]) = (list[i2], list[i1]);
        return new string(list.ToArray());
    }
}