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

        list.RemoveRandom(out _);
        Assert.That(list, Has.Count.EqualTo(2));

        list.RemoveRandom(out _);
        Assert.That(list, Has.Count.EqualTo(1));

        list.RemoveRandom(out _);
        Assert.That(list, Has.Count.EqualTo(0));

        Assert.Throws<InvalidOperationException>(() => list.RemoveRandom(out _));
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