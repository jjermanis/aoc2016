namespace AoC2016;

public class Day20 : DayBase, IDay
{
    private struct Blacklist
    {
        public readonly long Lo;
        public readonly long High;

        public Blacklist(string raw)
        {
            var values = raw.Split('-');
            Lo = long.Parse(values[0]);
            High = long.Parse(values[1]);
        }
    }

    private readonly List<Blacklist> _blacklist;

    public Day20(string filename)
    {
        var lines = TextFileLines(filename);
        _blacklist = new List<Blacklist>();
        foreach (var line in lines)
            _blacklist.Add(new Blacklist(line));
        _blacklist.Sort((bl1, bl2) => bl1.Lo.CompareTo(bl2.Lo));
    }

    public Day20() : this("Day20.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(LowestOpenIp)}: {LowestOpenIp()}");
        Console.WriteLine($"{nameof(OpenIpCount)}: {OpenIpCount()}");
    }

    public long LowestOpenIp()
    {
        var result = 0L;
        foreach (var blacklist in _blacklist)
        {
            if (blacklist.Lo > result)
                return result;
            result = Math.Max(result, blacklist.High+1);
        }
        return result;
    }

    public long OpenIpCount()
    {
        var result = 0L;
        var curr = 0L;
        foreach (var blacklist in _blacklist)
        {
            if (blacklist.Lo > curr)
            {
                result += blacklist.Lo - curr;
                curr = blacklist.Lo;
            }
            curr = Math.Max(curr, blacklist.High + 1);
        }
        if (curr < 4294967295)
            result += (4294967295 - curr);
        return result;
    }
}