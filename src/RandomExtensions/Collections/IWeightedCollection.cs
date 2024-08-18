namespace RandomExtensions.Collections;

public interface IWeightedCollection<T> : IReadOnlyCollection<WeightedValue<T>>
{
    T GetItem<TRandom>(TRandom random) where TRandom : IRandom;
    void GetItems<TRandom>(TRandom random, Span<T> destination) where TRandom : IRandom;
}