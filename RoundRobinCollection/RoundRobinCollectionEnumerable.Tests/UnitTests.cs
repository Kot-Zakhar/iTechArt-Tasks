using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoundRobinCollection;

namespace RoundRobinCollectionEnumerable.Tests
{
    public class UnitTests
    {
        [Test]
        public void CollectionCreation_NullSourceCollection_ThrowsArgumentNullException()
        {
            Assert.Catch<ArgumentNullException>(() =>
            {
                new RoundRobinCollectionEnumerable<int>(null);
            });
        }

        [Test]
        public void CollectionCreation_EmptySourceCollection_ThrowsNothing()
        {
            Assert.DoesNotThrow(() =>
            {
                new RoundRobinCollectionEnumerable<int>(new List<int>());
            });
        }

        [Test]
        public void SingleIterator_SingleIteration_IteratesWell()
        {
            int amount = 10;

            IList <int> source = Enumerable.Range(0, amount).ToList();

            IEnumerator<int> iter = new RoundRobinCollectionEnumerable<int>(source);

            var result = Enumerable.Range(0, amount)
                .Select(i =>
                {
                    iter.MoveNext();
                    return iter.Current;
                }).ToList();

            Assert.AreEqual(source, result);
        }
    }
}