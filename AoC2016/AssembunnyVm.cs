namespace AoC2016;

internal class AssembunnyVm
{
    // TODO try making _instructions a member of all Instructions, to avoid having to pass repeatedly.
    //  Or maybe Instructions all have a reference to AssembunnyVm, which would have needed members?
    // TODO switch to using an array internally, convert to Dictionary at end

    internal interface Instruction
    {
        int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions);
        Instruction Toggle();
    }

    internal class CpyNumReg : Object, Instruction
    {
        private readonly int ValueSrc;
        private readonly char RegisterDest;

        public CpyNumReg(int valueSrc, char regDest)
        {
            ValueSrc = valueSrc;
            RegisterDest = regDest;
        }

        public override string ToString()
            => $"cpy {ValueSrc} {RegisterDest}";

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            registers[RegisterDest] = ValueSrc;
            return null;
        }

        public Instruction Toggle()
            => new JmpNumReg(ValueSrc, RegisterDest);
    }

    internal class CpyRegReg : Object, Instruction
    {
        private readonly char RegisterSrc;
        private readonly char RegisterDest;

        public CpyRegReg(char regSrc, char regDest)
        {
            RegisterSrc = regSrc;
            RegisterDest = regDest;
        }

        public override string ToString()
            => $"cpy {RegisterSrc} {RegisterDest}";

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            registers[RegisterDest] = registers[RegisterSrc];
            return null;
        }

        public Instruction Toggle()
            => new JmpRegReg(RegisterSrc, RegisterDest);
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

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            registers[Register]++;
            return null;
        }

        public Instruction Toggle()
            => new Dec(Register);
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

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            registers[Register]--;
            return null;
        }

        public Instruction Toggle()
            => new Inc(Register);

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

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            if (registers[RegCheck] != 0)
                return ValueDelta;
            return null;
        }

        public Instruction Toggle()
        {
            throw new NotImplementedException();
        }
    }

    internal class JmpNumReg : Object, Instruction
    {
        private readonly int ValueCheck;
        private readonly char RegDelta;

        public JmpNumReg(int valueCheck, char regDelta)
        {
            ValueCheck = valueCheck;
            RegDelta = regDelta;
        }

        public override string ToString()
            => $"jnz {ValueCheck} {RegDelta}";

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            if (ValueCheck != 0)
                return registers[RegDelta];
            return null;
        }

        public Instruction Toggle()
            => new CpyNumReg(ValueCheck, RegDelta);
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

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            if (ValueCheck != 0)
                return ValueDelta;
            return null;
        }

        public Instruction Toggle()
        {
            throw new NotImplementedException();
        }
    }

    internal class JmpRegReg : Object, Instruction
    {
        private readonly char RegCheck;
        private readonly char RegDelta;

        public JmpRegReg(char regCheck, char regDelta)
        {
            RegCheck = regCheck;
            RegDelta = regDelta;
        }

        public override string ToString()
            => $"jnz {RegCheck} {RegDelta}";

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            if (registers[RegCheck] != 0)
                return registers[RegDelta];
            return null;
        }

        public Instruction Toggle()
        {
            throw new NotImplementedException();
        }
    }

    internal class Tgl : Object, Instruction
    {
        private readonly char Register;

        public Tgl(char register)
        {
            Register = register;
        }

        public override string ToString()
            => $"tcl {Register}";

        public int? Execute(Dictionary<char, int> registers, int pc, List<Instruction> instructions)
        {
            var dest = pc + registers[Register];
            if (dest < instructions.Count)
            {
                var newInst = instructions[dest].Toggle();
                instructions[dest] = newInst;
            }
            return null;
        }

        public Instruction Toggle()
        {
            return new Inc(Register);
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
                        return new CpyNumReg(value, tokens[2][0]);
                    else
                        return new CpyRegReg(tokens[1][0], tokens[2][0]);
                }
            case "inc":
                return new Inc(tokens[1][0]);
            case "dec":
                return new Dec(tokens[1][0]);
            case "jnz":
                {
                    var isVal1 = int.TryParse(tokens[1], out int value1);
                    var isVal2 = int.TryParse(tokens[2], out int value2);
                    if (isVal1)
                    {
                        if (isVal2)
                            return new JmpNumNum(value1, value2);
                        else
                            return new JmpNumReg(value1, tokens[2][0]);
                    }
                    else
                    {
                        if (isVal2)
                            return new JmpRegNum(tokens[1][0], value2);
                        else
                            return new JmpRegReg(tokens[1][0], tokens[2][0]);
                    }
                }
            case "tgl":
                return new Tgl(tokens[1][0]);
            default:
                throw new Exception($"Unhandled: {line}");
        }
    }

    public Dictionary<char, int> RunProgram(Dictionary<char, int> registers)
    {
        for (var pc = 0; pc < _instructions.Count(); pc++)
        {
            var delta = _instructions[pc].Execute(registers, pc, _instructions);
            if (delta != null)
                pc += delta.Value - 1;
        }
        return registers;
    }

}
