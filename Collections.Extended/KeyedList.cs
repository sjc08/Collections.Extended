using System.Collections;

namespace Asjc.Collections.Extended
{
    /// <inheritdoc/>
    /// <remarks>
    /// This class inherits from <see cref="KeyedList{TKey, TValue}"/>.
    /// </remarks>
    public class KeyedList<TValue> : KeyedList<object, TValue>
    {
        public KeyedList() : base() { }

        public KeyedList(int capacity) : base(capacity) { }

        public KeyedList(IEnumerable<KeyValuePair<object, TValue>> pairs) : base(pairs) { }

        public KeyedList(Func<TValue, object> keySelector) : base(keySelector) { }

        public KeyedList(Func<TValue, object> keySelector, int capacity) : base(keySelector, capacity) { }

        public KeyedList(Func<TValue, object> keySelector, IEnumerable<KeyValuePair<object, TValue>> pairs) : base(keySelector, pairs) { }

        public KeyedList(Func<TValue, object> keySelector, IEnumerable<TValue> values) : base(keySelector, values) { }
    }

    /// <inheritdoc cref="IKeyedList{TKey, TValue}"/>
    public class KeyedList<TKey, TValue> : OrderedDictionary<TKey, TValue>, IKeyedList<TKey, TValue> where TKey : notnull
    {
        public KeyedList()
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        public KeyedList(int capacity) : base(capacity)
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        public KeyedList(IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs)
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        public KeyedList(Func<TValue, TKey> keySelector)
        {
            KeySelector = keySelector;
        }

        public KeyedList(Func<TValue, TKey> keySelector, int capacity) : base(capacity)
        {
            KeySelector = keySelector;
        }

        public KeyedList(Func<TValue, TKey> keySelector, IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs)
        {
            KeySelector = keySelector;
        }

        public KeyedList(Func<TValue, TKey> keySelector, IEnumerable<TValue> values)
        {
            KeySelector = keySelector;
            foreach (var item in values)
                Add(item);
        }

        public new TValue this[int index]
        {
            get => base[index].Value;
            set => base[index] = new(KeySelector(value), value);
        }

        /// <inheritdoc/>
        public Func<TValue, TKey> KeySelector { get; set; }

        bool ICollection<TValue>.IsReadOnly => false;

        public void Add(TValue item) => Add(KeySelector(item), item);

        public bool Contains(TValue item) => Values.Contains(item);

        public void CopyTo(TValue[] array, int arrayIndex) => OrderedValues.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="KeyedList{TKey, TValue}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}.Enumerator"/> for the <see cref="KeyedList{TKey, TValue}"/>.
        /// </returns>
        public new List<TValue>.Enumerator GetEnumerator() => OrderedValues.GetEnumerator();

        public int IndexOf(TValue item) => OrderedValues.IndexOf(item);

        public void Insert(int index, TValue item) => Insert(index, new KeyValuePair<TKey, TValue>(KeySelector(item), item));

        public bool Remove(TValue item)
        {
            int index = IndexOf(item);
            if (index >= 0)
            {
                RemoveAt(index);
                return true;
            }
            return false;
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

        IEnumerator<TValue> IEnumerable<TValue>.GetEnumerator() => GetEnumerator();

        /// <summary>
        /// Converts the specified <see cref="KeyedList{TKey, TValue}"/> to a <see cref="List{T}"/>.
        /// </summary>
        /// <remarks>
        /// Changes to the return value are not reflected in the original dictionary.
        /// </remarks>
        public static explicit operator List<TValue>(KeyedList<TKey, TValue> kl) => kl.OrderedValues;
    }
}
