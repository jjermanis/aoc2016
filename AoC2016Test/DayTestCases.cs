namespace AoC2016Test;

public class DayTestCases
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void Day01()
    {
        var d = new Day01();
        Assert.Multiple(() =>
        {
            Assert.That(d.TotalWalkDistance(), Is.EqualTo(278));
            Assert.That(d.RepeatLocationDistance(), Is.EqualTo(161));
        });
    }

    [Test]
    public void Day02()
    {
        var d = new Day02();
        Assert.Multiple(() =>
        {
            Assert.That(d.SquareCode(), Is.EqualTo(73597));
            Assert.That(d.DiamondCode(), Is.EqualTo("A47DA"));
        });
    }

    [Test]
    public void Day03()
    {
        var d = new Day03();
        Assert.Multiple(() =>
        {
            Assert.That(d.HorizontalTriangleCount(), Is.EqualTo(1032));
            Assert.That(d.VeriticalTriangleCount(), Is.EqualTo(1838));
        });
    }

    [Test]
    public void Day04()
    {
        var d = new Day04();
        Assert.Multiple(() =>
        {
            Assert.That(d.RealRoomIdSum(), Is.EqualTo(158835));
            Assert.That(d.NorthPoleRoomId(), Is.EqualTo(993));
        });
    }

    [Test]
    public void Day05()
    {
        var d = new Day05();
        Assert.Multiple(() =>
        {
            Assert.That(d.Part1(), Is.EqualTo("2414bc77"));
            Assert.That(d.Part2(), Is.EqualTo("437e60fc"));
        });
    }

    [Test]
    public void Day06()
    {
        var d = new Day06();
        Assert.Multiple(() =>
        {
            Assert.That(d.MostCommonEncoding(), Is.EqualTo("tsreykjj"));
            Assert.That(d.LeastCommonEncoding(), Is.EqualTo("hnfbujie"));
        });
    }

    [Test]
    public void Day07()
    {
        var d = new Day07();
        Assert.Multiple(() =>
        {
            Assert.That(d.TlsCount(), Is.EqualTo(118));
            Assert.That(d.SslCount(), Is.EqualTo(260));
        });
    }

    [Test]
    public void Day08()
    {
        var d = new Day08();
        Assert.That(d.LitPixelCount(), Is.EqualTo(106));
        // 8-2 is graphical; no obvious unit test
    }

    [Test]
    public void Day09()
    {
        var d = new Day09();
        Assert.Multiple(() =>
        {
            Assert.That(d.DecompressedLength(), Is.EqualTo(98135));
            Assert.That(d.ImprovedDecompressedLength(), Is.EqualTo(10964557606));
        });
    }

    [Test]
    public void Day10()
    {
        var d = new Day10();
        Assert.Multiple(() =>
        {
            Assert.That(d.SpecificBotCheck(), Is.EqualTo(27));
            Assert.That(d.FirstThreeOutputProduct(), Is.EqualTo(13727));
        });
    }

    [Test]
    public void Day12()
    {
        var d = new Day12();
        Assert.Multiple(() =>
        {
            Assert.That(d.RegisterAValue(), Is.EqualTo(318083));
            Assert.That(d.RegisterAValueWithInitC(), Is.EqualTo(9227737));
        });
    }

    [Test]
    public void Day13()
    {
        var d = new Day13();
        Assert.Multiple(() =>
        {
            Assert.That(d.ShortestPathLength(), Is.EqualTo(82));
            Assert.That(d.DistinctPathsUnderLength50(), Is.EqualTo(138));
        });
    }

    [Test]
    public void Day14()
    {
        var d = new Day14();
        Assert.Multiple(() =>
        {
            Assert.That(d.SimpleKeyNum64(), Is.EqualTo(18626));
            Assert.That(d.ComplexKeyNum64(), Is.EqualTo(20092));
        });
    }

    [Test]
    public void Day15()
    {
        var d = new Day15();
        Assert.Multiple(() =>
        {
            Assert.That(d.FirstClearTime(), Is.EqualTo(400589));
            Assert.That(d.FirstClearTimeWithNewDisc(), Is.EqualTo(3045959));
        });
    }

    [Test]
    public void Day16()
    {
        var d = new Day16();
        Assert.Multiple(() =>
        {
            Assert.That(d.SmallDiskChecksum(), Is.EqualTo("11100111011101111"));
            Assert.That(d.LargeDiskChecksum(), Is.EqualTo("10001110010000110"));
        });
    }

    /*
    [Test]
    public void DayStarter()
    {
        var d = new DayStarter();
        Assert.Multiple(() =>
        {
            Assert.That(d.Part1(), Is.EqualTo(-1));
            Assert.That(d.Part2(), Is.EqualTo(-1));
        });
    }
    */
}