using Collections;
using NuGet.Frameworks;
using System;

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
    }
}
