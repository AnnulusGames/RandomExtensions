using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using RandomExtensions.Algorithms;

namespace RandomExtensions;

public static partial class RandomEx
{
    /// <summary>
    /// Returns a random bool value.
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static bool NextBool<T>(this T random)
        where T : IRandom
    {
        return (random.NextUInt() & 1) == 1;
    }

    /// <summary>
    /// Returns a random uint value in [0, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint NextUInt<T>(this T random, uint max)
        where T : IRandom
    {
        return (uint)((random.NextUInt() * (ulong)max) >> 32);
    }

    /// <summary>
    /// Returns a random uint value in [min, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static uint NextUInt<T>(this T random, uint min, uint max)
        where T : IRandom
    {
        ThrowHelper.CheckMinMax(min, max);
        uint range = max - min;
        return (uint)(random.NextUInt() * (ulong)range >> 32) + min;
    }

    /// <summary>
    /// Returns a random int value in [int.MinValue, int.MaxValue].
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int NextInt<T>(this T random)
        where T : IRandom
    {
        return (int)random.NextUInt() ^ -2147483648;
    }

    /// <summary>
    /// Returns a random int value in [0, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int NextInt<T>(this T random, int max)
        where T : IRandom
    {
        ThrowHelper.CheckMax(max);
        return (int)((random.NextUInt() * (ulong)max) >> 32);
    }

    /// <summary>
    /// Returns a random int value in [min, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static int NextInt<T>(this T random, int min, int max)
        where T : IRandom
    {
        ThrowHelper.CheckMinMax(min, max);
        var range = (uint)(max - min);
        return (int)(random.NextUInt() * (ulong)range >> 32) + min;
    }

    /// <summary>
    /// Returns a random ulong value in [0, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong NextULong<T>(this T random, ulong max)
        where T : IRandom
    {
        if (max <= uint.MaxValue)
        {
            return random.NextUInt();
        }

        return random.NextULong() % (max + 1);
    }

    /// <summary>
    /// Returns a random ulong value in [min, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static ulong NextULong<T>(this T random, ulong min, ulong max)
        where T : IRandom
    {
        ThrowHelper.CheckMinMax(min, max);
        return NextULong(random, max - min) + min;
    }

    /// <summary>
    /// Returns a random long value in [long.MinValue, long.MaxValue].
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static long NextLong<T>(this T random)
        where T : IRandom
    {
        return (long)random.NextULong();
    }

    /// <summary>
    /// Returns a random long value in [0, max).
    /// </summary>
    public static long NextLong<T>(this T random, long max)
        where T : IRandom
    {
        ThrowHelper.CheckMax(max);

        if (max <= int.MaxValue)
        {
            return (int)((random.NextUInt() * (ulong)max) >> 32);
        }

        if (max > 1)
        {
            int bits = Log2Ceiling((ulong)max);
            while (true)
            {
                ulong result = random.NextULong() >> (sizeof(ulong) * 8 - bits);
                if (result < (ulong)max)
                {
                    return (long)result;
                }
            }
        }

        return 0;
    }

    /// <summary>
    /// Returns a random long value in [min, max).
    /// </summary>
    public static long NextLong<T>(this T random, long min, long max)
        where T : IRandom
    {
        ulong range = (ulong)(max - min);

        if (range <= int.MaxValue)
        {
            return NextInt(random, (int)range) + min;
        }

        if (range > 1)
        {
            int bits = Log2Ceiling(range);
            while (true)
            {
                ulong result = random.NextULong() >> (sizeof(ulong) * 8 - bits);
                if (result < range)
                {
                    return (long)result + min;
                }
            }
        }

        return min;
    }

    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    static int Log2Ceiling(ulong value)
    {
        int result = BitOperations.Log2(value);
        if (BitOperations.PopCount(value) != 1)
        {
            result++;
        }
        return result;
    }

    /// <summary>
    /// Returns a random float value in [0.0, 1.0).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NextFloat<T>(this T random)
        where T : IRandom
    {
        return (random.NextUInt() >> 8) * (1.0f / (1u << 24));
    }

