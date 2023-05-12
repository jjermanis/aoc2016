namespace AoC2016;

public class Day12 : DayBase, IDay
{
    // TODO: OK performance (about 600ms). Should AssembunnyVm use (internally) array instead of Dictionary?

    private readonly AssembunnyVm _vm;

    public Day12(string filename)
    {
        _vm = new AssembunnyVm(TextFileStringList(filename));
    }

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
        _vm.RunProgram(registers);
        return registers['a'];
    }

    public int RegisterAValueWithInitC()
    {
        var registers = InitRegisters();
        registers['c'] = 1;
        _vm.RunProgram(registers);
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