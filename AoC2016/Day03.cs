namespace AoC2016;

public class Day03 : DayBase, IDay
{
    private readonly IEnumerable<string> _lines;

    public Day03(string filename)
        => _lines = TextFileLines(filename);

    public Day03() : this("Day03.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(HorizontalTriangleCount)}: {HorizontalTriangleCount()}");
        Console.WriteLine($"{nameof(VeriticalTriangleCount)}: {VeriticalTriangleCount()}");
    }

    public int HorizontalTriangleCount()
    {
        var result = 0;
        foreach(var line in _lines)
        {
            var vals = line.Split(null)
                .Where(v => v.Length > 0)
                .Select(v => int.Parse(v))
                .OrderBy(v => v)
                .ToList();
            if (vals[0] + vals[1] > vals[2])
                result++;
        }
        return result;
    }

    public int VeriticalTriangleCount()
    {
        // Parse input, converting each line to list of int
        var vals = new List<List<int>>();
        foreach (var line in _lines)
        {
            var curr = line.Split(null)
                .Where(v => v.Length > 0)
                .Select(v => int.Parse(v))
                .ToList();
            vals.Add(curr);
        }

        var result = 0;
        // For every three rows, check each of the three columns
        for (int rowNum=0; rowNum<vals.Count; rowNum+=3)
        {
            for (int x = 0; x < 3; x++)
            {
                var curr = new List<int>();
                for (int y = 0; y < 3; y++)
                    curr.Add(vals[rowNum + y][x]);
                curr.Sort();
                if (curr[0] + curr[1] > curr[2])
                    result++;
            }
        }
        return result;
    }
}