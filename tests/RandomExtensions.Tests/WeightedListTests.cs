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

        list.RemoveRandom(out var item0);
        Assert.That(list, Has.Count.EqualTo(2));
        Assert.That(item0, Is.EqualTo(1).Or.EqualTo(2).Or.EqualTo(3));

        list.RemoveRandom(out var item1);
        Assert.That(list, Has.Count.EqualTo(1));
        Assert.That(item1, Is.EqualTo(1).Or.EqualTo(2).Or.EqualTo(3));

        list.RemoveRandom(out var item2);
        Assert.That(list, Has.Count.EqualTo(0));
        Assert.That(item2, Is.EqualTo(1).Or.EqualTo(2).Or.EqualTo(3));

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