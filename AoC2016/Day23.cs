namespace AoC2016;

public class Day23 : DayBase, IDay
{
    // TODO, it takes 1.5 minutes to run 23-2. Already optimized. Look for more?
    // TODO text of AoC trying multiplication to replace inc and dec.

    private readonly IList<string> _lines;

    public Day23(string filename)
    {
        _lines = TextFileStringList(filename);
    }

    public Day23() : this("Day23.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(Value7Eggs)}: {Value7Eggs()}");
        Console.WriteLine($"{nameof(Value12Eggs)}: {Value12Eggs()}");
    }

    public int Value7Eggs()
        => ValueForSafe(7);

    public int Value12Eggs()
        => ValueForSafe(12);

    private int ValueForSafe(int eggCount)
    {
        var vm = new AssembunnyVm(_lines);
        var registers = InitRegisters();
        registers['a'] = eggCount;
        registers = vm.RunProgram(registers);
        return registers['a'];
    }

    private Dictionary<char, int> InitRegisters()
    {
        var result = new Dictionary<char, int>();
        for (var c = 'a'; c <= 'd'; c++)
            result[c] = 0;
        return result;
    }

}