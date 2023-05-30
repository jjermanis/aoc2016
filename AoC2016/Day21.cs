namespace AoC2016;

public class Day21 : DayBase, IDay
{
    // TODO clean up the permutations code

    internal interface IInstruction
    {
        string Execute(string input);
    }

    internal class SwapPositionInstruction : IInstruction
    {
        public readonly int Operand1;
        public readonly int Operand2;

        public SwapPositionInstruction(int op1, int op2)
        {
            Operand1 = op1;
            Operand2 = op2;
        }

        public string Execute(string input)
            => Swap(input, Operand1, Operand2);
    }

    internal class SwapLetterInstruction : IInstruction
    {
        public readonly char Operand1;
        public readonly char Operand2;

        public SwapLetterInstruction(char op1, char op2)
        {
            Operand1 = op1;
            Operand2 = op2;
        }

        public string Execute(string input)
        {
            int index1 = input.IndexOf(Operand1);
            int index2 = input.IndexOf(Operand2);
            return Swap(input, index1, index2);
        }
    }

    internal class RotateLeftInstruction : IInstruction
    {
        public readonly int Operand1;

        public RotateLeftInstruction(int op1)
        {
            Operand1 = op1;
        }

        public string Execute(string input)
            => Rotate(input, Operand1);
    }

    internal class RotateRightInstruction : IInstruction
    {
        public readonly int Operand1;

        public RotateRightInstruction(int op1)
        {
            Operand1 = op1;
        }

        public string Execute(string input)
            => Rotate(input, input.Length - Operand1);
    }

    internal class RotateBasedInstruction : IInstruction
    {
        public readonly char Operand1;

        public RotateBasedInstruction(char op1)
        {
            Operand1 = op1;
        }

        public string Execute(string input)
        {
            var val = input.IndexOf(Operand1) + 1;
            if (val > 4)
                val = (val + 1) % input.Length;
            return Rotate(input, input.Length - val);
        }
    }

    internal class ReverseInstruction : IInstruction
    {
        public readonly int Operand1;
        public readonly int Operand2;

        public ReverseInstruction(int op1, int op2)
        {
            Operand1 = op1;
            Operand2 = op2;
        }

        public string Execute(string input)
        {
            var left = Operand1 > 0 ? input[0..Operand1] : "";

            var middleList = new List<char>(
                input.Substring(Operand1, Operand2 - Operand1 + 1));
            middleList.Reverse();
            var middle = new string(middleList.ToArray());

            var right = Operand2 < input.Length ? input[(Operand2 + 1)..] : "";

            return left + middle + right;
        }
    }

    internal class MoveInstruction : IInstruction
    {
        public readonly int Operand1;
        public readonly int Operand2;

        public MoveInstruction(int op1, int op2)
        {
            Operand1 = op1;
            Operand2 = op2;
        }

        public string Execute(string input)
        {
            var list = new List<char>(input);
            var val = list[Operand1];
            list.RemoveAt(Operand1);
            list.Insert(Operand2, val);
            return new string(list.ToArray());
        }
    }

    private readonly List<IInstruction> _operations;

    public Day21(string filename)
    {
        _operations = new List<IInstruction>();
        foreach (var line in TextFileLines(filename))
            _operations.Add(ParseOperation(line));
    }

    public Day21() : this("Day21.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(Scramble)}: {Scramble()}");
        Console.WriteLine($"{nameof(Unscramble)}: {Unscramble()}");
    }

    public string Scramble()
        =>Evaluate("abcdefgh");

    public string Unscramble()
    {
        foreach (var password in AllPermutations("abcdefgh"))
        {
            var encrypted = Evaluate(password);
            if (encrypted.Equals("fbgdceah"))
                return password;
        }
        throw new Exception("No result found");
    }

    private static IInstruction ParseOperation(string text)
    {
        var tokens = text.Split(' ');
        switch (tokens[0])
        {
            case "swap":
                if (tokens[1].Equals("position"))
                    return new SwapPositionInstruction(int.Parse(tokens[2]), int.Parse(tokens[5]));
                else if (tokens[1] == "letter")
                    return new SwapLetterInstruction(tokens[2][0], tokens[5][0]);
                else
                    throw new ArgumentException($"Unrecognized swap type: {tokens[1]}");
            case "rotate":
                if (tokens[1] == "left")
                    return new RotateLeftInstruction(int.Parse(tokens[2]));
                else if (tokens[1] == "right")
                    return new RotateRightInstruction(int.Parse(tokens[2]));
                else if (tokens[1] == "based")
                    return new RotateBasedInstruction(tokens[6][0]);
                else
                    throw new ArgumentException($"Unrecognized rotate type: {tokens[1]}");
            case "reverse":
                return new ReverseInstruction(int.Parse(tokens[2]), int.Parse(tokens[4]));
            case "move":
                return new MoveInstruction(int.Parse(tokens[2]), int.Parse(tokens[5]));
            default:
                throw new ArgumentException($"Unrecognized op: {tokens[0]}");
        }
    }

    public string Evaluate(string input)
    {
        var curr = input;
        foreach (var inst in _operations)
            curr = inst.Execute(curr);
        return curr;
    }

    static IList<string> AllPermutations(string val)
    {
        var result = new List<string>();
        Permute(val, 0, val.Length - 1, result);
        return result;
    }

    private static void Permute(String str,
        int l, int r, IList<string> result)
    {
        if (l == r)
            result.Add(str);
        else
        {
            for (int i = l; i <= r; i++)
            {
                var option = Swap(str, l, i);
                Permute(option, l + 1, r, result);
            }
        }
    }
    public static String Swap(String val,
                        int i1, int i2)
    {
        var list = new List<char>(val);
        (list[i1], list[i2]) = (list[i2], list[i1]);
        return new string(list.ToArray());
    }

    internal static string Rotate(string input, int posCount)
    {
        return input[posCount..] + input[0..posCount];
    }
}