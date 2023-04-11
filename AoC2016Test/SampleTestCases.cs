namespace AoC2016Test;

public class SampleTestCases
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Day01()
    {
        var t1 = new Day01("Day01Test01.txt");
        Assert.That(t1.TotalWalkDistance(), Is.EqualTo(5));

        var t2 = new Day01("Day01Test02.txt");
        Assert.That(t2.TotalWalkDistance(), Is.EqualTo(2));

        var t3 = new Day01("Day01Test03.txt");
        Assert.That(t3.TotalWalkDistance(), Is.EqualTo(12));

        var t4 = new Day01("Day01Test04.txt");
        Assert.That(t4.RepeatLocationDistance(), Is.EqualTo(4));
    }

    [Test]
    public void Day02()
    {
        var d = new Day02("Day02Test.txt");
        Assert.Multiple(() =>
        {
            Assert.That(d.SquareCode(), Is.EqualTo(1985));
            Assert.That(d.DiamondCode(), Is.EqualTo("5DB3"));
        });
    }

    // NOTE: no useful samples for Day 3

    [Test]
    public void Day04()
    {
        var t1 = new Day04("Day04Test.txt");
        Assert.That(t1.RealRoomIdSum(), Is.EqualTo(1514));

        // NOTE: no useful sample for 4-2
    }

    [Test]
    public void Day05()
    {
        var d = new Day05("Day05Test.txt");
        Assert.Multiple(() =>
        {
            Assert.That(d.Part1(), Is.EqualTo("18f47a30"));
            Assert.That(d.Part2(), Is.EqualTo("05ace8e3"));
        });
    }

    [Test]
    public void Day06()
    {
        var d = new Day06("Day06Test.txt");
        Assert.Multiple(() =>
        {
            Assert.That(d.MostCommonEncoding(), Is.EqualTo("easter"));
            Assert.That(d.LeastCommonEncoding(), Is.EqualTo("advent"));
        });
    }

    [Test]
    public void Day07()
    {
        var t1 = new Day07("Day07Test01.txt");
        Assert.That(t1.TlsCount(), Is.EqualTo(2));

        var t2 = new Day07("Day07Test02.txt");
        Assert.That(t2.SslCount(), Is.EqualTo(3));
    }

    // NOTE: no useful samples for Day 8

    [Test]
    public void Day09()
    {
        var t1 = new Day09("Day09Test01.txt");
        Assert.That(t1.DecompressedLength(), Is.EqualTo(6));

        var t2 = new Day09("Day09Test02.txt");
        Assert.That(t2.DecompressedLength(), Is.EqualTo(7));

        var t3 = new Day09("Day09Test03.txt");
        Assert.That(t3.DecompressedLength(), Is.EqualTo(9));
        Assert.That(t3.ImprovedDecompressedLength(), Is.EqualTo(9));

        var t4 = new Day09("Day09Test04.txt");
        Assert.That(t4.DecompressedLength(), Is.EqualTo(11));

        var t5 = new Day09("Day09Test05.txt");
        Assert.That(t5.DecompressedLength(), Is.EqualTo(6));

        var t6 = new Day09("Day09Test06.txt");
        Assert.That(t6.DecompressedLength(), Is.EqualTo(18));

        var t7 = new Day09("Day09Test07.txt");
        Assert.That(t7.ImprovedDecompressedLength(), Is.EqualTo(20));

        var t8 = new Day09("Day09Test08.txt");
        Assert.That(t8.ImprovedDecompressedLength(), Is.EqualTo(241920));

        var t9 = new Day09("Day09Test09.txt");
        Assert.That(t9.ImprovedDecompressedLength(), Is.EqualTo(445));
    }

    // NOTE: no useful samples for Day 10

    [Test]
    public void Day12()
    {
        var d = new Day12("Day12Test.txt");
        Assert.That(d.RegisterAValue(), Is.EqualTo(42));
    }

    [Test]
    public void Day13()
    {
        var d = new Day13("Day13Test.txt");
        Assert.That(d.ShortestPathLength(7, 4), Is.EqualTo(11));
    }

    [Test]
    public void Day14()
    {
        var d = new Day14("Day14Test.txt");
        Assert.Multiple(() =>
        {
            Assert.That(d.SimpleKeyNum64(), Is.EqualTo(22728));
            Assert.That(d.ComplexKeyNum64(), Is.EqualTo(22551));
        });
    }

    [Test]
    public void Day15()
    {
        var d = new Day15("Day15Test.txt");
        Assert.That(d.FirstClearTime(), Is.EqualTo(5));
        // NOTE: no sample for 15-2
    }

    // NOTE: no useful samples for Day 16

    [Test]
    public void Day17()
    {
        var d = new Day17("Day17Test.txt");
        Assert.Multiple(() =>
        {
            Assert.That(d.ShortestPath(), Is.EqualTo("DDRRRD"));
            Assert.That(d.LongestPathLength(), Is.EqualTo(370));
        });
    }

    [Test]
    public void Day18()
    {
        var d = new Day18("Day18Test.txt");
        Assert.That(d.SafeTiles(10), Is.EqualTo(38));
        // NOTE: no sample for 18-2
    }
}

