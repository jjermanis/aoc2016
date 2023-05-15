namespace AoC2016;

internal class AssembunnyVm
{
    // TODO try making _instructions a member of all Instructions, to avoid having to pass repeatedly.
    //  Or maybe Instructions all have a reference to AssembunnyVm, which would have needed members?

    internal enum Operator
    {
        Cpy,
        Inc,
        Dec, 
        Jnz,
        Tgl,
        Out
    };

    internal interface Instruction
    {
        Operator Operator { get; }
        int? Execute(int[] registers, int pc, List<Instruction> instructions);
        Instruction Toggle();
    }

    internal class CpyNumReg : Object, Instruction
    {
        private readonly int ValueSrc;
        private readonly int RegisterDest;

        public CpyNumReg(int valueSrc, int regDest)
        {
            ValueSrc = valueSrc;
            RegisterDest = regDest;
        }

        public override string ToString() 
            => $"cpy {ValueSrc} {(char)('a' + RegisterDest)}";

        public Operator Operator => Operator.Cpy;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
        {
            registers[RegisterDest] = ValueSrc;
            return null;
        }

        public Instruction Toggle()
            => new JmpNumReg(ValueSrc, RegisterDest);
    }

    internal class CpyRegReg : Object, Instruction
    {
        private readonly int RegisterSrc;
        private readonly int RegisterDest;

        public CpyRegReg(int regSrc, int regDest)
        {
            RegisterSrc = regSrc;
            RegisterDest = regDest;
        }

        public override string ToString()
            => $"cpy {(char)('a' + RegisterSrc)} {(char)('a' + RegisterDest)}";

        public Operator Operator => Operator.Cpy;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
        {
            registers[RegisterDest] = registers[RegisterSrc];
            return null;
        }

        public Instruction Toggle()
            => new JmpRegReg(RegisterSrc, RegisterDest);
    }

    internal class Inc : Object, Instruction
    {
        private readonly int Register;

        public Inc(int register) 
            => Register = register;

        public override string ToString()
            => $"inc {(char)('a' + Register)}";

        public Operator Operator => Operator.Inc;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
        {
            registers[Register]++;
            return null;
        }

        public Instruction Toggle()
            => new Dec(Register);
    }

    internal class Dec : Object, Instruction
    {
        private readonly int Register;

        public Dec(int register)
        {
            Register = register;
        }

        public override string ToString()
            => $"dec {(char)('a' + Register)}";

        public Operator Operator => Operator.Dec;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
        {
            registers[Register]--;
            return null;
        }

        public Instruction Toggle()
            => new Inc(Register);
    }

    internal class JmpRegNum : Object, Instruction
    {
        private readonly int RegCheck;
        private readonly int ValueDelta;

        public JmpRegNum(int regCheck, int valueDelta)
        {
            RegCheck = regCheck;
            ValueDelta = valueDelta;
        }

        public override string ToString()
            => $"jnz {(char)('a' + RegCheck)} {ValueDelta}";

        public Operator Operator => Operator.Jnz;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
        {
            if (registers[RegCheck] != 0)
                return ValueDelta;
            return null;
        }

        public Instruction Toggle()
            => throw new NotImplementedException();
    }

    internal class JmpNumReg : Object, Instruction
    {
        private readonly int ValueCheck;
        private readonly int RegDelta;

        public JmpNumReg(int valueCheck, int regDelta)
        {
            ValueCheck = valueCheck;
            RegDelta = regDelta;
        }

        public override string ToString()
            => $"jnz {ValueCheck} {(char)('a' + RegDelta)}";

        public Operator Operator => Operator.Jnz;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
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

        public Operator Operator => Operator.Jnz;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
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
        private readonly int RegCheck;
        private readonly int RegDelta;

        public JmpRegReg(int regCheck, int regDelta)
        {
            RegCheck = regCheck;
            RegDelta = regDelta;
        }

        public override string ToString()
            => $"jnz {(char)('a' + RegCheck)} {(char)('a' + RegDelta)}";

        public Operator Operator => Operator.Jnz;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
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
        private readonly int Register;

        public Tgl(int register)
        {
            Register = register;
        }

        public override string ToString()
            => $"tcl {(char)('a' + Register)}";

        public Operator Operator => Operator.Tgl;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
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
            => new Inc(Register);
    }

    internal class OutReg : Object, Instruction
    {
        private readonly int Register;

        public OutReg(int register)
        {
            Register = register;
        }

        public override string ToString()
            => $"out {(char)('a' + Register)}";

        public Operator Operator => Operator.Out;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
            => registers[Register];

        public Instruction Toggle()
            => new Inc(Register);
    }

    internal class OutNum : Object, Instruction
    {
        private readonly int Value;

        public OutNum(int value)
        {
            Value = value;
        }

        public override string ToString()
            => $"out {Value}";

        public Operator Operator => Operator.Out;

        public int? Execute(int[] registers, int pc, List<Instruction> instructions)
            => Value;

        public Instruction Toggle()
            => throw new NotImplementedException();
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
                    var destReg = RegIndex(tokens[2]);
                    if (isVal)
                        return new CpyNumReg(value, destReg);
                    else
                        return new CpyRegReg(RegIndex(tokens[1]), destReg);
                }
            case "inc":
                return new Inc(RegIndex(tokens[1]));
            case "dec":
                return new Dec(RegIndex(tokens[1]));
            case "jnz":
                {
                    var isVal1 = int.TryParse(tokens[1], out int value1);
                    var isVal2 = int.TryParse(tokens[2], out int value2);
                    if (isVal1)
                    {
                        if (isVal2)
                            return new JmpNumNum(value1, value2);
                        else
                            return new JmpNumReg(value1, RegIndex(tokens[2]));
                    }
                    else
                    {
                        if (isVal2)
                            return new JmpRegNum(RegIndex(tokens[1]), value2);
                        else
                            return new JmpRegReg(RegIndex(tokens[1]), RegIndex(tokens[2]));
                    }
                }
            case "tgl":
                return new Tgl(RegIndex(tokens[1]));
            case "out":
                {
                    var isVal = int.TryParse(tokens[1], out int value);
                    if (isVal)
                        return new OutNum(value);
                    else
                        return new OutReg(RegIndex(tokens[1]));
                }
            default:
                throw new Exception($"Unhandled: {line}");
        }
    }

    private int RegIndex(string token)
        => token[0] - 'a';

    public void RunProgram(Dictionary<char, int> registers)
    {
        foreach (var _ in RunProgramWithOutput(registers))
            throw new Exception("Program generates output. Use RunProgramWithOutput for this case.");
    }

    public IEnumerable<int> RunProgramWithOutput(Dictionary<char, int> registers)
    {
        var regArray = new int[registers.Count];
        for (int i = 0; i < registers.Count; i++)
            regArray[i] = registers[(char)('a' + i)];

        var progLen = _instructions.Count();
        for (var pc = 0; pc < progLen; pc++)
        {
            var curr = _instructions[pc];

            var result = curr.Execute(regArray, pc, _instructions);
            if (result != null)
            {
                var val = result.Value;

                switch (curr.Operator)
                {
                    case Operator.Jnz:
                        pc += val - 1;
                        break;
                    case Operator.Out:
                        yield return val;
                        break;
                    default:
                        throw new Exception("No result expected for {curr}");
                }
            }
        }
        for (int i = 0; i < registers.Count; i++)
            registers[(char)('a' + i)] = regArray[i];
    }
}
