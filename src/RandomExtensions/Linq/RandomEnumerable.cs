using System.Runtime.InteropServices;

namespace RandomExtensions.Linq;

/// <summary>
/// Provides additional LINQ methods that utilize random numbers.
/// </summary>
public static class RandomEnumerable
{
    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<int> Repeat(int min, int max, int length)
    {
        return Repeat(min, max, length, RandomEx.Shared);
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<int> Repeat<TRandom>(int min, int max, int length, TRandom random)
        where TRandom : IRandom
    {
        for (int i = 0; i < length; i++)
        {
            yield return random.NextInt(min, max);
        }
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<uint> Repeat(uint min, uint max, int length)
    {
        return Repeat(min, max, length, RandomEx.Shared);
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<uint> Repeat<TRandom>(uint min, uint max, int length, TRandom random)
        where TRandom : IRandom
    {
        for (int i = 0; i < length; i++)
        {
            yield return random.NextUInt(min, max);
        }
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<long> Repeat(long min, long max, int length)
    {
        return Repeat(min, max, length, RandomEx.Shared);
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<long> Repeat<TRandom>(long min, long max, int length, TRandom random)
        where TRandom : IRandom
    {
        for (int i = 0; i < length; i++)
        {
            yield return random.NextLong(min, max);
        }
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<ulong> Repeat(ulong min, ulong max, int length)
    {
        return Repeat(min, max, length, RandomEx.Shared);
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<ulong> Repeat<TRandom>(ulong min, ulong max, int length, TRandom random)
        where TRandom : IRandom
    {
        for (int i = 0; i < length; i++)
        {
            yield return random.NextULong(min, max);
        }
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<float> Repeat(float min, float max, int length)
    {
        return Repeat(min, max, length, RandomEx.Shared);
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<float> Repeat<TRandom>(float min, float max, int length, TRandom random)
        where TRandom : IRandom
    {
        for (int i = 0; i < length; i++)
        {
            yield return random.NextFloat(min, max);
        }
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<double> Repeat(double min, double max, int length)
    {
        return Repeat(min, max, length, RandomEx.Shared);
    }

    /// <summary>
    /// Creates a repeating sequence of random values ​​in the range [min, max).
    /// </summary>
    public static IEnumerable<double> Repeat<TRandom>(double min, double max, int length, TRandom random)
        where TRandom : IRandom
    {
        for (int i = 0; i < length; i++)
        {
            yield return random.NextDouble(min, max);
        }
    }

    /// <summary>
    /// Returns a random element of a sequence.
    /// </summary>
    public static T RandomElement<T>(this IEnumerable<T> source)
    {
        return RandomElement(source, RandomEx.Shared);
    }

    /// <summary>
    /// Returns a random element of a sequence.
    /// </summary>
    public static TValue RandomElement<TValue, TRandom>(this IEnumerable<TValue> source, TRandom random)
        where TRandom : IRandom
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        if (source is TValue[] array)
        {
            return random.GetItem(array);
        }
        if (source is List<TValue> list)
        {
            return random.GetItem<TRandom, TValue>(CollectionsMarshal.AsSpan(list));
        }
        if (source is IReadOnlyList<TValue> readOnlyList)
        {
            if (readOnlyList.Count == 0) throw new ArgumentException("The collection is empty.");
            return readOnlyList[random.NextInt(0, readOnlyList.Count)];
        }

        var count = source.Count();
        if (count == 0) throw new ArgumentException("The collection is empty.");

        return source.ElementAt(random.NextInt(0, count));
    }

    /// <summary>
    /// Returns a shuffled sequence.
    /// </summary>
    public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> source)
    {
        return Shuffle(source, RandomEx.Shared);
    }

    /// <summary>
    /// Returns a shuffled sequence.
    /// </summary>
    public static IEnumerable<TValue> Shuffle<TValue, TRandom>(this IEnumerable<TValue> source, TRandom random)
        where TRandom : IRandom
    {
        if (source == null) throw new ArgumentNullException(nameof(source));

        var buffer = source.ToArray();
        
        for (int i = 0; i < buffer.Length; i++)
        {
            int j = random.NextInt(i, buffer.Length);
            yield return buffer[j];
            buffer[j] = buffer[i];
        }
    }
}