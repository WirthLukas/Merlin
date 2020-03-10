using System;
using System.Collections;
using System.Collections.Generic;

// TODO: Check Method Comments
namespace Merlin.Utils.Collections
{
    /// <summary>
    /// very basic wrapper around an array that auto-expands it when it reaches capacity. Note that when iterating it should be done
    /// like this accessing the buffer directly but using the FastList.length field:
    /// 
    /// for( var i = 0; i &lt;= list.length; i++ )
    /// 	var item = list.buffer[i];
    /// </summary>
    public class FastList<T> : ICollection<T>
    {
        #region <<Fields>>
        
        private T[] _buffer;
        private int _length = 0;

        #endregion

        #region <<Properties>>
        /// <summary>
        /// access to the length of the filled items in the buffer
        /// </summary>
        public int Count => _length;

        /// <summary>
        /// provided for ease of access though it is recommended to just access the buffer directly.
        /// </summary>
        /// <param name="index">Index.</param>
        public T this[int index] => _buffer[index];

        public bool IsReadOnly { get; } = false;

        #endregion

        public FastList(int size = 5)
        {
            _buffer = new T[size];
        }

        #region <<Methods>>

        /// <summary>
        /// works just like clear except it does not null our all the items in the buffer. Useful when dealing with structs.
        /// </summary>
        public void Reset() => _length = 0;

        /// <summary>
        /// removes the item at the given index from the list
        /// </summary>
        public void RemoveAt(int index)
        { 
            if (index >= _length)
                throw new ArgumentException("Index should not be out of Range!", nameof(index));

            _length--;

            if (index < _length)
                Array.Copy(_buffer, index + 1, _buffer, 
                    index, _length - index);

            _buffer[_length] = default(T);
        }

        /// <summary>
        /// removes the item at the given index from the list but does NOT maintain list order
        /// </summary>
        /// <param name="index">Index.</param>
        public void RemoveAtWithSwap(int index)
        {
            if (index >= _length)
                throw new ArgumentException("Index should not be out of Range!", nameof(index));

            _buffer[index] = _buffer[_length - 1];
            _buffer[_length - 1] = default(T);
            _length--;
        }


        /// <summary>
        /// if the buffer is at its max more space will be allocated to fit additionalItemCount
        /// </summary>
        public void EnsureCapacity(int additionalItemCount = 1)
        {
            if (_length + additionalItemCount >= _buffer.Length)
                Array.Resize(ref _buffer, Math.Max(_buffer.Length << 1, _length + additionalItemCount));
        }


        /// <summary>
        /// adds all items from array
        /// </summary>
        /// <param name="array">Array.</param>
        public void AddRange(IEnumerable<T> array)
        {
            foreach (var item in array)
                Add(item);
        }


        /// <summary>
        /// sorts all items in the buffer up to length
        /// </summary>
        public void Sort()
        {
            Array.Sort(_buffer, 0, _length);
        }


        /// <summary>
        /// sorts all items in the buffer up to length
        /// </summary>
        public void Sort(IComparer comparer)
        {
            Array.Sort(_buffer, 0, _length, comparer);
        }


        /// <summary>
        /// sorts all items in the buffer up to length
        /// </summary>
        public void Sort(IComparer<T> comparer)
        {
            Array.Sort(_buffer, 0, _length, comparer);
        }

        #endregion

        #region <<Interface Methods>>

        /// <summary>
        /// adds the item to the list
        /// </summary>
        public void Add(T item)
        {
            if (_length == _buffer.Length)
                Array.Resize(ref _buffer, Math.Max(_buffer.Length << 1, 10));

            _buffer[_length++] = item;
        }

        /// <summary>
        /// clears the list and nulls out all items in the buffer
        /// </summary>
        public void Clear()
        {
            Array.Clear(_buffer, 0, _length);
            _length = 0;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="array"></param>
        /// <param name="arrayIndex"></param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="ArgumentOutOfRangeException"></exception>
        /// <exception cref="ArgumentException"></exception>
        public void CopyTo(T[] array, int arrayIndex)
        {
            if (array == null)
                throw new ArgumentNullException(nameof(array));

            if (arrayIndex < 0)
                throw new ArgumentOutOfRangeException(nameof(arrayIndex), "The starting array index cannot be negative.");

            if (_length > array.Length - arrayIndex + 1)
                throw new ArgumentException("The destination array has fewer elements than the collection.");

            Array.Copy(_buffer, arrayIndex, array,
                0, _length - arrayIndex);
        }

        /// <summary>
        /// checks to see if item is in the FastList
        /// </summary>
        /// <param name="item">Item.</param>
        public bool Contains(T item)
        {
            var comp = EqualityComparer<T>.Default;

            for (var i = 0; i < _length; ++i)
            {
                if (comp.Equals(_buffer[i], item))
                    return true;
            }

            return false;
        }

        /// <summary>
        /// removes the item from the list
        /// </summary>
        /// <param name="item">Item.</param>
        public bool Remove(T item)
        {
            var comp = EqualityComparer<T>.Default;

            for (var i = 0; i < _length; ++i)
            {
                if (comp.Equals(_buffer[i], item))
                {
                    RemoveAt(i);
                    return true;
                }
            }

            return false;
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new FastListEnumerator(this);
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        #endregion

        #region <<Sub Types>>

        private class FastListEnumerator :IEnumerator<T>
        {
            private readonly FastList<T> _list;
            private int _currentIndex = -1;

            public T Current => _list[_currentIndex];

            object IEnumerator.Current => Current;

            public FastListEnumerator(FastList<T> list)
            {
                _list = list;
            }

            public bool MoveNext()
            {
                // Avoid returning a null value
                do
                {
                    //Avoids going beyond the end of the collection.
                    if (++_currentIndex >= _list.Count)
                        return false;
                } while (_list[_currentIndex] == null);

                return true;
            }

            public void Reset() => _currentIndex = -1;

            public void Dispose() { }
        }

        #endregion
    }
}
