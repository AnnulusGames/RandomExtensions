using BenchmarkDotNet.Attributes;
using RandomExtensions;

namespace Benchmark;

[Config(typeof(BenchmarkConfig))]
public class NextIntBenchmark
{
    [Benchmark(Description = "System.Random")]
    public int SystemRandom()
    {
        return Random.Shared.Next(0, 10);
    }

    [Benchmark(Description = "RandomExtension")]
    public int RandomExtensions()
    {
        return RandomEx.Shared.NextInt(0, 10);
    }
}

[Config(typeof(BenchmarkConfig))]
public class NextDoubleBenchmark
{
    [Benchmark(Description = "System.Random")]
    public double SystemRandom()
    {
        return Random.Shared.NextDouble();
    }

    [Benchmark(Description = "RandomExtension")]
    public double RandomExtensions()
    {
        return RandomEx.Shared.NextDouble();
    }
}