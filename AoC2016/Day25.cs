namespace AoC2016;

public class Day25 : DayBase, IDay
{
    // TODO yet another VM problem, similar to Day 12 and 23. Shared VM code.
    // This case is a little different since this VM should run infinitely.
    // TODO is there a better approach to "infinite" than trying it 1k times?

    private readonly IList<string> _lines;

    public Day25(string filename)
        => _lines = TextFileStringList(filename);

    public Day25() : this("Day25.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(LowestInitForAlternation)}: {LowestInitForAlternation()}");
        // As usual, there is not a Part2 for Day 25 (and Day 25 only)
    }

    public int LowestInitForAlternation()
    {
        var aStart = 1;
        while (true)
        {
            var registers = InitRegisters();
            registers['a'] = aStart;
            if (RunProgram(registers))
                return aStart;
            aStart++;
        }
    }

    private Dictionary<char, int> InitRegisters()
    {
        var result = new Dictionary<char, int>();
        for (var c = 'a'; c <= 'd'; c++)
            result[c] = 0;
        return result;
    }

    private int ParseValue(string val, Dictionary<char, int> registers)
    {
        if (int.TryParse(val, out var valNum))
            return valNum;
        else
            return registers[val[0]];
    }

    private bool RunProgram(Dictionary<char, int> registers)
    {
        var expected = 0;
        var count = 0;
        for (var i = 0; i < _lines.Count(); i++)
        {
            var curr = _lines[i].Split(' ');
            var inst = curr[0];

            switch (inst)
            {
                case "cpy":
                    registers[curr[2][0]] = ParseValue(curr[1], registers);
                    break;
                case "inc":
                    registers[curr[1][0]]++;
                    break;
                case "dec":
                    registers[curr[1][0]]--;
                    break;
                case "jnz":
                    if (ParseValue(curr[1], registers) != 0)
                        i += int.Parse(curr[2]) - 1;
                    break;
                case "out":
                    var output = ParseValue(curr[1], registers);
                    if (output != expected)
                        return false;
                    count++;
                    if (count >= 100)
                        return true;
                    expected = (expected + 1) % 2;
                    break;
                default:
                    throw new Exception($"Unknown instruction: {inst}");
            }
        }
        return false;
    }
}