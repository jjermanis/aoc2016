namespace AoC2016;

public class Day25 : DayBase, IDay
{
    // TODO is there a better approach to "infinite" than trying it 100 times?

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

    private bool RunProgram(Dictionary<char, int> registers)
    {
        var count = 0;
        var vm = new AssembunnyVm(_lines);
        foreach (var val in vm.RunProgramWithOutput(registers))
        {
            if (val != count++ % 2)
                return false;
 
            if (count >= 100)
                return true;
        }
        return false;
    }
}