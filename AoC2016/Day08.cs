namespace AoC2016;

public class Day08 : DayBase, IDay
{
    private enum Operation
    {
        Rect,
        RotRow,
        RotCol,
    }
    private class Instruction
    {
        public readonly Operation Operation;
        public readonly int Arg1;
        public readonly int Arg2;

        public Instruction(string text)
        {
            var tokens = text.Split(' ');
            if (tokens[1] == "row")
            {
                Operation = Operation.RotRow;
                Arg1 = int.Parse(tokens[2].Substring(2));
                Arg2 = int.Parse(tokens[4]);
            }
            else if (tokens[1] == "column")
            {
                Operation = Operation.RotCol;
                Arg1 = int.Parse(tokens[2].Substring(2));
                Arg2 = int.Parse(tokens[4]);
            }
            else if (tokens[0] == "rect")
            {
                Operation=Operation.Rect;
                var size = tokens[1].Split('x');
                Arg1 = int.Parse(size[0]);
                Arg2 = int.Parse(size[1]);
            }
            else
                throw new Exception($"Could not parse: {text}");
        }
    }

    const int SIZE_X = 50;
    const int SIZE_Y = 6;
    private readonly bool[,] _screen = new bool[SIZE_X, SIZE_Y];

    public Day08(string filename)
    { 
        var lines = TextFileLines(filename);
        foreach (var line in lines)
        {
            var inst = new Instruction(line);
            switch (inst.Operation)
            {
                case Operation.Rect:
                    for (var x = 0; x < inst.Arg1; x++)
                        for (var y = 0; y < inst.Arg2; y++)
                            _screen[x, y] = true;
                    break;
                case Operation.RotRow:
                    var tempRow = new bool[SIZE_X];
                    for (var x = 0; x < SIZE_X; x++)
                        tempRow[(x + inst.Arg2) % SIZE_X] = _screen[x, inst.Arg1];
                    for (var x = 0; x < SIZE_X; x++)
                        _screen[x, inst.Arg1] = tempRow[x];
                    break;
                case Operation.RotCol:
                    var tempCol = new bool[SIZE_Y];
                    for (var y = 0; y < SIZE_Y; y++)
                        tempCol[(y + inst.Arg2) % SIZE_Y] = _screen[inst.Arg1, y];
                    for (var y = 0; y < SIZE_Y; y++)
                        _screen[inst.Arg1, y] = tempCol[y];
                    break;
                default:
                    throw new Exception("Unhandled instruction");
            }
        }
    }

    public Day08() : this("Day08.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(LitPixelCount)}: {LitPixelCount()}");
        Console.WriteLine($"{nameof(DisplayPixels)}: {DisplayPixels()}");
    }

    public int LitPixelCount()
    {
        var result = 0;
        for (var x = 0; x < SIZE_X; x++)
            for (var y = 0; y < SIZE_Y; y++)
                if (_screen[x, y])
                    result++;
        return result;
    }

    public int DisplayPixels()
    {
        for (var y = 0; y < SIZE_Y; y++)
        {
            for (var x = 0; x < SIZE_X; x++)
                Console.Write(_screen[x, y] ? 'X' : ' ');
            Console.WriteLine();
        }
        return 0;
    }
}