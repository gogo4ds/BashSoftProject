namespace BashSoft.DataStructures
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Text;
    using BashSoft.Contracts;

    public class SimpleSortedList<T> : ISimpleOrderedBag<T> where T : IComparable<T>
    {
        private const int DefaultSize = 16;
        private static readonly IComparer<T> DefaultComparer = Comparer<T>.Create((x, y) => x.CompareTo(y));
        private readonly IComparer<T> comparison;

        private T[] innerCollection;

        public SimpleSortedList(IComparer<T> comparer, int capacity)
        {
            this.InitializeInnerCollection(capacity);
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

        public int Size { get; private set; }

        public void Add(T element)
        {
            if (this.innerCollection.Length == this.Size)
            {
                this.Resize();
            }

            this.innerCollection[this.Size] = element;
            this.Size++;
            Array.Sort(this.innerCollection, 0, this.Size, this.comparison);
        }

        public void AddAll(ICollection<T> collection)
        {
            if (this.Size + collection.Count >= this.innerCollection.Length)
            {
                this.MultiResize(collection);
            }

            foreach (var item in collection)
            {
                this.innerCollection[this.Size] = item;
                this.Size++;
            }

            Array.Sort(this.innerCollection, 0, this.Size, this.comparison);
        }

        public IEnumerator<T> GetEnumerator()
        {
            foreach (var item in this.innerCollection)
            {
                yield return item;
            }
        }

        public string JoinWith(string joiner)
        {
            var builder = new StringBuilder();

            foreach (var element in this)
            {
                builder.Append(element);
                builder.Append(joiner);
            }

            builder.Remove(builder.Length - 1, 1);
            return builder.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this.GetEnumerator();
        }

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
            var newCollection = new T[this.Size * 2];
            Array.Copy(this.innerCollection, newCollection, this.Size);
            this.innerCollection = newCollection;
        }

        private void MultiResize(ICollection<T> collection)
        {
            var newSize = this.innerCollection.Length * 2;
            while (this.Size + collection.Count >= newSize)
            {
                newSize *= 2;
            }

            var newCollection = new T[newSize];
            Array.Copy(this.innerCollection, newCollection, this.Size);
            this.innerCollection = newCollection;
        }
    }
}