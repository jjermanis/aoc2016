namespace AoC2016;

public class Day16 : DayBase, IDay
{
    // TODO conversion from byte[] to string is probably not neccessary, and
    // probably takes time for Part 2. Try everything in byte[]
    // TODO tweak the interface to support test cases for Sample input.

    private readonly string _start;

    public Day16(string filename)
        => _start = TextFile(filename);

    public Day16() : this("Day16.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(SmallDiskChecksum)}: {SmallDiskChecksum()}");
        Console.WriteLine($"{nameof(LargeDiskChecksum)}: {LargeDiskChecksum()}");
    }

    public string SmallDiskChecksum()
        => Part1(272);

    public string LargeDiskChecksum()
        => Part1(35651584);

    public string Part1(int targetSize)
    {
        var curr = _start;
        while (curr.Length < targetSize)
        {
            var currLen = curr.Length;
            var next = new char[currLen * 2 + 1];
            next[currLen] = '0';
            for (int i = 0; i < currLen; i++)
            {
                next[i] = curr[i];
                next[currLen * 2 - i] = curr[i] == '0' ? '1' : '0';
            }
            curr = new string(next);
        }
        curr = curr[..targetSize];

        while (curr.Length % 2 == 0)
        {
            var next = new char[curr.Length / 2];
            for (int i = 0; i < curr.Length; i+=2)
                next[i/2] = curr[i] == curr[i+1] ? '1' : '0';
            curr = new string(next);
        }
        return curr;
    }

}