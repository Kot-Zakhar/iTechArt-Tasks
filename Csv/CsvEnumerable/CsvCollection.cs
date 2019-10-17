using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using CsvHelper;

namespace CsvEnumerable
{
    public class CsvCollection<T> : IEnumerable<T>
    {
        //public CsvReader reader { get; private set; }
        public Stream fileStream { get; private set; }
        private bool disposed = false;
        public CsvCollection(string path)
        {
            this.fileStream = new FileStream(Path.GetFullPath(path), FileMode.Open, FileAccess.Read, FileShare.Read);
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
            fileStream.Seek(0, SeekOrigin.Begin);
            return new CsvEnumerator()
            //reader.
            //return new CsvEnumerator<T>(new CsvReader(reader, true));
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
        private bool headerIsValid = true;
        private bool disposed = false;

        public CsvEnumerator(CsvCollection<T> collection)
        {
            currentCollection = collection;
        }
        
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
            if (!headerIsValid)
                return false;

            
        }

        void IEnumerator.Reset()
        {

            try
            {
            }
            catch (ValidationException)
            {
                headerIsValid = false;
            }
        }
    }
}