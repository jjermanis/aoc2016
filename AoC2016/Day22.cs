namespace AoC2016;

public class Day22 : DayBase, IDay
{
    // TODO substantial code cleanup and simplification can be done
    // TODO is there a viable way to calculate the distance from the targer
    // (once reached) to the goal?

    private struct Node
    {
        public int X;
        public int Y;
        public int Used;
        public int Available;
        public bool IsGoal { get; set; }

        public Node(string text)
        {
            var values = text.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            var system = Regex.Match(values[0], @"/dev/grid/node-x(\d*)-y(\d*)");
            X = int.Parse(system.Groups[1].Value);
            Y = int.Parse(system.Groups[2].Value);
            Used = int.Parse(values[2][..^1]);
            Available = int.Parse(values[3][..^1]);
            IsGoal = false;
        }

        public Node MoveTo(Node b)
        {
            var size = this.Used;

            b.Used += size;
            b.Available -= size;
            if (b.Available < 0)
                throw new Exception("Overfilled");

            this.Used = 0;
            this.Available += size;

            if (this.IsGoal)
                b.IsGoal = true;
            return b;
        }

        public override string ToString()
            =>$"x{X}-y{Y}";
    }
    private List<Node> _nodes;
    private readonly int _maxX;
    private readonly int _maxY;

    private readonly List<(int dx, int dy)> _NEXT_DELTAS = new List<(int, int)>
        { (-1, 0), (1, 0), (0, -1), (0, 1) };

    public Day22(string filename)
    {
        _nodes = new List<Node>();
        var lines = TextFileLines(filename);
        foreach (var line in lines)
        {
            if (line.StartsWith("/dev/"))
            {
                var curr = new Node(line);
                _nodes.Add(curr);
                _maxX = Math.Max(_maxX, curr.X);
                _maxY = Math.Max(_maxY, curr.Y);
            }
        }
    }

    public Day22() : this("Day22.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(ViablePairCount)}: {ViablePairCount()}");
        Console.WriteLine($"{nameof(ShortestPathLength)}: {ShortestPathLength()}");
    }

    public int ViablePairCount()
    {
        var result = 0;
        for (var a = 0; a < _nodes.Count; a++)
            for (var b = 0; b < _nodes.Count; b++)
            {
                if (a != b)
                {
                    var nodeA = _nodes[a];
                    var nodeB = _nodes[b];

                    if (nodeA.Used > 0 &&
                        nodeB.Available >= nodeA.Used)
                        result++;
                }
            }
        return result;
    }

    public int ShortestPathLength()
    {
        for (var i = 0; i < _nodes.Count; i++)
        {
            var curr = _nodes[i];
            if (curr.X == _maxX && curr.Y == 0)
            {
                curr.IsGoal = true;
                _nodes[i] = curr;
            }
        }

        var foundGoal = false;
        var queue = new Queue
            <(Dictionary<(int, int), Node> Nodes, int Time)>();
        var visited = new HashSet<(int, int)>();
        queue.EnsureCapacity(100000);
        queue.Enqueue((CreateDictionary(_nodes), 0));
        while (queue.Count > 0)
        {
            var (nodes, time) = queue.Dequeue();
            var newTime = time + 1;
            var moves = LegalMoves(nodes);
            foreach(var move in moves)
            {
                var newSet = new Dictionary<(int, int), Node>(nodes);
                var a = newSet[(move.A.X, move.A.Y)];
                var b = newSet[(move.B.X, move.B.Y)];

                if (visited.Contains((a.X, a.Y)))
                    continue;
                visited.Add((a.X, a.Y));

                b = a.MoveTo(b);

                if (b.X == 0 && b.Y == 0 && b.IsGoal)
                    return newTime;

                newSet[(move.A.X, move.A.Y)] = a;
                newSet[(move.B.X, move.B.Y)] = b;
                if (b.IsGoal && !foundGoal)
                {
                    foundGoal = true;
                    visited = new HashSet<(int, int)>();
                    queue = new Queue
                        <(Dictionary<(int, int), Node> Nodes, int Time)>();
                    return newTime + b.X * 5;
                }
                queue.Enqueue((newSet, newTime));
            }
        }
        for (var y = 0; y <= _maxY; y++)
        {
            for (var x = 0; x <= _maxX; x++)
                Console.Write(visited.Contains((x, y)) ? 'X' : '.');
            Console.WriteLine();
        }
        throw new Exception("No path found");
    }

    private static Dictionary<(int, int), Node> CreateDictionary(List<Node> nodes)
    {
        var result = new Dictionary<(int, int), Node>();
        foreach(var node in nodes)
            result[(node.X, node.Y)] = node;
        return result;
    }

    private List<(Node A, Node B)> LegalMoves(Dictionary<(int, int), Node> nodes)
    {
        var result = new List<(Node, Node)>();
        for (var x = 0; x <= _maxX; x++)
            for (var y = 0; y <= _maxY; y++)
                foreach (var delta in _NEXT_DELTAS)
                {
                    if (!nodes.ContainsKey((x + delta.dx, y + delta.dy)))
                        continue;
                    var a = nodes[(x, y)];
                    var b = nodes[(x + delta.dx, y + delta.dy)];

                    if (a.Used > 0 && a.Used < b.Available)
                        result.Add((a, b));
                }
        return result;
    }
}