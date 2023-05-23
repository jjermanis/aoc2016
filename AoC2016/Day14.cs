using System.Security.Cryptography;

namespace AoC2016;

public class Day14 : DayBase, IDay
{
    // TODO this is slow for Part 2. MD5 is part of it for sure. Profiling should
    // be used to check for parts to optimize.
    // The code can be cleaned between Part 1 and 2 with a refactor. That logic is
    // largely shared.

    string _prefix;

    public Day14(string filename)
        => _prefix = TextFile(filename);

    public Day14() : this("Day14.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(SimpleKeyNum64)}: {SimpleKeyNum64()}");
        Console.WriteLine($"{nameof(ComplexKeyNum64)}: {ComplexKeyNum64()}");
    }

    public int SimpleKeyNum64()
    {
        var md5 = MD5.Create();
        var curr = 0;
        var padKeyCount = 0;
        var keys = new List<int>();
        var triples = new List<(char c, int index)>();
        while (padKeyCount < 64)
        {
            triples = RemoveOldTriples(curr, triples);

            var rawBytes = md5.ComputeHash(Encoding.ASCII.GetBytes($"{_prefix}{curr}"));
            var hash = BitConverter.ToString(rawBytes).Replace("-", "");

            // See if this matches a previous triple as a quint
            var quints = ContainedQuints(hash);
            foreach (var quint in quints)
            {
                foreach (var triple in triples)
                    if (triple.c == quint)
                    {
                        keys.Add(triple.index);
                        padKeyCount++;
                    }    
                triples = RemoveTripleByChar(quint, triples);
            }

            // See if this has a triple
            var newTriple = ContainsTriple(hash);
            if (newTriple.HasValue)
                triples.Add((newTriple.Value, curr));

            curr++;
        }
        keys.Sort();
        return keys[63];
    }

    public int ComplexKeyNum64()
    {
        var md5 = MD5.Create();
        var curr = 0;
        var padKeyCount = 0;
        var keys = new List<int>();
        var triples = new List<(char c, int index)>();
        while (padKeyCount < 64 || triples.Count > 0)
        {
            triples = RemoveOldTriples(curr, triples);

            var rawBytes = md5.ComputeHash(Encoding.ASCII.GetBytes($"{_prefix}{curr}"));
            for (var i = 0; i < 2016; i++)
            {
                var tempHash = ToLowerCaseHexArray(rawBytes);
                rawBytes = md5.ComputeHash(tempHash);
            }

            var hash = BitConverter.ToString(rawBytes).Replace("-", "");

            // See if this matches a previous triple as a quint
            var quints = ContainedQuints(hash);
            foreach (var quint in quints)
            {
                foreach (var triple in triples)
                    if (triple.c == quint)
                    {
                        keys.Add(triple.index);
                        padKeyCount++;
                    }
                triples = RemoveTripleByChar(quint, triples);
            }

            // See if this has a triple
            if (padKeyCount < 64)
            {
                var newTriple = ContainsTriple(hash);
                if (newTriple.HasValue)
                    triples.Add((newTriple.Value, curr));
            }
            curr++;
        }
        keys.Sort();
        return keys[63];
    }

    private static List<(char c, int index)> RemoveOldTriples(int curr, List<(char c, int index)> triples)
        => triples.Where(t => t.index >= curr - 1000).ToList();

    private static List<(char c, int index)> RemoveTripleByChar(char c, List<(char c, int index)> triples)
        => triples.Where(t => t.c != c).ToList();

    private static byte[] ToLowerCaseHexArray(byte[] rawBytes)
    {
        var result = new byte[rawBytes.Length * 2];
        for (var x = 0; x < rawBytes.Length; x++)
        {
            var high = rawBytes[x] / 16;
            var low = rawBytes[x] % 16;
            result[x * 2] = high > 9 ? (byte)(high + 87) : (byte)(high + 48);
            result[x * 2 + 1] = low > 9 ? (byte)(low + 87) : (byte)(low + 48);
        }
        return result;
    }

    private static HashSet<char> ContainedQuints(string hash)
    {
        var result = new HashSet<char>();
        for (int i = 0; i <= hash.Length - 5; i++)
        {
            var isQuint = true;
            for (int j=1; j < 5; j++)
                if (hash[i] != hash[i+j])
                    isQuint = false;
            if (isQuint)
                result.Add(hash[i]);
        }
        return result;
    }

    private static char? ContainsTriple(string hash)
    {
        for (int i = 0; i <= hash.Length - 3; i++)
            if (hash[i] == hash[i+1] && hash[i] == hash[i+2])
                return hash[i];
        return null;
    }
}