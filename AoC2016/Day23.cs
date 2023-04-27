namespace AoC2016;

public class Day23 : DayBase, IDay
{
    // TODO this is described to be based on the work from Day 12. Refactor
    // that code to be shared?
    // TODO, it takes 6.5 minutes to run 23-2. Proposed optimization from Day
    // 12 would help.
    // TODO text of AoC trying multiplication to replace inc and dec.
 
    private readonly IList<string> _lines;

    public Day23(string filename)
        => _lines = TextFileStringList(filename);

    public Day23() : this("Day23.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(Value7Eggs)}: {Value7Eggs()}");
        Console.WriteLine($"{nameof(Value12Eggs)}: {Value12Eggs()}");
    }

    public int Value7Eggs()
    {
        var registers = InitRegisters();
        registers['a'] = 7;
        RunProgram(registers);
        return registers['a'];
    }

    public int Value12Eggs()
    {

        var registers = InitRegisters();
        registers['a'] = 12;
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
        var program = new List<string>(_lines);
        for (var i = 0; i < program.Count(); i++)
        {
            var curr = program[i].Split(' ');
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
                        i += ParseValue(curr[2], registers) - 1;
                    break;
                case "tgl":
                    var dist = registers[curr[1][0]];
                    var targetAddr = i + dist;
                    if (targetAddr < program.Count())
                    {
                        var targetInst = program[targetAddr].Split(' ');
                        string newInst;
                        if (targetInst.Length == 2)
                        {
                            newInst = targetInst[0].Equals("inc") ? "dec" : "inc";
                            program[targetAddr] = $"{newInst} {targetInst[1]}";
                        }
                        else
                        {
                            newInst = targetInst[0].Equals("jnz") ? "cpy" : "jnz";
                            program[targetAddr] = $"{newInst} {targetInst[1]} {targetInst[2]}";
                        }
                    }
                    break;
                default:
                    throw new Exception($"Unknown instruction: {inst}");
            }
        }
    }
}