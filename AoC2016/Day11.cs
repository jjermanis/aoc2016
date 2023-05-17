namespace AoC2016;

public class Day11 : DayBase, IDay
{
    // TODO this is slowish (over 50 seconds). Look to optimize.
    // A more simple structure would make this quicker. 
    // TODO maybe priority queue, ordered by move number and quality score?
    // TODO indicates that time is spent in ValidMoves()
    // TODO refactor after optimization attempts

    private enum Kind
    {
        Generator,
        Microchip
    }
    private struct Item
    {
        public readonly string Element;
        public readonly Kind Kind;

        public Item(string element, Kind kind)
        {
            Element = element;
            Kind = kind;
        }
        public override string ToString()
        {
            return $"{Element.ToLower()} {Kind.ToString().ToLower()}";
        }       
    }

    private readonly IList<IList<Item>> _startLayout;
    private readonly int _elementCount;

    public Day11(string filename)
    { 
        var lines = TextFileStringList(filename);
        _startLayout = new List<IList<Item>>();
        for (int i = 0; i < lines.Count; i++)
            _startLayout.Add(new List<Item>());

        for (var f=0; f<lines.Count; f++)
        {
            var tokens = lines[f].Split(' ');
            for (int t = 0; t < tokens.Length; t++)
            {
                var curr = tokens[t];
                _elementCount++;
                if (curr.StartsWith("generator"))
                    _startLayout[f].Add(new Item (tokens[t - 1], Kind.Generator));
                else if (curr.StartsWith("microchip"))
                {
                    var element = (tokens[t - 1].Split('-'))[0];
                    _startLayout[f].Add(new Item (element, Kind.Microchip));
                }
                else
                    _elementCount--;
            }
        }
    }

    public Day11() : this("Day11.txt")
    {
        
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(MinimumSteps)}: {MinimumSteps()}");
        Console.WriteLine($"{nameof(MinimumWithExtraParts)}: {MinimumWithExtraParts()}");
    }

    public int MinimumSteps()
        => Solve(_startLayout, _elementCount);

    public int MinimumWithExtraParts()
    {
        var baseLayout = CopyFloors(_startLayout);
        baseLayout[0].Add(new Item("elerium", Kind.Generator));
        baseLayout[0].Add(new Item("elerium", Kind.Microchip));
        baseLayout[0].Add(new Item("dilithium", Kind.Generator));
        baseLayout[0].Add(new Item("dilithium", Kind.Microchip));
        return Solve(baseLayout, _elementCount + 4);
    }

    private int Solve(IList<IList<Item>> startFloors, int targetCount)
    {
        var visited = new HashSet<long>();
        var bestScore = 0;

        var options = new Queue<(int Floor, IList<IList<Item>> Floors, int MoveCount)>();
        options.Enqueue((0, startFloors, 0));
        while (options.Count > 0)
        {
            var (floorNum, floors, moveCount) = options.Dequeue();
            var qs = QualityScore(floors);
            if (qs + 7 < bestScore)
                continue;
            bestScore = Math.Max(qs, bestScore);

            moveCount++;
            foreach (var newMove in ValidMoves(floorNum, floors))
            {
                var currFloor = newMove.Item1;
                var currFloors = newMove.Item2;

                var id = IdScore(currFloor, currFloors);
                if (visited.Contains(id))
                    continue;

                visited.Add(id);

                var score = QualityScore(currFloors);
                if (score + 7 < bestScore)
                    continue;
                bestScore = Math.Max(score, bestScore);

                if (currFloors[3].Count == targetCount)
                    return moveCount;
                options.Enqueue((currFloor, currFloors, moveCount));
            }
        }
        throw new Exception("No solution found");
    }

    private int QualityScore(IList<IList<Item>> floors)
    {
        var result = 0;
        for (int i = 1; i < 4; i++)
            result += floors[i].Count * (i + 1);
        return result;
    }

    private IEnumerable<(int, IList<IList<Item>>)> ValidMoves(int floorNum, IList<IList<Item>> floors)
    {
        for (var i=0; i < floors[floorNum].Count; i++)
        {            
            if (floorNum < 3)
            {
                var curr = CopyFloors(floors);
                var item = curr[floorNum][i];
                curr[floorNum].RemoveAt(i);

                curr[floorNum+1].Add(item);
                if (AllFloorsValid(curr))
                    yield return (floorNum + 1, curr);
            }
            if (floorNum > 0)
            {
                var curr = CopyFloors(floors);
                var item = curr[floorNum][i];
                curr[floorNum].RemoveAt(i);

                curr[floorNum - 1].Add(item);
                if (AllFloorsValid(curr))
                    yield return (floorNum - 1, curr);
            }

            for (var j=i+1; j < floors[floorNum].Count; j++)
            {
                if (floorNum < 3)
                {
                    var curr = CopyFloors(floors);
                    var item1 = curr[floorNum][i];
                    var item2 = curr[floorNum][j];
                    curr[floorNum].RemoveAt(j);
                    curr[floorNum].RemoveAt(i);

                    curr[floorNum + 1].Add(item1);
                    curr[floorNum + 1].Add(item2);
                    if (AllFloorsValid(curr))
                        yield return (floorNum + 1, curr);
                }
                if (floorNum > 0)
                {
                    var curr = CopyFloors(floors);
                    var item1 = curr[floorNum][i];
                    var item2 = curr[floorNum][j];
                    curr[floorNum].RemoveAt(j);
                    curr[floorNum].RemoveAt(i);

                    curr[floorNum - 1].Add(item1);
                    curr[floorNum - 1].Add(item2);
                    if (AllFloorsValid(curr))
                        yield return (floorNum - 1, curr);
                }

            }
        }
    }

    private IList<IList<Item>> CopyFloors(IList<IList<Item>> floors)
    {
        var result = new List<IList<Item>>();
        for (int i = 0; i < floors.Count; i++)
        {
            var curr = new List<Item>();
            foreach (var item in floors[i])
                curr.Add(item);
            result.Add(curr);
        }
        return result;
    }

    private bool AllFloorsValid(IList<IList<Item>> floors)
    {
        foreach (var floor in floors)
            if (!IsFloorValid(floor))
                return false;
        return true;
    }

    private bool IsFloorValid(IList<Item> floor)
    {
        var generators = floor.Where(x => x.Kind == Kind.Generator);
        if (generators.Count() == 0)
            return true;
        var chips = floor.Where(x => x.Kind == Kind.Microchip);
        if (chips.Count() == 0)
            return true;

        foreach (var chip in chips)
        {
            var matchingGen = generators.Where(x => x.Element.Equals(chip.Element));
            if (matchingGen.Count() == 0)
                return false;
        }
        return true;
    }

    private long IdScore(int floor, IList<IList<Item>> floors)
    {
        var result = 0L;
        for (int i=0; i<floors.Count; i++)
        {
            long b = (long)Math.Pow(13, i + 1);
            for (int x = 0; x < floors[i].Count; x++)
            {
                var curr = floors[i][x];
                long ws = curr.Element[0] + (11 * curr.Element[1]);
                long ks = curr.Kind == Kind.Generator ? 19 : 7;
                result += (b * ws * ks);
            }
        }
        return result + (189733 * floor);
    }
}