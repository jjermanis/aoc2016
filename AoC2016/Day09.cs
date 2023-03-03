namespace AoC2016;

public class Day09 : DayBase, IDay
{
    // TODO: refactoring between DecompressedLength() and ImprovedDecompressedLength()?

    private readonly string _line;

    public Day09(string filename)
        => _line = TextFile(filename);

    public Day09() : this("Day09.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(DecompressedLength)}: {DecompressedLength()}");
        Console.WriteLine($"{nameof(ImprovedDecompressedLength)}: {ImprovedDecompressedLength()}");
    }

    public int DecompressedLength()
    {
        var result = 0;

        for (var x=0; x<_line.Length; x++)
        {
            if (_line[x] != '(')
                result++;
            else
            {
                var rightParenLoc = _line.IndexOf(')', x);
                var markerContent = _line[(x + 1)..rightParenLoc];
                var values = markerContent.Split('x');
                var len = int.Parse(values[0]);
                var reps = int.Parse(values[1]);
                x = rightParenLoc + len;
                result += len * reps;
            }
        }
        return result;
    }

    public long ImprovedDecompressedLength()
        => ImprovedDecompressedLength(_line);

    private static long ImprovedDecompressedLength(string line)
    {
        var result = 0L;
        for (var x = 0; x < line.Length; x++)
        {
            if (line[x] != '(')
                result++;
            else
            {
                var rightParenLoc = line.IndexOf(')', x);
                var markerContent = line[(x + 1)..rightParenLoc];
                var values = markerContent.Split('x');
                var len = int.Parse(values[0]);
                var reps = int.Parse(values[1]);
                var substring = line.Substring(rightParenLoc+1, len);
                result += ImprovedDecompressedLength(substring) * reps;
                x = rightParenLoc + len;
            }
        }
        return result;
    }
}