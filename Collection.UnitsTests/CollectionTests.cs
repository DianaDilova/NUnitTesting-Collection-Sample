using Collections;
using NuGet.Frameworks;
using System;
using System.Reflection;

namespace Collection.UnitsTests
{
    public class CollectionTests
    {

        [Test]
        public void Test_Collection_EmptyConstructor()
        {
            var coll = new Collection<int>();

            Assert.AreEqual(coll.ToString(), "[]");

        }

        [Test]
        public void Test_Collection_ConstructorSingleItem()
        {
            var coll = new Collection<int>(5);
            var actual = coll.ToString();
            var expected = "[5]";

            Assert.AreEqual(coll.ToString(), "[5]");
        }

        [Test]
        public void Test_Collection_ConstructorMultipleItems()
        {
            var coll = new Collection<int>(5, 6);
            var actual = coll.ToString();
            var expected = "[5, 6]";

            Assert.AreEqual(coll.ToString(), "[5, 6]");

        }

        [Test]
        public void Test_Collection_CountAndCapacity()
        {
            var coll = new Collection<int>(5, 6);

            Assert.AreEqual(coll.Count, 2, "Check for count");
            Assert.That(coll.Capacity, Is.GreaterThan(coll.Count));

        }

        [Test]
        public void Test_Collection_Add()
        {
            var coll = new Collection<string>("Ivan", "Peter");

            coll.Add("Gosho");

            Assert.AreEqual(coll.ToString(), "[Ivan, Peter, Gosho]");
        }

        [Test]
        public void Test_Collection_GetByIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            var item = collection[1];

            Assert.That(item.ToString(), Is.EqualTo("6"));
        }

        [Test]
        public void Test_Collection_SetByIndex()
        {
            var collection = new Collection<int>(5, 6, 7);
            collection[1] = 666;

            Assert.That(collection.ToString(), Is.EqualTo("[5, 666, 7]"));
        }

        [Test]
        public void Test_Collection_GetByInvalidIndex()
        {
            var coll = new Collection<string>("Ivan", "Peter");

            Assert.That(() => { var item = coll[2]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = coll[-1]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(() => { var name = coll[500]; }, Throws.InstanceOf<ArgumentOutOfRangeException>());
            Assert.That(coll.ToString(), Is.EqualTo("[Ivan, Peter]"));
        }

        [Test]
        public void Test_Collection_AddWithGrow()
        {
            var coll = new Collection<int>();
            int initialCapacity = coll.Capacity;

            for (int i = 0; i < 15; i++)
            {
                coll.Add(i);
            }

            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(initialCapacity));
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(coll.Count));
        }

        [Test]
        public void Test_Collection_AddRange()
        {
            var coll = new Collection<int>(23, 24, 25);
            coll.AddRange(26, 27, 28);

            Assert.AreEqual(coll.ToString(), ("[23, 24, 25, 26, 27, 28]"));
        }

        [Test]
        public void Test_Collection_SetByInvalidIndex()
        {
            var coll = new Collection<int>(5, 6);

            Assert.Throws<ArgumentOutOfRangeException>(() => coll[2] = 7);
        }

