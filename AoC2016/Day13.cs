namespace AoC2016;

public class Day13 : DayBase, IDay
{
    // TODO Some refactoring between Part 1 and 2. Much of the logic is similar.
    
    private readonly int _favNum;

    private readonly List<(int dx, int dy)> _offsets = new List<(int dx, int dy)>()
    {
        (-1, 0), (1, 0), (0, 1), (0, -1)
    };

    public Day13(string filename)
        => _favNum = TextFileIntList(filename).First();

    public Day13() : this("Day13.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(ShortestPathLength)}: {ShortestPathLength()}");
        Console.WriteLine($"{nameof(DistinctPathsUnderLength50)}: {DistinctPathsUnderLength50()}");
    }

    public int ShortestPathLength()
        => ShortestPathLength(31, 39);

    public int DistinctPathsUnderLength50()
    {
        var pq = new Queue<(int x, int y, int dist)>();
        pq.Enqueue((1, 1, 0));
        var visited = new HashSet<(int x, int y)>();
        visited.Add((1, 1));

        while (true)
        {
            var curr = pq.Dequeue();
            if (curr.dist == 50)
                return visited.Count;

            var incdDist = curr.dist + 1;

            foreach (var nextLoc in GetOptions(curr.x, curr.y))
            {
                if (!visited.Contains((nextLoc.x, nextLoc.y)))
                {
                    visited.Add((nextLoc.x, nextLoc.y));
                    pq.Enqueue((nextLoc.x, nextLoc.y, incdDist));
                }
            }
        }
        throw new Exception("No path found");
    }

    public int ShortestPathLength(int destX, int destY)
    {
        var pq = new Queue<(int x, int y, int dist)>();
        pq.Enqueue((1, 1, 0));
        var visited = new HashSet<(int x, int y)>();
        visited.Add((1, 1));

        while (pq.Count > 0)
        {
            var curr = pq.Dequeue();
            var incdDist = curr.dist + 1;

            foreach (var nextLoc in GetOptions(curr.x, curr.y))
            {
                if (!visited.Contains((nextLoc.x, nextLoc.y)))
                {
                    if (nextLoc.x == destX && nextLoc.y == destY)
                        return incdDist;
                    visited.Add((nextLoc.x, nextLoc.y));
                    pq.Enqueue((nextLoc.x, nextLoc.y, incdDist));
                }
            }
        }
        throw new Exception("No path found");
    }

    private bool IsOpen(int x, int y)
    {
        var val = x*x + 3*x + 2*x*y + y + y*y;
        val += _favNum;

        var result = 0;
        while (val > 0)
        {
            if (val % 2 == 1)
                result++;
            val /= 2;
        }
        return result % 2 == 0;
    }

    private IEnumerable<(int x, int y)> GetOptions(int x, int y)
    {
        foreach (var offset in _offsets)
        {
            var (newX, newY) = (x + offset.dx, y + offset.dy);
            if (newX < 0 || newY < 0)
                continue;
            if (IsOpen(newX, newY))
                yield return (newX, newY);
        }
    }
}