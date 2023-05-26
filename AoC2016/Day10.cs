namespace AoC2016;

public class Day10 : DayBase, IDay
{
    // TODO refactoring would help here, for readibility
    // TODO refactor the very similar Part 1 and 2... maybe exec in ctor?

    private struct Destination
    {
        public readonly bool IsOutput;
        public readonly int Value;

        public Destination(string type, string value)
        {
            IsOutput = type.Equals("output");
            Value = int.Parse(value);
        }
    }
    private readonly Dictionary<int, (Destination low, Destination hi)> _rules;
    private readonly Dictionary<int, (int? a, int? b)> _bots;
    private readonly Dictionary<int, int> _output;

    public Day10(string filename)
    {
        _rules = new Dictionary<int, (Destination low, Destination hi)>();
        _bots = new Dictionary<int, (int? a, int? b)>();
        _output = new Dictionary<int, int>();

        var lines = TextFileLines(filename);
        foreach (var line in lines)
        {
            if (line[0] == 'b')
            {
                var m = Regex.Match(line,
                    @"bot (\d*) gives low to ([a-z]+) (\d*) and high to ([a-z]+) (\d*)");
                var id = int.Parse(m.Groups[1].Value);
                var low = new Destination(m.Groups[2].Value, m.Groups[3].Value);
                var hi = new Destination(m.Groups[4].Value, m.Groups[5].Value);
                _rules[id] = (low, hi);
            }
            else
            {
                var m = Regex.Match(line,
                    @"value (\d*) goes to bot (\d*)");
                var value = int.Parse(m.Groups[1].Value);
                var id = int.Parse(m.Groups[2].Value);
                if (!_bots.ContainsKey(id))
                    _bots[id] = (value, null);
                else
                {
                    var (currA, _) = _bots[id];
                    _bots[id] = (currA, value);
                }
            }
        }
    }

    public Day10() : this("Day10.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(SpecificBotCheck)}: {SpecificBotCheck()}");
        Console.WriteLine($"{nameof(FirstThreeOutputProduct)}: {FirstThreeOutputProduct()}");
    }

    public int SpecificBotCheck()
    {
        var fullBots = new Queue<int>();
        foreach(var id in _bots.Keys)
            if (_bots[id].b != null)
                fullBots.Enqueue(id);
        while (fullBots.Count > 0)
        {
            var currId = fullBots.Dequeue();
            var (currBotA, currBotB) = _bots[currId];
            var currRules = _rules[currId];

            if (currBotA == null || currBotB == null)
                throw new Exception("Logical error: bots with an empty dest should not be in the queue");

            var low = Math.Min(currBotA.Value, currBotB.Value);
            var high = Math.Max(currBotA.Value, currBotB.Value);

            if (low == 17 && high == 61)
                return currId;

            var x = UpdateDestination(currRules.low, low);
            if (x != null)
                fullBots.Enqueue(x.Value);
            x = UpdateDestination(currRules.hi, high);
            if (x != null)
                fullBots.Enqueue(x.Value);

            _bots[currId] = (null, null);
        }

        return 0;
    }

    public int FirstThreeOutputProduct()
    {
        var fullBots = new Queue<int>();
        foreach (var id in _bots.Keys)
            if (_bots[id].b != null)
                fullBots.Enqueue(id);
        while (fullBots.Count > 0)
        {
            var currId = fullBots.Dequeue();
            var (currBotA, currBotB) = _bots[currId];
            var currRules = _rules[currId];

            if (currBotA == null || currBotB == null)
                throw new Exception("Logical error: bots with an empty dest should not be in the queue");

            var low = Math.Min(currBotA.Value, currBotB.Value);
            var high = Math.Max(currBotA.Value, currBotB.Value);

            var x = UpdateDestination(currRules.low, low);
            if (x != null)
                fullBots.Enqueue(x.Value);
            x = UpdateDestination(currRules.hi, high);
            if (x != null)
                fullBots.Enqueue(x.Value);

            _bots[currId] = (null, null);
        }

        return _output[0] * _output[1] * _output[2];
    }

    private int? UpdateDestination(Destination rule, int value)
    {
        if (rule.IsOutput)
        {
            _output[rule.Value] = value;
            return null;
        }
        else
        {
            if (_bots.ContainsKey(rule.Value))
            {
                var (currA, _) = _bots[rule.Value];
                _bots[rule.Value] = (currA, value);
                return rule.Value;
            }
            else
            {
                _bots[rule.Value] = (value, null);
                return null;
            }
        }
    }
}