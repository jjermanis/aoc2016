namespace AoC2016;

public class Day07 : DayBase, IDay
{
    private readonly char[] BRACKETS = new char[] { '[', ']' };

    private readonly IEnumerable<string> _lines;

    public Day07(string filename)
        => _lines = TextFileLines(filename);

    public Day07() : this("Day07.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(TlsCount)}: {TlsCount()}");
        Console.WriteLine($"{nameof(SslCount)}: {SslCount()}");
    }

    public int TlsCount()
        => MatchingCount(SupportsTls);

    public int SslCount()
        => MatchingCount(SupportsSsl);

    private int MatchingCount(Func<string[], bool> ConfirmSupportFunc)
    {
        var result = 0;
        foreach (var line in _lines)
        {
            var sequences = line.Split(BRACKETS);
            if (ConfirmSupportFunc(sequences))
                result++;
        }
        return result;
    }

    private bool SupportsTls(string[] sequences)
    {
        bool isAbbaFound = false;
        bool insideHypernet = false;
        foreach (var sequence in sequences)
        {
            var currContains = ContainsAbba(sequence);
            if (currContains)
                if (insideHypernet)
                    return false;
                else 
                    isAbbaFound = true;
            insideHypernet = !insideHypernet;
        }
        return isAbbaFound;
    }

    private bool ContainsAbba(string sequence)
    {
        for (var x = 0; x < sequence.Length-3; x++)
            if (sequence[x] == sequence[x + 3] &&
                sequence[x + 1] == sequence[x + 2] &&
                sequence[x] != sequence[x + 1])
                return true;
        return false;
    }

    private bool SupportsSsl(string[] sequences)
    {
        var abaSet = new HashSet<string>();
        for (var i=0; i < sequences.Length; i+=2)
            AddAbas(sequences[i], abaSet);

        var babSet = new HashSet<string>();
        foreach (var aba in abaSet)
            babSet.Add($"{aba[1]}{aba[0]}{aba[1]}");

        for (var i = 1; i < sequences.Length; i += 2)
            if (ContainsBab(sequences[i], babSet))
                return true;
        return false;
    }

    private void AddAbas(string sequence, HashSet<string> abaSet)
    {
        for (var x = 0; x < sequence.Length - 2; x++)
            if (sequence[x] == sequence[x + 2] &&
                sequence[x] != sequence[x + 1])
                abaSet.Add(sequence.Substring(x, 3));
    }

    private bool ContainsBab(string sequence, HashSet<string> babSet)
    {
        for (var x = 0; x < sequence.Length - 2; x++)
            if (babSet.Contains(sequence.Substring(x, 3)))
                return true;
        return false;
    }
}