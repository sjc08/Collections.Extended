#nullable disable // Safe?
namespace Asjc.Collections.Extended
{
    /// <remarks>
    /// This class inherits from <see cref="UniqueTypeList{TBase}"/>.
    /// </remarks>
    public class UniqueTypeList : UniqueTypeList<object>
    {
        /// <inheritdoc/>
        public UniqueTypeList() : base() { }

        /// <inheritdoc/>
        public UniqueTypeList(int capacity) : base(capacity) { }

        /// <inheritdoc/>
        public UniqueTypeList(IEnumerable<KeyValuePair<Type, object>> pairs) : base(pairs) { }

        /// <inheritdoc/>
        public UniqueTypeList(IEnumerable<object> values) : base(values) { }
    }

    /// <summary>
    /// Represents a list where each type can usually have only one instance.
    /// </summary>
    public class UniqueTypeList<TBase> : KeyedList<Type, TBase>
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueTypeList{TBase}"/> class.
        /// </summary>
        public UniqueTypeList() : base(obj => obj?.GetType() ?? throw new ArgumentNullException(nameof(obj))) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueTypeList{TBase}"/> class that has the specified initial capacity.
        /// </summary>
        public UniqueTypeList(int capacity) : base(obj => obj?.GetType() ?? throw new ArgumentNullException(nameof(obj)), capacity) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueTypeList{TBase}"/> class that contains elements copied from the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        public UniqueTypeList(IEnumerable<KeyValuePair<Type, TBase>> pairs) : base(obj => obj?.GetType() ?? throw new ArgumentNullException(nameof(obj)), pairs) { }

        /// <summary>
        /// Initializes a new instance of the <see cref="UniqueTypeList{TBase}"/> class that contains elements copied from the specified <see cref="IEnumerable{T}"/>.
        /// </summary>
        public UniqueTypeList(IEnumerable<TBase> values) : base(obj => obj?.GetType() ?? throw new ArgumentNullException(nameof(obj)), values) { }

        /// <summary>
        /// Gets the element whose key is the type <typeparamref name="T"/>.
        /// </summary>
        public T Get<T>() where T : TBase => (T)this[typeof(T)];

        /// <summary>
        /// Sets the element whose key is the type <typeparamref name="T"/>.
        /// </summary>
        public void Set<T>(T value) where T : TBase => this[typeof(T)] = value;
    }
}
