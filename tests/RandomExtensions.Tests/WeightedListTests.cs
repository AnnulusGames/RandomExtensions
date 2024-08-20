using RandomExtensions.Collections;
using RandomExtensions.Linq;

namespace RandomExtensions.Tests;

public class WeightedListTests
{
    [Test]
    public void Test_RemoveRandom()
    {
        var list = Enumerable.Range(0, 100).ToWeightedList(x => 1);
        var hashSet = new HashSet<int>();

        for (int i = 0; i < 100; i++)
        {
            list.RemoveRandom(out var item);
            Assert.That(item, Is.GreaterThanOrEqualTo(0).And.LessThan(100));
            Assert.IsTrue(hashSet.Add(item));
        }

        Assert.Throws<InvalidOperationException>(() => list.RemoveRandom(out _));
    }

    [Test]
    public void Test_GetItem()
    {
        var list = new WeightedList<int>
        {
            { 1, 1.0 },
            { 2, 2.0 },
            { 3, 3.0 },
        };

        for (int i = 0; i < 10000; i++)
        {
            var element = list.GetItem();
            Assert.That(list, Has.Count.EqualTo(3));
            Assert.That(element, Is.EqualTo(1).Or.EqualTo(2).Or.EqualTo(3));
        }

        var empty = new WeightedList<int>();
        Assert.Throws<InvalidOperationException>(() => empty.GetItem());
    }

    [Test]
    public void Test_GetItems()
    {
        var list = new WeightedList<int>
        {
            { 1, 1.0 },
            { 2, 2.0 },
            { 3, 3.0 },
        };

        var elements = list.GetItems(10000);

        for (int i = 0; i < elements.Length; i++)
        {
            Assert.That(elements[i], Is.EqualTo(1).Or.EqualTo(2).Or.EqualTo(3));
        }

        var empty = new WeightedList<int>();
        Assert.Throws<InvalidOperationException>(() => empty.GetItems(10));
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