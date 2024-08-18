using RandomExtensions;
using RandomExtensions.Linq;

Console.WriteLine(string.Join(':', RandomEnumerable.Repeat(0f, 10f, 100)));