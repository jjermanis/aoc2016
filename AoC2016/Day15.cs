using System.Text.RegularExpressions;

namespace AoC2016;

public class Day15 : DayBase, IDay
{
    // TODO refactor between Part 1 and 2
    // This can be optimized by incrementing by the product of the previouses wheels position
    // counts... though this is already compartively fast.

    private struct Disc
    {
        public readonly int Index;
        public readonly int PositionCount;
        public readonly int StartPosition;

        public Disc(string desc)
        {
            var m = Regex.Match(desc,
                @"Disc #(\d*) has (\d*) positions; at time=0, it is at position (\d*).");
            Index = int.Parse(m.Groups[1].Value);
            PositionCount = int.Parse(m.Groups[2].Value);
            StartPosition = int.Parse(m.Groups[3].Value);
        }

        public Disc(int index, int positionCount, int startPosition)
        {
            Index = index;
            PositionCount = positionCount;
            StartPosition = startPosition;
        }
    }

    private readonly List<Disc> _discs;

    public Day15(string filename)
    {
        _discs = new List<Disc>();
        foreach (var line in TextFileLines(filename))
            _discs.Add(new Disc(line));
    }

    public Day15() : this("Day15.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(FirstClearTime)}: {FirstClearTime()}");
        Console.WriteLine($"{nameof(FirstClearTimeWithNewDisc)}: {FirstClearTimeWithNewDisc()}");
    }

    public int FirstClearTime()
    {
        var curr = 0;
        while (true)
        {
            var fellThrough = true;
            foreach (var disc in _discs)
            {
                var pos = (disc.StartPosition + disc.Index + curr) % disc.PositionCount;
                if (pos != 0)
                {
                    fellThrough = false;
                    break;
                }
            }
            if (fellThrough)
                return curr;
            curr++;
        }
    }

    public int FirstClearTimeWithNewDisc()
    {
        _discs.Add(new Disc(_discs.Count + 1, 11, 0));

        var curr = 0;
        while (true)
        {
            var fellThrough = true;
            foreach (var disc in _discs)
            {
                var pos = (disc.StartPosition + disc.Index + curr) % disc.PositionCount;
                if (pos != 0)
                {
                    fellThrough = false;
                    break;
                }
            }
            if (fellThrough)
                return curr;
            curr++;
        }
    }
}