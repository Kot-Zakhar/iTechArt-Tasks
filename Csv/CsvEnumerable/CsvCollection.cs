using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using CsvHelper;

namespace CsvEnumerable
{
    public class CsvCollection<T> : IEnumerable<T>
    {
        protected CsvReader reader;
        protected bool leaveReader;
        private bool disposed = false;
        public CsvCollection(TextReader reader, bool leaveReaderOpen = false)
        {
            this.reader = new CsvReader(reader);
            this.leaveReader = leaveReaderOpen;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                if (!leaveReader)
                    reader.Dispose();
                reader = null;
                disposed = true;
            }
        }

        public IEnumerator<T> GetEnumerator()
        {
            return new CsvEnumerator<T>();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

    }

    internal class CsvEnumerator<T>: IEnumerator<T>
    {
        protected CsvCollection<T> currentCollection;
        protected T current;
        private bool a = true;
        private bool disposed = false;

        object IEnumerator.Current
        {
            get => current;
        }

        T IEnumerator<T>.Current
        {
            get => current;
        }

        public void Dispose()
        {
            if (!disposed)
            {

                disposed = true;
            }
        }

        bool IEnumerator.MoveNext()
        {
            bool result = a;
            a = !a;
            return result;
        }

        void IEnumerator.Reset()
        {
            a = true;
        }
    }
}