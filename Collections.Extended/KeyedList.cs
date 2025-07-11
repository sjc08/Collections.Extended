using System.Collections;

namespace Asjc.Collections.Extended
{
    /// <inheritdoc cref="IKeyedList{TKey, TValue}"/>
    /// <typeparam name="TKey">The type of keys in the list.</typeparam>
    /// <typeparam name="TValue">The type of values in the list.</typeparam>
    public class KeyedList<TKey, TValue> : OrderedDictionary<TKey, TValue>, IKeyedList<TKey, TValue> where TKey : notnull
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedList{TKey, TValue}"/> class.
        /// </summary>
        public KeyedList()
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedList{TKey, TValue}"/> class that has the specified initial capacity.
        /// </summary>
        public KeyedList(int capacity) : base(capacity)
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedList{TKey, TValue}"/> class that contains elements copied from the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        public KeyedList(IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs)
        {
            KeySelector = _ => throw new NotSupportedException();
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedList{TKey, TValue}"/> class that has the specified key selector function.
        /// </summary>
        public KeyedList(Func<TValue, TKey> keySelector)
        {
            KeySelector = keySelector;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedList{TKey, TValue}"/> class that has the specified key selector function and initial capacity.
        /// </summary>
        public KeyedList(Func<TValue, TKey> keySelector, int capacity) : base(capacity)
        {
            KeySelector = keySelector;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedList{TKey, TValue}"/> class that has the specified key selector function and contains elements copied from the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        public KeyedList(Func<TValue, TKey> keySelector, IEnumerable<KeyValuePair<TKey, TValue>> pairs) : base(pairs)
        {
            KeySelector = keySelector;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="KeyedList{TKey, TValue}"/> class that has the specified key selector function and contains elements copied from the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        public KeyedList(Func<TValue, TKey> keySelector, IEnumerable<TValue> values)
        {
            KeySelector = keySelector;
            foreach (var item in values)
                Add(item);
        }

        /// <inheritdoc/>
        public new TValue this[int index]
        {
            get => base[index].Value;
            set => base[index] = new(KeySelector(value), value);
        }

        /// <inheritdoc/>
        public Func<TValue, TKey> KeySelector { get; set; }

        bool ICollection<TValue>.IsReadOnly => false;

        /// <summary>
        /// Adds an item to the <see cref="KeyedList{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to add to the <see cref="KeyedList{TKey, TValue}"/>.</param>
        public void Add(TValue item) => Add(KeySelector(item), item);

        /// <summary>
        /// Determines whether the <see cref="KeyedList{TKey, TValue}"/> contains a specific value.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="KeyedList{TKey, TValue}"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="item"/> is found in the <see cref="KeyedList{TKey, TValue}"/>; otherwise, <see langword="false"/>.</returns>
        public bool Contains(TValue item) => Values.Contains(item);

        /// <summary>
        /// Copies the elements of the <see cref="KeyedList{TKey, TValue}"/> to an array, starting at the specified array index.
        /// </summary>
        /// <param name="array">The one-dimensional <see cref="Array"/> that is the destination of the elements copied from <see cref="KeyedList{TKey, TValue}"/>. The <see cref="Array"/> must have zero-based indexing.</param>
        /// <param name="arrayIndex">The zero-based index in <paramref name="array"/> at which copying begins.</param>
        public void CopyTo(TValue[] array, int arrayIndex) => OrderedValues.CopyTo(array, arrayIndex);

        /// <summary>
        /// Returns an enumerator that iterates through the <see cref="KeyedList{TKey, TValue}"/>.
        /// </summary>
        /// <returns>
        /// A <see cref="List{T}.Enumerator"/> for the <see cref="KeyedList{TKey, TValue}"/>.
        /// </returns>
        public new List<TValue>.Enumerator GetEnumerator() => OrderedValues.GetEnumerator();

        /// <summary>
        /// Determines the index of a specific item in the <see cref="KeyedList{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to locate in the <see cref="KeyedList{TKey, TValue}"/>.</param>
        /// <returns>The index of <paramref name="item"/> if found in the list; otherwise, -1.</returns>
        public int IndexOf(TValue item) => OrderedValues.IndexOf(item);

        /// <summary>
        /// Inserts an item to the <see cref="KeyedList{TKey, TValue}"/> at the specified index.
        /// </summary>
        /// <param name="index">The zero-based index at which <paramref name="item"/> should be inserted.</param>
        /// <param name="item">The object to insert into the <see cref="KeyedList{TKey, TValue}"/>.</param>
        public void Insert(int index, TValue item) => Insert(index, new KeyValuePair<TKey, TValue>(KeySelector(item), item));

        /// <summary>
        /// Removes the first occurrence of a specific object from the <see cref="KeyedList{TKey, TValue}"/>.
        /// </summary>
        /// <param name="item">The object to remove from the <see cref="KeyedList{TKey, TValue}"/>.</param>
        /// <returns><see langword="true"/> if <paramref name="item"/> was successfully removed from the <see cref="KeyedList{TKey, TValue}"/>; otherwise, <see langword="false"/>. This method also returns <see langword="false"/> if <paramref name="item"/> is not found in the original <see cref="KeyedList{TKey, TValue}"/>.</returns>
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