    /// <summary>
    /// Returns a random float value in [0.0, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NextFloat<T>(this T random, float max)
        where T : IRandom
    {
        ThrowHelper.CheckMax(max);
        return NextFloat(random) * max;
    }

    /// <summary>
    /// Returns a random float value in [min, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static float NextFloat<T>(this T random, float min, float max)
        where T : IRandom
    {
        ThrowHelper.CheckMinMax(min, max);
        return NextFloat(random) * (max - min) + min;
    }

    /// <summary>
    /// Returns a random double value in [0.0, 1.0).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NextDouble<T>(this T random)
        where T : IRandom
    {
        return (random.NextULong() >> 11) * (1.0 / (1ul << 53));
    }

    /// <summary>
    /// Returns a random double value in [0.0, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NextDouble<T>(this T random, double max)
        where T : IRandom
    {
        ThrowHelper.CheckMax(max);
        return NextDouble(random) * max;
    }

    /// <summary>
    /// Returns a random double value in [min, max).
    /// </summary>
    [MethodImpl(MethodImplOptions.AggressiveInlining)]
    public static double NextDouble<T>(this T random, double min, double max)
        where T : IRandom
    {
        ThrowHelper.CheckMinMax(min, max);
        return NextDouble(random) * (max - min) + min;
    }

    /// <summary>
    /// Returns a Gaussian distributed random double value with mean 0.0 and standard deviation 1.0.
    /// </summary>
    public static double NextDoubleGaussian<T>(this T random)
        where T : IRandom
    {
        double v1, v2, s;
        do
        {
            v1 = 2 * NextDouble(random) - 1;
            v2 = 2 * NextDouble(random) - 1;
            s = v1 * v1 + v2 * v2;
        } while (s >= 1 || s == 0);

        double multiplier = Math.Sqrt(-2 * Math.Log(s) / s);
        return v1 * multiplier;
    }

    /// <summary>
    /// Fills the buffer with random bytes [0..0x7f].
    /// </summary>
    public static unsafe void NextBytes<T>(this T random, byte[] buffer)
        where T : IRandom
    {
        ThrowHelper.ThrowIfNull(buffer);
        NextBytes(random, buffer.AsSpan());
    }

    /// <summary>
    /// Fills the buffer with random bytes [0..0x7f].
    /// </summary>
    public static unsafe void NextBytes<T>(this T random, Span<byte> buffer)
        where T : IRandom
    {
        if (random is Xoshiro128StarStarRandom xoshiro1)
        {
            FastNextBytes(ref xoshiro1.GetState(), buffer);
        }
        else if (random is SharedRandom shared && shared.GetLocalInstance() is Xoshiro128StarStarRandom xoshiro2)
        {
            FastNextBytes(ref xoshiro2.GetState(), buffer);
        }
        else
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = (byte)random.NextUInt();
            }
        }
    }

    static unsafe void FastNextBytes(ref Xoshiro128StarStar state, Span<byte> buffer)
    {
        uint s0 = state.S0, s1 = state.S1, s2 = state.S2, s3 = state.S3;

        while (buffer.Length >= sizeof(uint))
        {
            Unsafe.WriteUnaligned(
                ref MemoryMarshal.GetReference(buffer),
                BitOperations.RotateLeft(s1 * 5, 7) * 9);

            uint t = s1 << 9;
            s2 ^= s0;
            s3 ^= s1;
            s1 ^= s2;
            s0 ^= s3;
            s2 ^= t;
            s3 = BitOperations.RotateLeft(s3, 11);

            buffer = buffer[sizeof(uint)..];
        }

        if (!buffer.IsEmpty)
        {
            uint next = BitOperations.RotateLeft(s1 * 5, 7) * 9;
            byte* remainingBytes = (byte*)&next;

            for (int i = 0; i < buffer.Length; i++)
            {
                buffer[i] = remainingBytes[i];
            }

            uint t = s1 << 9;
            s2 ^= s0;
            s3 ^= s1;
            s1 ^= s2;
            s0 ^= s3;
            s2 ^= t;
            s3 = BitOperations.RotateLeft(s3, 11);
        }

        state.S0 = s0;
        state.S1 = s1;
        state.S2 = s2;
        state.S3 = s3;
    }
}