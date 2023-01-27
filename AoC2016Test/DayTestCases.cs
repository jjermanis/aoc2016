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