using RandomExtensions.Collections;

namespace RandomExtensions.Tests;

public class WeightedListTests
{
    [Test]
    public void Test_RemoveRandom()
    {
        var list = new WeightedList<int>
        {
            { 1, 1.0 },
            { 2, 2.0 },
            { 3, 3.0 },
        };

        Assert.That(list.RemoveRandom(out _), Is.Not.EqualTo(-1));
        Assert.That(list.RemoveRandom(out _), Is.Not.EqualTo(-1));
        Assert.That(list.RemoveRandom(out _), Is.Not.EqualTo(-1));
        Assert.That(list.RemoveRandom(out _), Is.EqualTo(-1));
    }

    [Test]
    public void Test_Values()
    {
        var list = new WeightedList<int>
        {
            { 0, 1.0 },
            { 1, 1.0 },
            { 2, 1.0 },
            { 3, 1.0 },
            { 4, 1.0 },
        };

        var index = 0;
        foreach (var value in list.Values)
        {
            Assert.That(value, Is.EqualTo(index));
            index++;
        }
    }
}