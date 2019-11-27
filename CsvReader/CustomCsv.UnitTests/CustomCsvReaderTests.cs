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
            TextReader stream = GetGoodTextReader_WithHeaders();
            var csvReader = new CustomCsvReader(stream);

            IEnumerable<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(recordValues, GetRecordValues(0));
        }

        [Test]
        public void ReadValues_GoodStreamAndNoHeaders_ReturnsListOfValues()
        {
            TextReader stream = GetGoodTextReader_NoHeaders();
            var csvReader = new CustomCsvReader(stream, new CustomCsvReaderOptions() { AreHeadersInFile = false, AreHeadersRequired = false });

            IEnumerable<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(recordValues, GetRecordValues(0));
        }

        [Test]
        public void ReadRecord_GoodStreamWithHeadersAndNoOptions_ReturnsDictionary()
        {
            TextReader stream = GetGoodTextReader_WithHeaders();
            var csvReader = new CustomCsvReader(stream);

            IDictionary<string, string> record = csvReader.ReadRecord();

            Assert.AreEqual(record, GetRecord(GetRecordValues(-1), GetRecordValues(0)));
        }

        [Test]
        public void ReaderCreation_GoodStreamNoHeaders_ThrowsException()
        {
            Assert.Catch(() =>
            {
                TextReader stream = GetGoodTextReader_NoHeaders();
                var csvReader = new CustomCsvReader(stream, new CustomCsvReaderOptions() { AreHeadersInFile = false });
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

        private TextReader GetGoodTextReader_NoHeaders(int recordAmount = magicNumber, int fieldAmount = magicNumber)
        {
            string[][] records = Enumerable.Range(0, recordAmount).Select(index => GetRecordValues(index, fieldAmount)).ToArray();

            var abstractCsvFile = string.Join('\n', records.Select(record => string.Join(',', record)));
            return new StringReader(abstractCsvFile);
        }

        private TextReader GetGoodTextReader_WithHeaders(int recordAmount = magicNumber, int fieldAmount = magicNumber)
        {
            string[][] records = Enumerable.Range(-1, recordAmount + 1).Select(index => GetRecordValues(index, fieldAmount))
                .ToArray();

            var abstractCsvFile = string.Join('\n', records.Select(record => string.Join(',', record)));
            return new StringReader(abstractCsvFile);
        }


    }
}