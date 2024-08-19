using BenchmarkDotNet.Configs;
using BenchmarkDotNet.Diagnosers;
using BenchmarkDotNet.Jobs;
using BenchmarkDotNet.Running;
using System.Reflection;

BenchmarkSwitcher.FromAssembly(Assembly.GetEntryAssembly()!).Run(args);

namespace Benchmark
{
    class BenchmarkConfig : ManualConfig
    {
        public BenchmarkConfig()
        {
            AddDiagnoser(MemoryDiagnoser.Default);
            AddJob(Job.ShortRun.WithWarmupCount(10).WithIterationCount(100));
        }
    }
}