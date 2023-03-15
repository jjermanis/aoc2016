namespace AoC2016;

public class Day12 : DayBase, IDay
{
    // TODO: this runs a little slow (~2 seconds). Profiler is vague. Is it repeated
    // parsing slowing it?

    private readonly IList<string> _lines;

    public Day12(string filename)
        => _lines = TextFileStringList(filename);

    public Day12() : this("Day12.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(RegisterAValue)}: {RegisterAValue()}");
        Console.WriteLine($"{nameof(RegisterAValueWithInitC)}: {RegisterAValueWithInitC()}");
    }

    public int RegisterAValue()
    {
        var registers = InitRegisters();
        RunProgram(registers);
        return registers['a'];
    }

    public int RegisterAValueWithInitC()
    {
        var registers = InitRegisters();
        registers['c'] = 1;
        RunProgram(registers);
        return registers['a'];
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

    private void RunProgram(Dictionary<char, int> registers)
    {
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
                default:
                    throw new Exception($"Unknown instruction: {inst}");
            }
        }
    }
}