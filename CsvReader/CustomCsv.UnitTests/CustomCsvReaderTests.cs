using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using NUnit.Framework;

namespace CustomCsv.UnitTests
{
    [TestFixture]
    public class CustomCsvReaderTests
    {
        private const int magicNumber = 4;

        [Test]
        public void ReadValues_GoodStreamWithHeadersAndNoOptions_ReturnsListOfValues()
        {
            TextReader stream = GetGoodTextReader(addHeaders: true);
            var csvReader = new CustomCsvReader(stream);

            IEnumerable<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(recordValues, GetRecordValues(0));
        }

        [Test]
        public void ReadValues_GoodStreamAndNoHeaders_ReturnsListOfValues()
        {
            TextReader stream = GetGoodTextReader();
            var csvReader = new CustomCsvReader(stream, new CustomCsvReaderOptions() { AreHeadersInStream = false });

            IEnumerable<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(recordValues, GetRecordValues(0));
        }

        [Test]
        public void ReadRecord_GoodStreamWithHeadersAndNoOptions_ReturnsDictionary()
        {
            TextReader stream = GetGoodTextReader(addHeaders: true);
            var csvReader = new CustomCsvReader(stream);

            IDictionary<string, string> record = csvReader.ReadRecord();

            Assert.AreEqual(record, GetRecord(GetRecordValues(-1), GetRecordValues(0)));
        }

        [Test]
        public void ReadRecord_GoodStreamNoHeadersInFileOrInOptions_ThrowsException()
        {
            var csvReader = new CustomCsvReader(GetGoodTextReader(), new CustomCsvReaderOptions() { AreHeadersInStream = false });
            Assert.Catch(() =>
            {
                csvReader.ReadRecord();
            });
        }

        [Test]
        public void ReadRecord_HeadersAmountNotEqualValueAmount_ThrowsException()
        {
            var csvReader = new CustomCsvReader(GetNotNormalizedTextReader(addHeaders: true));

            Assert.Catch(() =>
            {
                csvReader.ReadRecord();
            });
        }

        [Test]
        public void ReadRecord_ReadingMoreThanExist_ReturnsNull()
        {
            var csvReader = new CustomCsvReader(GetGoodTextReader(recordAmount: 4, addHeaders: true));

            IList<IDictionary<string, string>> records = Enumerable.Range(0, 5).Select(index => csvReader.ReadRecord()).ToList();

            Assert.IsNull(records[4]);
        }

        [Test]
        public void ReaderCreation_StreamIsNull_ThrowsException()
        {
            Assert.Catch(typeof(NullReferenceException),() =>
            {
                new CustomCsvReader(null);
            });
        }


        private string[] GetRecordValues(int recordIndex, int fieldAmount = magicNumber)
        {
            return Enumerable.Range(0, fieldAmount).Select(index => $"{recordIndex}record_{index}field").ToArray();
        }

        private Dictionary<string, string> GetRecord(IList<string> headers, IList<string> values)
        {
            // Open for more elegant solutions
            return headers.Join(values, header => headers.IndexOf(header), value => values.IndexOf(value),
                    (header, value) => new KeyValuePair<string, string>(header, value))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private TextReader GetNotNormalizedTextReader(int recordAmount = magicNumber, int minFieldAmount = magicNumber, int maxFieldAmount = 2 * magicNumber, bool addHeaders = false)
        {
            Random rand = new Random();
            string[][] records = Enumerable.Range(addHeaders ? -1 : 0, recordAmount + (addHeaders ? 1 : 0)).Select(index => GetRecordValues(index, rand.Next(minFieldAmount, maxFieldAmount))).ToArray();

            var abstractCsvFile = string.Join('\n', records.Select(record => string.Join(',', record)));
            return new StringReader(abstractCsvFile);
        }

        private TextReader GetGoodTextReader(int recordAmount = magicNumber, int fieldAmount = magicNumber, bool addHeaders = false)
        {
            string[][] records = Enumerable.Range(addHeaders ? -1 : 0, recordAmount + (addHeaders ? 1 : 0)).Select(index => GetRecordValues(index, fieldAmount)).ToArray();

            var abstractCsvFile = string.Join('\n', records.Select(record => string.Join(',', record)));
            return new StringReader(abstractCsvFile);
        }


    }
}