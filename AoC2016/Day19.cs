namespace AoC2016;

public class Day19 : DayBase, IDay
{
    // TODO refactor DayBase to support reading a single int out of the file. Day 13
    // would use it too.
    // 19-2 is super slow, about 15 minutes. Switching to a different data struct
    // (double linked list?) should drop this a lot.

    private readonly int _elfCount;

    public Day19(string filename)
        => _elfCount = TextFileIntList(filename).First();

    public Day19() : this("Day19.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(LastElfStealNeighbor)}: {LastElfStealNeighbor()}");
        Console.WriteLine($"{nameof(LastElfStealAcross)}: {LastElfStealAcross()}");
    }

    public int LastElfStealNeighbor()
    {
        var remaining = new List<int>();
        for (var x=1; x <= _elfCount; x++)
            remaining.Add(x);
        bool currTakes = true;

        while (remaining.Count > 1)
        {
            var next = new List<int>();

            foreach (var x in remaining)
            {
                if (currTakes)
                    next.Add(x);
                currTakes = !currTakes;
            }
            remaining = next;
        }
        return remaining[0];
    }

    public int LastElfStealAcross()
    {
        var remaining = new List<int>();
        for (var x = 1; x <= _elfCount; x++)
            remaining.Add(x);
        var currIndex = 0;

        while (remaining.Count > 1)
        {
            var victimIndex = currIndex + (remaining.Count / 2);
            bool isBehind = false;
            if (victimIndex >= remaining.Count)
            {
                isBehind = true;
                victimIndex = victimIndex % remaining.Count;
            }
            remaining.RemoveAt(victimIndex);
            if (!isBehind)
                currIndex++;
            if (currIndex == remaining.Count)
                currIndex = 0;
        }
        return remaining[0];
    }
}