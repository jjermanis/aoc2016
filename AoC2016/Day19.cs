namespace AoC2016;

public class Day19 : DayBase, IDay
{
    private readonly int _elfCount;

    public Day19(string filename)
        => _elfCount = TextFileIntList(filename).First();

    public Day19() : this("Day19.txt")
    {
    }

    public void Do()
    {
        Console.WriteLine($"{nameof(LastElfStealNeighbor)}: {LastElfStealNeighbor()}");
        Console.WriteLine($"{nameof(LastElfStealAcross)}: {LastElfStealAcross()}");
    }

    public int LastElfStealNeighbor()
    {
        var remaining = new List<int>();
        for (var x=1; x <= _elfCount; x++)
            remaining.Add(x);
        bool currTakes = true;

        while (remaining.Count > 1)
        {
            var next = new List<int>();

            foreach (var x in remaining)
            {
                if (currTakes)
                    next.Add(x);
                currTakes = !currTakes;
            }
            remaining = next;
        }
        return remaining[0];
    }

    public int LastElfStealAcross()
    {
        var remaining = new LinkedList<int>();
        for (var x = 1; x <= _elfCount; x++)
            remaining.AddLast(x);
        var currNode = remaining.Find((remaining.Count / 2) + 1);
        bool extraStep = remaining.Count % 2 == 1;
        while (remaining.Count > 1)
        {
            if (currNode == null || remaining.First == null)
                throw new Exception("Logical error - null nodes not expected");

            var next = GetNext(currNode, remaining.First);
            remaining.Remove(currNode);
            if (extraStep)
                next = GetNext(next, remaining.First);
            extraStep = !extraStep;
            currNode = next;
        }
        if (remaining.First == null)
            throw new Exception("Logical error");
        return remaining.First.Value;
    }

    private static LinkedListNode<int> GetNext(LinkedListNode<int> curr, LinkedListNode<int> head)
        => curr.Next ?? head;

}