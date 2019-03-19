using System;

namespace RM.Common.Utils
{
    /// <summary> A basic generic object pool. </summary>
    /// <typeparam name="T"> The type of objects to pool. </typeparam>
    public class ObjectPool<T> where T : class
    {
        private readonly object _arrayLocker = new object();
        private readonly Func<T> _creator;
        private readonly Action<T> _resetAction;

        private T[] _array;
        private int _size;

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class.
        /// </summary>
        /// <param name="creator">A function for creating the instances of <typeparamref name="T" /></param>
        public ObjectPool(Func<T> creator) : this(creator, null, 0, 16)
        { }

        /// <summary>
        /// Initializes a new instance of the <see cref="ObjectPool{T}"/> class.
        /// </summary>
        /// <param name="creator">A function for creating the instances of <typeparamref name="T" /></param>
        /// <param name="initialCount">How many object instances to create initially.</param>
        /// <param name="initialCapacity">How large the initial capacity of the pool should be. Set this high enough that you won't exceed it, but not so high as to needlessly waste memory.</param>
        public ObjectPool(Func<T> creator, int initialCount, int initialCapacity) : this(creator, null, initialCount, initialCapacity)
        { }

        /// <summary>
        /// Creates the object pool with <paramref name="initialCount" /> number of instances created and added to the pool. The pool will have an initial capacity of <paramref name="initialCapacity" />.
        /// </summary>
        /// <param name="creator">A function for creating the instances of <typeparamref name="T" /></param>
        /// <param name="resetAction">An action for reseting the state of the instances of type <typeparamref name="T" /> to the initial state.</param>
        /// <param name="initialCount">How many object instances to create initially.</param>
        /// <param name="initialCapacity">How large the initial capacity of the pool should be. Set this high enough that you won't exceed it, but not so high as to needlessly waste memory.</param>
        public ObjectPool(Func<T> creator, Action<T> resetAction, int initialCount, int initialCapacity)
        {
            if (creator == null) throw new ArgumentNullException(nameof(creator));
            if (initialCount < 0 || initialCount > initialCapacity) throw new ArgumentOutOfRangeException(nameof(initialCount));
            if (initialCapacity < 0) throw new ArgumentOutOfRangeException(nameof(initialCapacity));

            _creator = creator;
            _resetAction = resetAction;

            _array = new T[initialCapacity];
            _size = initialCount;

            for (var i = 0; i < initialCount; i++) _array[i] = creator();
        }

        /// <summary>
        /// Gets an instance of <typeparamref name="T" /> from the pool. If the pool is empty, a new instance of <typeparamref name="T" /> is created and returned to you instead.
        /// </summary>
        /// <returns>An unused instance of <typeparamref name="T" />.</returns>
        public T Acquire()
        {
            lock (_arrayLocker)
            {
                if (_size == 0) return _creator();
                var obj = _array[--_size];
                _array[_size] = null;
                return obj;
            }
        }

        /// <summary>
        /// Return (or add) an existing instance of <typeparamref name="T" /> to the pool.
        /// </summary>
        /// <param name="item"> The instance of <typeparamref name="T" /> to return to the pool.</param>
        /// <exception cref="System.ArgumentNullException"> Thrown when <paramref name="item" /> is null.</exception>
        /// <remarks>Do not return an object that you are still using. This will likely lead to the same object being "checked out" twice which will cause bugs. Also if <typeparamref name="T" /> implements <see cref="System.IDisposable" />, do not return an object that has been disposed.</remarks>
        public void Release(T item)
        {
            if (item == null) throw new ArgumentNullException(nameof(item));

            _resetAction?.Invoke(item);

            lock (_arrayLocker)
            {
                if (_size == _array.Length)
                {
                    var objArray = new T[2 * _size];
                    Array.Copy(_array, 0, objArray, 0, _size);
                    _array = objArray;
                }
                _array[_size++] = item;
            }
        }
    }
}