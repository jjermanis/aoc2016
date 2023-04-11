namespace AoC2016;

public class Day18 : DayBase, IDay
{
    // TODO: this is reasonably fast as-is. Can this be sped up through keeping track of
    // already visited rows? Since this would repeat in a pattern, could the duration
    // be determine quickly, regardless of size?
    // Could also be quicker using a bool array, as opposed to HashSet. The HashSet makes
    // the logic more easy for handling the edge. But the array would be quicker.

    private readonly string _firstRow;

    public Day18(string filename)
        => _firstRow = TextFile(filename);

    public Day18() : this("Day18.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(RowsCount40)}: {RowsCount40()}");
        Console.WriteLine($"{nameof(RowsCount400000)}: {RowsCount400000()}");
    }

    public int RowsCount40()
        => SafeTiles(40);

    public int RowsCount400000()
        => SafeTiles(400000);

    public int SafeTiles(int rowCount)
    {
        var width = _firstRow.Length;
        var currTraps = ParseFirstRow();
        var result = width - currTraps.Count;

        for (var r = 1; r < rowCount; r++)
        {
            var nextTraps = new HashSet<int>();
            for (var x = 0; x < width; x++)
                if (currTraps.Contains(x - 1) ^ currTraps.Contains(x + 1))
                    nextTraps.Add(x);
            result += width - nextTraps.Count;
            currTraps = nextTraps;
        }

        return result;
    }

    private HashSet<int> ParseFirstRow()
    {
        var result = new HashSet<int>();
        for (int i = 0; i < _firstRow.Length; i++)
            if (_firstRow[i] == '^')
                result.Add(i);
        return result;
    }
}