using RandomExtensions.Algorithms;

namespace RandomExtensions;

/// <summary>
/// IRandom implementation using xoshiro128**
/// </summary>
public sealed class Xoshiro128StarStarRandom : IRandom
{
    Xoshiro128StarStar xoshiro;

    public Xoshiro128StarStarRandom()
    {
        xoshiro = new Xoshiro128StarStar();
    }

    public Xoshiro128StarStarRandom(uint seed)
    {
        InitState(seed);
    }

    public Xoshiro128StarStarRandom(uint s0, uint s1, uint s2, uint s3)
    {
        xoshiro = new Xoshiro128StarStar(s0, s1, s2, s3);
    }

    public void InitState(uint seed)
    {
        xoshiro.S0 = SplitMix32.Next(ref seed);
        xoshiro.S1 = SplitMix32.Next(ref seed);
        xoshiro.S2 = SplitMix32.Next(ref seed);
        xoshiro.S3 = SplitMix32.Next(ref seed);
    }

    public uint NextUInt()
    {
        return xoshiro.Next();
    }

    public ulong NextULong()
    {
        return (((ulong)xoshiro.Next()) << 32) | xoshiro.Next();
    }

    public void Jump()
    {
        xoshiro.Jump();
    }

    public void LongJump()
    {
        xoshiro.LongJump();
    }

    internal ref Xoshiro128StarStar GetState()
    {
        return ref xoshiro;
    }
}