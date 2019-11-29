using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using RoundRobinCollection;

namespace RoundRobinCollectionEnumerable.Tests
{
    public class RoundRobinCollectionEnumerableTests
    {
        private const int sourceLength = 10;

        [SetUp]
        public void InitSource()
        {
        }

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
        public void IterationCollection_SingleIteratorAndSingleIteration_IteratesWell()
        {
            var source = Enumerable.Range(0, sourceLength).ToList();
            IEnumerator<int> iter = new RoundRobinCollectionEnumerable<int>(source);

            var result = Enumerable.Range(0, sourceLength)
                .Select(i =>
                {
                    iter.MoveNext();
                    return iter.Current;
                }).ToList();

            Assert.AreEqual(source, result);
        }

        [Test]
        public void IterationThroughCollection_SingleIteratorAndSeveralIterations_IteratesEndlessly()
        {
            int iterationAmount = 3;

            var source = Enumerable.Range(0, sourceLength).ToList();
            var enumerable = new RoundRobinCollectionEnumerable<int>(source);

            List<int> expected = new List<int>();
            for (int i = 0; i < iterationAmount; i++)
                expected.AddRange(source);
;
            var iter = enumerable.GetEnumerator();

            var actual = Enumerable.Range(0, sourceLength * iterationAmount)
                .Select(i =>
                {
                    iter.MoveNext();
                    return iter.Current;
                }).ToList();

            Assert.AreEqual(expected, actual);
        }

        [Test]
        public void IterationThroughCollection_SeveralIterators_IteratesEndlessly()
        {
            int iteratorsAmount = 3;

            var source = Enumerable.Range(0, sourceLength).ToList();
            var enumerable = new RoundRobinCollectionEnumerable<int>(source);

            IList<IEnumerator<int>> iterators = Enumerable.Range(0, iteratorsAmount)
                .Select(i => enumerable.GetEnumerator()).ToList();

            List<int> expected = new List<int>();
            for (int i = 0; i < iteratorsAmount; i++)
                expected.AddRange(source);

            var actual = new List<int>();

            for (int i = 0; i < sourceLength; i++)
            {
                foreach(var iter in iterators)
                {
                    iter.MoveNext();
                    actual.Add(iter.Current);
                }
            }

            Assert.AreEqual(expected, actual);
        }
    }
}