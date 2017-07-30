using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using BashSoft.Contracts;

namespace BashSoft.DataStructures
{
    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private const int DefaultSize = 16;
        private static readonly IComparer<T> DefaultComparer = Comparer<T>.Create((x, y) => x.CompareTo(y));

        private T[] innerCollection;
        private int size;
        private IComparer<T> comparison;

        public SimpleSortedList(IComparer<T> comparer, int capacity)
        {
            InitializeInnerCollection(capacity);
            this.comparison = comparer;
        }

        public SimpleSortedList(int capacity)
            : this(DefaultComparer, capacity)
        {
        }

        public SimpleSortedList(IComparer<T> comparer)
            : this(comparer, DefaultSize)
        {
        }

        public SimpleSortedList()
            : this(DefaultComparer, DefaultSize)
        {
        }

        public int Size => this.size;

        private void InitializeInnerCollection(int capacity)
        {
            if (capacity < 0)
            {
                throw new ArgumentException("Capacity cannot be negative!");
            }

            this.innerCollection = new T[capacity];
        }

        private void Resize()
        {
            T[] newCollection = new T[this.Size * 2];
            Array.Copy(innerCollection, newCollection, Size);
            innerCollection = newCollection;
        }

        private void MultiResize(ICollection<T> collection)
        {
            int newSize = innerCollection.Length * 2;
            while (Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            T[] newCollection = new T[newSize];
            Array.Copy(innerCollection, newCollection, this.size);
            innerCollection = newCollection;
        }

        public void Add(T element)
        {
            if (this.innerCollection.Length == this.Size)
            {
                Resize();
            }

            this.innerCollection[size] = element;
            this.size++;
            Array.Sort(this.innerCollection, 0, this.size, this.comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if (Size + collection.Count >= this.innerCollection.Length)
            {
                MultiResize(collection);
            }

            foreach (var item in collection)
            {
                this.innerCollection[Size] = item;
                this.size++;
            }

            Array.Sort(innerCollection, 0, size, comparison);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in innerCollection)
            {
                yield return item;
            }
        }

        public string JoinWith(string joiner)
        {
            StringBuilder builder = new StringBuilder();

            foreach (var element in this)
            {
                builder.Append(element);
                builder.Append(joiner);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
    }
}