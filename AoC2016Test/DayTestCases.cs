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

    /*
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