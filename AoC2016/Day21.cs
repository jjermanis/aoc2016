namespace AoC2016;

public class Day21 : DayBase, IDay
{
    // TODO compiler warnings are heavy
    // TODO clean up the permutations code

    private enum OpType
    {
        SwapPosition,
        SwapLetter,
        RotateLeft,
        RotateRight,
        RotateBased,
        Reverse,
        Move
    }

    private struct Instruction
    {
        public readonly OpType Operation;
        public readonly object? Operand1;
        public readonly object? Operand2;

        public Instruction(string text)
        {
            var tokens = text.Split(' ');
            switch (tokens[0])
            {
                case "swap":
                    if (tokens[1].Equals("position"))
                    {
                        Operation = OpType.SwapPosition;
                        Operand1 = int.Parse(tokens[2]);
                        Operand2 = int.Parse(tokens[5]);
                    }
                    else if (tokens[1] == "letter")
                    {
                        Operation = OpType.SwapLetter;
                        Operand1 = tokens[2][0];
                        Operand2 = tokens[5][0];
                    }
                    else 
                        throw new ArgumentException($"Unrecognized swap type: {tokens[1]}");
                    break;
                case "rotate":
                    if (tokens[1] == "left")
                    {
                        Operation = OpType.RotateLeft;
                        Operand1 = int.Parse(tokens[2]);
                        Operand2 = null;
                    }
                    else if (tokens[1] == "right")
                    {
                        Operation = OpType.RotateRight;
                        Operand1 = int.Parse(tokens[2]);
                        Operand2 = null;
                    }
                    else if (tokens[1] == "based")
                    {
                        Operation = OpType.RotateBased;
                        Operand1 = tokens[6][0];
                        Operand2 = null;
                    }
                    else
                        throw new ArgumentException($"Unrecognized rotate type: {tokens[1]}");
                    break;
                case "reverse":
                    Operation = OpType.Reverse;
                    Operand1 = int.Parse(tokens[2]);
                    Operand2 = int.Parse(tokens[4]);
                    break;
                case "move":
                    Operation = OpType.Move;
                    Operand1 = int.Parse(tokens[2]);
                    Operand2 = int.Parse(tokens[5]);
                    break;
                default:
                    throw new ArgumentException($"Unrecognized op: {tokens[0]}");
            }
        }

        public string Execute(string input)
        {
            return Operation switch
            {
                OpType.SwapPosition => SwapPosition(input),
                OpType.SwapLetter => SwapLetter(input),
                OpType.RotateLeft => RotateLeft(input),
                OpType.RotateRight => RotateRight(input),
                OpType.RotateBased => RotateBased(input),
                OpType.Reverse => Reverse(input),
                OpType.Move => Move(input),
                _ => throw new Exception($"No implementation for {Operation}")
            };
        }

        string SwapPosition(string input)
            => Swap(input, (int)Operand1, (int)Operand2);

        string SwapLetter(string input)
        {
            int index1 = input.IndexOf((char)Operand1);
            int index2 = input.IndexOf((char)Operand2);
            return Swap(input, index1, index2);
        }

        string Swap(string input, int i1, int i2)
        {
            var list = new List<char>(input);
            char temp = list[i1];
            list[i1] = list[i2];
            list[i2] = temp;
            return new string(list.ToArray());
        }

        string RotateLeft(string input)
            => Rotate(input, (int)Operand1);


        string RotateRight(string input)
            => Rotate(input, input.Length - (int)Operand1);

        string RotateBased(string input)
        {
            char match = (char)Operand1;
            var val = input.IndexOf(match) + 1;
            if (val > 4)
                val = (val + 1) % input.Length;
            return Rotate(input, input.Length - val);
        }

        string Rotate(string input, int posCount)
        {
            return input.Substring(posCount) + input.Substring(0, posCount);
        }

        string Reverse(string input)
        {
            int index1 = (int)Operand1;
            int index2 = (int)Operand2;

            var left = index1 > 0 ? input.Substring(0, index1) : "";

            var middleList = new List<char>(
                input.Substring(index1, index2-index1+1));
            middleList.Reverse();
            var middle = new string(middleList.ToArray());

            var right = index2 < input.Length ? input.Substring(index2 + 1) : "";

            return left + middle + right;
        }

        string Move(string input)
        {
            var list = new List<char>(input);
            var removeIndex = (int)Operand1;
            var insertIndex = (int)Operand2;
            //if (insertIndex > removeIndex)
            //    insertIndex--;
            var val = list[removeIndex];
            list.RemoveAt(removeIndex);
            list.Insert(insertIndex, val);
            return new string(list.ToArray());
        }
    }
    private readonly List<Instruction> _instructions;

    public Day21(string filename)
    { 
        _instructions = new List<Instruction>();
        foreach (var line in TextFileLines(filename))
            _instructions.Add(new Instruction(line));
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

    public string Evaluate(string input)
    {
        var curr = input;
        foreach (var inst in _instructions)
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
        char temp = list[i1];
        list[i1] = list[i2];
        list[i2] = temp;
        return new string(list.ToArray());
    }
}