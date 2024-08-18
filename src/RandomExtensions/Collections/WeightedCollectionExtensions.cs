namespace RandomExtensions.Collections;

public static class WeightedCollectionExtensions
{
    public static T GetItem<T>(this IWeightedCollection<T> collection)
    {
        return collection.GetItem(RandomEx.Shared);
    }

    public static TValue[] GetItems<TValue, TRandom>(this IWeightedCollection<TValue> collection, int length, TRandom random)
        where TRandom : IRandom
    {
        ThrowHelper.ThrowIfLengthIsNegative(length);

        var array = new TValue[length];
        collection.GetItems(random, array.AsSpan());

        return array;
    }

    public static TValue[] GetItems<TValue, TRandom>(this IWeightedCollection<TValue> collection, int length)
        where TRandom : IRandom
    {
        return GetItems(collection, length, RandomEx.Shared);
    }

    public static void GetItems<T>(this IWeightedCollection<T> collection, Span<T> destination)
    {
        collection.GetItems(RandomEx.Shared, destination);
    }
}