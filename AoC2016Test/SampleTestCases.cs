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
}

