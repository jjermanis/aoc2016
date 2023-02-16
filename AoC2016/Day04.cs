using System.Text;

namespace AoC2016;

public class Day04 : DayBase, IDay
{
    private readonly IEnumerable<string> _lines;

    public Day04(string filename)
        => _lines = TextFileLines(filename);

    public Day04() : this("Day04.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(RealRoomIdSum)}: {RealRoomIdSum()}");
        Console.WriteLine($"{nameof(NorthPoleRoomId)}: {NorthPoleRoomId()}");
    }

    public int RealRoomIdSum()
    {
        var result = 0;
        foreach (var line in _lines)
        {
            var (name, sectorId, checksum) = ParseLine(line);

            var freq = new Dictionary<char, int>();
            foreach (var c in name)
            {
                if (c != '-')
                {
                    freq.TryGetValue(c, out int curr);
                    freq[c] = curr + 1;
                }
            }
            var sorted = new List<(char, int)>();
            foreach (var key in freq.Keys)
                sorted.Add((key, freq[key]));
            sorted.Sort((n1, n2) =>
            {
                if (n1.Item2 > n2.Item2)
                    return -1;
                if (n1.Item2 < n2.Item2)
                    return 1;
                return n1.Item1 < n2.Item1 ? -1 : 1;
            });
            var match = true;
            for (var i = 0; i < 5; i++)
                if (sorted[i].Item1 != checksum[i])
                {
                    match = false;
                    break;
                }
            if (match)
                result += sectorId;
        }
        return result;
    }

    public int NorthPoleRoomId()
    {
        foreach (var line in _lines)
        {
            var (name, sectorId, _) = ParseLine(line);

            var decrypt = new StringBuilder();
            foreach (var c in name)
                if (c != '-')
                    decrypt.Append((char)(((c - 'a' + sectorId) % 26) + 'a'));
                else
                    decrypt.Append(' ');
            if (decrypt.ToString().Equals("northpole object storage"))
                return sectorId;
        }
        throw new Exception("Match not found");
    }

    private static (string name, int sectorId, string checksum) ParseLine(string line)
    {
        var name = line[0..^11];
        var sectorId = int.Parse(line[^10..^7]);
        var checksum = line[^6..^1];
        return (name, sectorId, checksum);
    }
}