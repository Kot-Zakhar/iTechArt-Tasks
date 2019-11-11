using System;
using System.IO;
using System.Collections;
using System.Collections.Generic;
using CsvHelper;

namespace CsvEnumerable
{
    public class CsvCollection<T> : IEnumerable<T>
    {
        public string csvPath { get; private set; }
        private bool disposed = false;
        public CsvCollection(string path)
        {
            csvPath = path;
        }

        public void Dispose()
        {
            if (!disposed)
            {
                csvPath = null;
                disposed = true;
            }
        }

        public IEnumerator<T> GetEnumerator() => new CsvEnumerator<T>(csvPath);

        IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();

    }

    internal class CsvEnumerator<T>: IEnumerator<T>
    {
        protected CsvReader csvReader = null;
        protected string csvPath;
        private bool headerIsValid = true;
        private bool disposed = false;

        public CsvEnumerator(string path)
        {
            csvPath = path;
            Reset();
        }
        
        object IEnumerator.Current
        {
            get => csvReader.GetRecord<T>();
        }

        T IEnumerator<T>.Current
        {
            get => csvReader.GetRecord<T>();
        }

        public void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                csvReader?.Dispose();
                csvReader = null;
            }
        }

        public bool MoveNext() => headerIsValid && csvReader.Read();

        public void Reset()
        {
            csvReader?.Dispose();
            csvReader = new CsvReader(
                new StreamReader(
                    new FileStream(
                        csvPath, 
                        FileMode.Open, 
                        FileAccess.Read, 
                        FileShare.ReadWrite), 
                    System.Text.Encoding.Unicode)
            );

            try
            {
                if (csvReader.Read())
                {
                    csvReader.ReadHeader();
                    csvReader.ValidateHeader<T>();
                }
                else
                    headerIsValid = false;
            }
            catch (ValidationException)
            {
                headerIsValid = false;
            }
        }
    }
}