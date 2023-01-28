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

}

