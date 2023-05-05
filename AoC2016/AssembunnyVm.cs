namespace AoC2016;

internal class AssembunnyVm
{

    internal interface Instruction
    {
        int? Execute(Dictionary<char, int> registers);
    }

    internal class CpyFromValue : Object, Instruction
    {
        private readonly int ValueSrc;
        private readonly char RegisterDest;

        public CpyFromValue(int valueSrc, char regDest)
        {
            ValueSrc = valueSrc;
            RegisterDest = regDest;
        }

        public override string ToString()
            => $"cpy {ValueSrc} {RegisterDest}";

        public int? Execute(Dictionary<char, int> registers)
        {
            registers[RegisterDest] = ValueSrc;
            return null;
        }
    }

    internal class CpyFromReg : Object, Instruction
    {
        private readonly char RegisterSrc;
        private readonly char RegisterDest;

        public CpyFromReg(char regSrc, char regDest)
        {
            RegisterSrc = regSrc;
            RegisterDest = regDest;
        }

        public override string ToString()
            => $"cpy {RegisterSrc} {RegisterDest}";

        public int? Execute(Dictionary<char, int> registers)
        {
            registers[RegisterDest] = registers[RegisterSrc];
            return null;
        }
    }

    internal class Inc : Object, Instruction
    {
        private readonly char Register;

        public Inc(char register)
        {
            Register = register;
        }

        public override string ToString()
            => $"inc {Register}";

        public int? Execute(Dictionary<char, int> registers)
        {
            registers[Register]++;
            return null;
        }
    }

    internal class Dec : Object, Instruction
    {
        private readonly char Register;

        public Dec(char register)
        {
            Register = register;
        }

        public override string ToString()
            => $"dec {Register}";

        public int? Execute(Dictionary<char, int> registers)
        {
            registers[Register]--;
            return null;
        }
    }

    internal class JmpRegNum : Object, Instruction
    {
        private readonly char RegCheck;
        private readonly int ValueDelta;

        public JmpRegNum(char regCheck, int valueDelta)
        {
            RegCheck = regCheck;
            ValueDelta = valueDelta;
        }

        public override string ToString()
            => $"jnz {RegCheck} {ValueDelta}";

        public int? Execute(Dictionary<char, int> registers)
        {
            if (registers[RegCheck] != 0)
                return ValueDelta;
            return null;
        }
    }

    internal class JmpNumNum : Object, Instruction
    {
        private readonly int ValueCheck;
        private readonly int ValueDelta;

        public JmpNumNum(int valueCheck, int valueDelta)
        {
            ValueCheck = valueCheck;
            ValueDelta = valueDelta;
        }

        public override string ToString()
            => $"jnz {ValueCheck} {ValueDelta}";

        public int? Execute(Dictionary<char, int> registers)
        {
            if (ValueCheck != 0)
                return ValueDelta;
            return null;
        }
    }

    private readonly List<Instruction> _instructions;

    public AssembunnyVm(IEnumerable<string> lines)
    {
        _instructions = new List<Instruction>();
        foreach(var line in lines)
            _instructions.Add(CreateInstruction(line));
    }

    private Instruction CreateInstruction(string line)
    {
        var tokens = line.Split(' ');
        switch (tokens[0])
        {
            case "cpy":
                {
                    var isVal = int.TryParse(tokens[1], out int value);
                    if (isVal)
                        return new CpyFromValue(value, tokens[2][0]);
                    else
                        return new CpyFromReg(tokens[1][0], tokens[2][0]);
                }
            case "inc":
                return new Inc(tokens[1][0]);
            case "dec":
                return new Dec(tokens[1][0]);
            case "jnz":
                {
                    var isVal1 = int.TryParse(tokens[1], out int value1);
                    var value2 = int.Parse(tokens[2]);
                    if (isVal1)
                        return new JmpNumNum(value1, value2);
                    else
                        return new JmpRegNum(tokens[1][0], value2);
                }
            default:
                throw new Exception($"Unhandled: {line}");
        }
    }

    public Dictionary<char, int> RunProgram(Dictionary<char, int> registers)
    {
        for (var pc = 0; pc < _instructions.Count(); pc++)
        {
            var delta = _instructions[pc].Execute(registers);
            if (delta != null)
                pc += delta.Value - 1;
        }
        return registers;
    }

}
