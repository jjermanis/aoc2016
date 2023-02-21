namespace AoC2016;

public class Day06 : DayBase, IDay
{
    private readonly IEnumerable<string> _lines;

    public Day06(string filename)
        => _lines = TextFileLines(filename);

    public Day06() : this("Day06.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(MostCommonEncoding)}: {MostCommonEncoding()}");
        Console.WriteLine($"{nameof(LeastCommonEncoding)}: {LeastCommonEncoding()}");
    }

    public string MostCommonEncoding()
        => FindBestFit((curr, best) => curr > best);

    public string LeastCommonEncoding()
        => FindBestFit((curr, best) => curr < best);

    private string FindBestFit(Func<int, int, bool> BestComparerMethod)
    {
        var wordLen = _lines.First().Length;
        var frequency = LetterFrequency();

        var result = new char[wordLen];
        var bestFreq = new int[wordLen];
        for (var i = 0; i < wordLen; i++)
            foreach (var c in frequency[i].Keys)
            {
                var currFreq = frequency[i][c];
                if (bestFreq[i] == 0 || BestComparerMethod(currFreq, bestFreq[i]))
                {
                    result[i] = c;
                    bestFreq[i] = currFreq;
                }
            }
        return new string(result);
    }

    private Dictionary<char, int>[] LetterFrequency()
    {
        var wordLen = _lines.First().Length;
        var result = new Dictionary<char, int>[wordLen];
        for (var i = 0; i < wordLen; i++)
            result[i] = new Dictionary<char, int>();

        foreach (var line in _lines)
            for (var i = 0; i < wordLen; i++)
            {
                var c = line[i];
                result[i].TryGetValue(c, out int curr);
                result[i][c] = curr + 1;
            }
        return result;
    }

}