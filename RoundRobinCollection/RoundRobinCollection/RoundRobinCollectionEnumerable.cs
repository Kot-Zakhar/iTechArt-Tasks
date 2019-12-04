using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace RoundRobinCollection
{
    public class RoundRobinCollectionEnumerable<T> : IEnumerable<T>, IEnumerator<T>
    {
        private int currentIndex = -1;
        private readonly IList<T> _source;

        public T Current { get => _source[currentIndex]; }
        object IEnumerator.Current => _source[currentIndex];

        public RoundRobinCollectionEnumerable(IList<T> source)
        {
            _source = source ?? throw new ArgumentNullException("Source collection can't be null.");
        }

        public IEnumerator<T> GetEnumerator()
        {
            return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return this;
        }

        public bool MoveNext()
        {
            if (!_source.Any())
                return false;
            currentIndex = ++currentIndex % _source.Count();
            return true;
        }

        public void Reset()
        {
            currentIndex = -1;
        }

        public void Dispose() { }
    }
}