        [Test]
        public void Test_Collection_AddRangeWithGrow()
        {
            var coll = new Collection<int>();
            int oldCapacity = coll.Capacity;
            var newColl = Enumerable.Range(1000, 2000).ToArray();

            coll.AddRange(newColl);

            string expectedNums = "[" + string.Join(", ", newColl) + "]";
            Assert.That(coll.ToString(), Is.EqualTo(expectedNums));
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(oldCapacity));
            Assert.That(coll.Capacity, Is.GreaterThanOrEqualTo(coll.Count));
        }

        [Test]
        public void Test_Collection_ToStringNestedCollections()
        {
            var coll = new Collection<string>("Ivan", "Peter");
            var nums = new Collection<int>(10, 20);
            var dates = new Collection<DateTime>();

            var nested = new Collection<object>(coll, nums, dates);
            string nestedToString = nested.ToString();

            Assert.That(nestedToString, Is.EqualTo("[[Ivan, Peter], [10, 20], []]"));
        }

        [Test]
        public void Test_Collection_1MillionItems()
        {
            const int itemsCount = 1000000;
            var coll = new Collection<int>();
            coll.AddRange(Enumerable.Range(1, itemsCount).ToArray());

            Assert.That(coll.Count == itemsCount);
            Assert.That(coll.Capacity >= coll.Count);
            for (int i = itemsCount - 1; i >= 0; i--)
                coll.RemoveAt(i);

            Assert.That(coll.ToString() == "[]");
            Assert.That(coll.Capacity >= coll.Count);
        }

        [Test]
        public void Test_Collection_InsertAtStart()
        {
            var coll = new Collection<int>(5, 7);
            coll.InsertAt(0, 3);

            Assert.That(coll.ToString(), Is.EqualTo("[3, 5, 7]"));
        }

        [Test]
        public void Test_Collection_InsertAtEnd()
        {
            var coll = new Collection<int>(5, 7);
            coll.InsertAt(2, 3);

            Assert.That(coll.ToString(), Is.EqualTo("[5, 7, 3]"));
        }

        [Test]
        public void Test_Collection_InsertAtMiddle()
        {
            var coll = new Collection<int>(5, 7);
            coll.InsertAt(1, 3);

            Assert.That(coll.ToString(), Is.EqualTo("[5, 3, 7]"));
        }

        [Test]
        public void Test_Collection_InsertAtWithGrow()
        {
            var coll = new Collection<int>(5, 7, 9, 11);
            coll.InsertAt(4, 13);

            Assert.AreEqual(coll.Count, 5);
            Assert.That(string.Join(", ", coll), Is.EqualTo("[5, 7, 9, 11, 13]"));
        }

        [Test]
        public void Test_Collection_InsertAtInvalidIndex()
        {
            var coll = new Collection<int>(5, 7, 9);

            Assert.That(() => { coll.InsertAt(4, 11); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_ExchangeMiddle()
        {
            var coll = new Collection<int>(3, 5, 7);
            coll.Exchange(1, 2);

            Assert.That(coll.ToString(), Is.EqualTo("[3, 7, 5]"));
        }

        [Test]
        public void Test_Collection_ExchangeFirstLast()
        {
            var coll = new Collection<int>(3, 5, 7);
            coll.Exchange(0, 2);

            Assert.That(coll.ToString(), Is.EqualTo("[7, 5, 3]"));
        }

        [Test]
        public void Test_Collection_ExchangeInvalidIndexes()
        {
            var coll = new Collection<int>(3, 5, 7);

            Assert.That(() => { coll.Exchange(4, 5); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAtStart()
        {
            var coll = new Collection<int>(3, 5, 7);
            coll.RemoveAt(0);

            Assert.That(coll.ToString(), Is.EqualTo("[5, 7]"));
        }

        [Test]
        public void Test_Collection_RemoveAtEnd()
        {
            var coll = new Collection<int>(3, 5, 7);
            coll.RemoveAt(2);

            Assert.That(coll.ToString(), Is.EqualTo("[3, 5]"));
        }

        [Test]
        public void Test_Collection_RemoveAtMiddle()
        {
            var coll = new Collection<int>(3, 5, 7);
            coll.RemoveAt(1);

            Assert.That(coll.ToString(), Is.EqualTo("[3, 7]"));
        }

        [Test]
        public void Test_Collection_RemovaAtInvalidIndex()
        {
            var coll = new Collection<int>(3, 5, 7);

            Assert.That(() => { coll.RemoveAt(3); }, Throws.InstanceOf<ArgumentOutOfRangeException>());
        }

        [Test]
        public void Test_Collection_RemoveAll()
        {
            var coll = new Collection<int>(3, 5, 7, 9);
            for (int i = 0; i < 4; i++)
            {
                coll.RemoveAt(0);
            }

            Assert.That(coll.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_Clear()
        {
            var coll = new Collection<int>(3, 5, 7);
            coll.Clear();

            Assert.That(coll.Count, Is.EqualTo(0));
        }

        [Test]
        public void Test_Collection_ToStringEmpty()
        {
            var coll = new Collection<string>();

            Assert.That(coll.ToString(), Is.EqualTo("[]"));
        }

        [Test]
        public void Test_Collection_ToStringSingle()
        {
            var coll = new Collection<string>("Peter");

            Assert.That(coll.ToString(), Is.EqualTo("[Peter]"));
        }

        [Test]
        public void Tesst_Collection_ToStringMultiple()
        {
            var coll = new Collection<string>("Peter, Ivan");

            Assert.That(coll.ToString(), Is.EqualTo("[Peter, Ivan]"));

        }

        [TestCase("Peter,Maria,Ivan", 0, "Peter")]
        [TestCase("Peter,Maria,Ivan", 1, "Maria")]
        [TestCase("Peter,Maria,Ivan", 2, "Ivan")]
        [TestCase("Peter", 0, "Peter")]

        public void Test_Collection_GetByValidIndexDDT(string data, int index, string expected)
        {
            var coll = new Collection<string>(data.Split(","));
            var actual = coll[index];

            Assert.That(actual, Is.EqualTo(expected));
        }

        [TestCase("Ivan, Peter", 2)]
        [TestCase("Ivan, Peter", -1)]
        [TestCase("Ivan, Petar", 500)]
        [TestCase("", 0)]
        public void Test_Collection_GetInvalidByValidIndexDDT(string data, int index)
        {
            var coll = new Collection<string>(data.Split(",",
               StringSplitOptions.RemoveEmptyEntries));

            Assert.That(() => coll[index],
                Throws.InstanceOf<ArgumentOutOfRangeException>());

        }

        [TestCase("5,7", 0, 3, "[3, 5, 7]")]
        [TestCase("5,7", 1, 3, "[5, 3, 7]")]
        [TestCase("5,7", 2, 3, "[5, 7, 3]")]
        public void Test_CollectionInsertAtDDT(string data, int index, int value, string expected)
        {
            var coll = new Collection<int>(data.Split(",").Select(x => int.Parse(x)).ToArray());
            coll.InsertAt(index, value);

            Assert.That(coll.ToString(), Is.EqualTo(expected));
        }

        [TestCase("3,5,7", 0, "[5, 7]")]
        [TestCase("3,5,7", 1, "[3, 7]")]
        [TestCase("3,5,7", 2, "[3, 5]")]
        public void Test_CollectionRemoveDDT(string data, int index, string expected)
        {
            var coll = new Collection<string>(data.Split(","));
            coll.RemoveAt(index);

            Assert.That(coll.ToString(), Is.EqualTo(expected));
        }
    }
}
