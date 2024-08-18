namespace RandomExtensions;

public static partial class RandomEx
{
    /// <summary>
    /// Performs an in-place shuffle of an array.
    /// </summary>
    public static void Shuffle<TRandom, TValue>(this TRandom random, TValue[] values)
        where TRandom : IRandom
    {
        ThrowHelper.ThrowIfNull(values);

        for (int i = values.Length - 1; i > 0; i--)
        {
            int r = random.NextInt(i + 1);
            (values[i], values[r]) = (values[r], values[i]);
        }
    }

    /// <summary>
    /// Performs an in-place shuffle of a span.
    /// </summary>
    public static void Shuffle<TRandom, TValue>(this TRandom random, Span<TValue> values)
        where TRandom : IRandom
    {
        for (int i = values.Length - 1; i > 0; i--)
        {
            int r = random.NextInt(i + 1);
            (values[i], values[r]) = (values[r], values[i]);
        }
    }
}