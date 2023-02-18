using System.Security.Cryptography;
using System.Text;

namespace AoC2016;

public class Day05 : DayBase, IDay
{
    // TODO: these take several seconds. To an extent, this is unavoidable due to MD5.
    // Multi-threading, profiling are likely to help.

    private readonly string _prefix;

    public Day05(string filename)
        => _prefix = TextFile(filename);

    public Day05() : this("Day05.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(Part1)}: {Part1()}");
        Console.WriteLine($"{nameof(Part2)}: {Part2()}");
    }

    public string Part1()
    {
        var md5 = MD5.Create();

        var result = 0;
        var curr = 0;
        var matches = 0;
        while (matches < 8)
        {
            var x = md5.ComputeHash(Encoding.ASCII.GetBytes($"{_prefix}{curr++}"));
            if (x[0] == 0 && x[1] == 0 && x[2] < 16)
            {
                matches++;
                result = result * 16 + x[2];
            }
        }
        return result.ToString("x");
    }

    public string Part2()
    {
        var md5 = MD5.Create();

        var curr = 0L;
        var buffer = new int[8] { -1, -1, -1, -1, -1, -1, -1, -1 };
        var matches = 0;
        while (matches < 8)
        {
            var x = md5.ComputeHash(Encoding.ASCII.GetBytes($"{_prefix}{curr++}"));
            if (x[0] == 0 && x[1] == 0 && x[2] < 16)
            {
                var index = x[2];
                if (index < 8 && buffer[index] == -1)
                {
                    matches++;
                    var value = x[3] / 16;
                    buffer[index] = value;
                }
            }
        }
        var result = 0;
        for (int i = 0; i < 8; i++)
            result = result * 16 + buffer[i];
        return result.ToString("x").PadLeft(8, '0');
    }
}