using System.Security.Cryptography;

namespace AoC2016;

public class Day17 : DayBase, IDay
{
    // TODO refactor commonality between Part 1 & 2

    private enum Directions
    {
        Up = 0, 
        Down = 1, 
        Left = 2, 
        Right = 3
    }

    private readonly string _basePasscode;

    public Day17(string filename)
        => _basePasscode = TextFile(filename);

    public Day17() : this("Day17.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(ShortestPath)}: {ShortestPath()}");
        Console.WriteLine($"{nameof(LongestPathLength)}: {LongestPathLength()}");
    }

    public string ShortestPath()
    {
        var md5 = MD5.Create();

        var pq = new Queue<(int x, int y, string hash)>();
        pq.Enqueue((0, 0, _basePasscode));
        while (pq.Count > 0)
        {
            var (x, y, hash) = pq.Dequeue();
            var fullHash = md5.ComputeHash(Encoding.ASCII.GetBytes(hash));

            var directions = FindDirections(
                BitConverter.ToString(fullHash).Replace("-", ""));

            foreach (var direction in directions)
            {
                (int currX, int currY, char hashAdd) = direction switch
                {
                    Directions.Up => (x, y - 1, 'U'),
                    Directions.Down => (x, y + 1, 'D'),
                    Directions.Left => (x - 1, y, 'L'),
                    Directions.Right => (x + 1, y, 'R'),
                    _ => throw new Exception($"Unexpected direction: {direction}")
                };
                var newHash = hash + hashAdd;
                if (currX == 3 && currY == 3)
                    return newHash[_basePasscode.Length..];

                if (currX >= 0 && currX <= 3 && currY >= 0 && currY <= 3)
                    pq.Enqueue((currX, currY, newHash));
            }
        }

        throw new Exception("No path found");
    }

    public int LongestPathLength()
    {
        var result = 0;
        var md5 = MD5.Create();

        var pq = new Queue<(int x, int y, string hash)>();
        pq.Enqueue((0, 0, _basePasscode));
        while (pq.Count > 0)
        {
            var (x, y, hash) = pq.Dequeue();
            var fullHash = md5.ComputeHash(Encoding.ASCII.GetBytes(hash));

            var directions = FindDirections(
                BitConverter.ToString(fullHash).Replace("-", ""));

            foreach (var direction in directions)
            {
                (int currX, int currY, char hashAdd) = direction switch
                {
                    Directions.Up => (x, y - 1, 'U'),
                    Directions.Down => (x, y + 1, 'D'),
                    Directions.Left => (x - 1, y, 'L'),
                    Directions.Right => (x + 1, y, 'R'),
                    _ => throw new Exception($"Unexpected direction: {direction}")
                };
                var newHash = hash + hashAdd;
                if (currX == 3 && currY == 3)
                    result = newHash.Length;
                else if (currX >= 0 && currX <= 3 && currY >= 0 && currY <= 3)
                    pq.Enqueue((currX, currY, newHash));
            }
        }

        return result - _basePasscode.Length;
    }

    private static List<Directions> FindDirections(string hash)
    {
        var result = new List<Directions>();

        for (int i=0; i < 4; i++)
            if (hash[i] >= 'B' && hash[i] <= 'F')
                result.Add((Directions)i);

        return result;
    }
}