namespace BashSoft.Tests
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using BashSoft.Contracts;
    using BashSoft.DataStructures;
    using NUnit.Framework;

    [TestFixture]
    public class OrderedDataStructureTester
    {
        private ISimpleOrderedBag<string> names;

        [SetUp]
        public void TestInit()
        {
            this.names = new SimpleSortedList<string>();
        }

        [Test]
        public void CtorWithNoParams()
        {
            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void CtorWithInitialCapacity()
        {
            this.names = new SimpleSortedList<string>(20);

            Assert.AreEqual(this.names.Capacity, 20);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void CtorWithInitialComparer()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase);

            Assert.AreEqual(this.names.Capacity, 16);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void CtorWithAllParams()
        {
            this.names = new SimpleSortedList<string>(StringComparer.OrdinalIgnoreCase, 30);

            Assert.AreEqual(this.names.Capacity, 30);
            Assert.AreEqual(this.names.Size, 0);
        }

        [Test]
        public void AddIncreasesSize()
        {
            this.names.Add("Pesho");

            Assert.AreEqual(1, this.names.Size);
        }

        [Test]
        public void AddNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.Add(null));
        }

        [Test]
        public void AddingUnsortedDataIsHeldSorted()
        {
            this.names.Add("Rosen");
            this.names.Add("Georgi");
            this.names.Add("Balkan");

            CollectionAssert.AreEqual(new[] { "Balkan", "Georgi", "Rosen" }, this.names.Take(3).ToArray());
        }

        [Test]
        public void AddingMoreElementsThanInitialCapacity()
        {
            for (int i = 0; i < 17; i++)
            {
                this.names.Add("test");
            }

            Assert.AreEqual(17, this.names.Size);
            Assert.AreNotEqual(16, this.names.Capacity);
        }

        [Test]
        public void AddingAllFromCollectionIncreasesSize()
        {
            var list = new List<string> { "Pesho", "Gesho" };
            this.names.AddAll(list);

            Assert.AreEqual(2, this.names.Size);
        }

        [Test]
        public void AddingAllFromNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.AddAll(null));
        }

        [Test]
        public void AddAllKeepsCollectionSorted()
        {
            var list = new List<string> { "Pesho", "Gesho", "Besho" };
            this.names.AddAll(list);

            CollectionAssert.AreEqual(new[] { "Besho", "Gesho", "Pesho" }, this.names.Take(3).ToArray());
        }

        [Test]
        public void RemoveValidElementDecreasesSize()
        {
            this.names.Add("Pesho");
            this.names.Remove("Pesho");

            Assert.AreEqual(0, this.names.Size);
        }

        [Test]
        public void RemoveValidElementRemovesSelectedOne()
        {
            this.names.Add("Pesho");
            this.names.Add("Gosho");
            this.names.Remove("Pesho");

            CollectionAssert.AreEqual(new[] { "Gosho" }, this.names.Take(1).ToArray());
        }

        [Test]
        public void RemovingNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.Remove(null));
        }

        [Test]
        public void JoiningWithNullThrowsException()
        {
            Assert.Throws<ArgumentNullException>(() => this.names.JoinWith(null));
        }

        [Test]
        public void JoinWithWorksFine()
        {
            this.names.Add("Pesho");
            this.names.Add("Gosho");
            this.names.Add("Mosho");

            Assert.AreEqual("Gosho,Mosho,Pesho", this.names.JoinWith(","));
        }
    }
}