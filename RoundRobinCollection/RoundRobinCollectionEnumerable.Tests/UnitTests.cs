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
        private const int sourceLength = 10;
        private IList<int> _source;
        private IEnumerable<int> _enumerable;

        [SetUp]
        public void InitSource()
        {
            _source = Enumerable.Range(0, sourceLength).ToList();
            _enumerable = new RoundRobinCollectionEnumerable<int>(_source);
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
            IEnumerator<int> iter = new RoundRobinCollectionEnumerable<int>(_source);

            var result = Enumerable.Range(0, sourceLength)
                .Select(i =>
                {
                    iter.MoveNext();
                    return iter.Current;
                }).ToList();

            Assert.AreEqual(_source, result);
        }

        [Test]
        public void IterationThroughCollection_SingleIteratorAndSeveralIterations_IteratesEndlessly()
        {
            int iterationAmount = 3;

            List<int> expected = new List<int>();
            for (int i = 0; i < iterationAmount; i++)
                expected.AddRange(_source);
;
            var iter = _enumerable.GetEnumerator();

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

            IList<IEnumerator<int>> iterators = Enumerable.Range(0, iteratorsAmount)
                .Select(i => _enumerable.GetEnumerator()).ToList();

            List<int> expected = new List<int>();
            for (int i = 0; i < iteratorsAmount; i++)
                expected.AddRange(_source);

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