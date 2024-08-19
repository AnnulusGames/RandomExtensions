using System.Collections;

namespace RandomExtensions.Collections;

public interface IReadOnlyWeightedList<T> : IWeightedCollection<T>, IReadOnlyList<WeightedValue<T>>
{
}

public class WeightedList<T> : IReadOnlyWeightedList<T>, IList<WeightedValue<T>>
{
    public sealed class ValueCollection(WeightedList<T> list) : IReadOnlyList<T>
    {
        public T this[int index] => list[index].Value;
        public int Count => list.Count;
        public bool IsReadOnly => true;

        public Enumerator GetEnumerator()
        {
            return new Enumerator(list);
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            return GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public struct Enumerator(WeightedList<T> list) : IEnumerator<T>
        {
            int offset;
            T current;

            public T Current => current;
            object? IEnumerator.Current => current;

            public bool MoveNext()
            {
                if (offset == list.Count) return false;
                current = list[offset].Value;
                offset++;
                return true;
            }

            public void Dispose()
            {
            }

            public void Reset()
            {
                throw new NotSupportedException();
            }
        }
    }

    public WeightedList(int capacity)
    {
        list = new(capacity);
        values = new(this);
    }

    public WeightedList()
    {
        list = [];
        values = new(this);
    }

    double totalWeight;
    readonly List<WeightedValue<T>> list;
    readonly ValueCollection values;

    public WeightedValue<T> this[int index]
    {
        get => list[index];
        set
        {
            totalWeight -= list[index].Weight;
            list[index] = value;
            totalWeight += value.Weight;
        }
    }

    public int Count => list.Count;
    public bool IsReadOnly => false;

    public ValueCollection Values => values;

    public void Add(WeightedValue<T> item)
    {
        list.Add(item);
        totalWeight += item.Weight;
    }

    public void Add(T value, double weight)
    {
        Add(new WeightedValue<T>(value, weight));
    }

    public void Clear()
    {
        list.Clear();
    }

    public bool Contains(WeightedValue<T> item)
    {
        return list.Contains(item);
    }

    public bool Contains(T item)
    {
        return IndexOf(item) != -1;
    }

    public void CopyTo(WeightedValue<T>[] array, int arrayIndex)
    {
        list.CopyTo(array, arrayIndex);
    }

    public Enumerator GetEnumerator()
    {
        return new Enumerator(this);
    }

    public T GetItem(IRandom random)
    {
        var r = random.NextDouble() * totalWeight;
        var current = 0.0;

        for (int i = 0; i < list.Count; i++)
        {
            current += list[i].Weight;
            if (r <= current)
            {
                return list[i].Value;
            }
        }

        return default!;
    }

    public void GetItems(IRandom random, Span<T> destination)
    {
        for (int i = 0; i < destination.Length; i++)
        {
            destination[i] = GetItem(random);
        }
    }

    public int IndexOf(WeightedValue<T> item)
    {
        return list.IndexOf(item);
    }

    public int IndexOf(T item)
    {
        for (int i = 0; i < list.Count; i++)
        {
            if (EqualityComparer<T>.Default.Equals(list[i].Value, item)) return i;
        }

        return -1;
    }

    public void Insert(int index, WeightedValue<T> item)
    {
        list.Insert(index, item);
        totalWeight += item.Weight;
    }

    public void Insert(int index, T item, double weight)
    {
        Insert(index, new WeightedValue<T>(item, weight));
    }

    public bool Remove(WeightedValue<T> item)
    {
        if (list.Remove(item))
        {
            totalWeight -= item.Weight;
            return true;
        }

        return false;
    }

    public bool Remove(T item)
    {
        var index = IndexOf(item);
        if (index == -1) return false;

        RemoveAt(index);
        return true;
    }

    public void RemoveAt(int index)
    {
        var value = list[index];
        list.RemoveAt(index);
        totalWeight -= value.Weight;
    }

    public int RemoveRandom(IRandom random, out T item)
    {
        var r = random.NextDouble() * totalWeight;
        var current = 0.0;

        for (int i = 0; i < list.Count; i++)
        {
            current += list[i].Weight;
            if (r <= current)
            {
                item = list[i].Value;
                return i;
            }
        }

        item = default!;
        return -1;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return GetEnumerator();
    }

    IEnumerator<WeightedValue<T>> IEnumerable<WeightedValue<T>>.GetEnumerator()
    {
        return GetEnumerator();
    }

    public struct Enumerator(WeightedList<T> list) : IEnumerator<WeightedValue<T>>
    {
        int offset;
        WeightedValue<T> current;

        public WeightedValue<T> Current => current;
        object? IEnumerator.Current => current;

        public bool MoveNext()
        {
            if (offset == list.Count) return false;
            current = list[offset];
            offset++;
            return true;
        }

        public void Dispose()
        {
        }

        public void Reset()
        {
            throw new NotSupportedException();
        }
    }
}