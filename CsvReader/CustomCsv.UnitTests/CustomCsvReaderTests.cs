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
            TextReader stream = GenerateGoodTextReader(addHeaders: true);
            var csvReader = new CustomCsvReader(stream);

            IList<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(GenerateRecordValues(0), recordValues);
        }

        [Test]
        public void ReadValues_GoodStreamAndNoHeaders_ReturnsListOfValues()
        {
            TextReader stream = GenerateGoodTextReader();
            var csvReader = new CustomCsvReader(stream, new CustomCsvReaderOptions() { ReadHeaders = false });

            IList<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(GenerateRecordValues(0), recordValues);
        }

        [Test]
        public void ReadRecord_GoodStreamWithHeadersAndNoOptions_ReturnsDictionary()
        {
            TextReader stream = GenerateGoodTextReader(addHeaders: true);
            var csvReader = new CustomCsvReader(stream);

            IDictionary<string, string> record = csvReader.ReadRecord();

            Assert.AreEqual(GenerateRecord(GenerateRecordValues(-1), GenerateRecordValues(0)), record);
        }

        [Test]
        public void ReadRecord_GoodStreamNoHeadersInFileOrInOptions_ThrowsException()
        {
            var csvReader = new CustomCsvReader(GenerateGoodTextReader(), new CustomCsvReaderOptions() { ReadHeaders = false });
            Assert.Catch(() =>
            {
                csvReader.ReadRecord();
            });
        }

        [Test]
        public void ReadRecord_HeadersAmountNotEqualValueAmount_ThrowsException()
        {
            var csvReader = new CustomCsvReader(GenerateNotNormalizedTextReader(addHeaders: true));

            Assert.Catch(() =>
            {
                var results = Enumerable.Range(0, magicNumber).Select(i => csvReader.ReadRecord()).ToList();
            });
        }

        [Test]
        public void ReadRecord_ProvidingOwnHeadersInsteadHeadersInFile_ReturnsDictionary()
        {
            var csvReader = new CustomCsvReader(
                GenerateGoodTextReader(addHeaders: true),
                new CustomCsvReaderOptions()
                {
                    ReadHeaders = true,
                    Headers = GenerateRecordValues(-10)
                });

            var result = Enumerable.Range(0, magicNumber).Select(i => csvReader.ReadRecord()).ToList();

            Assert.AreEqual(
                Enumerable.Range(0, magicNumber).Select(i => GenerateRecord(GenerateRecordValues(-10), GenerateRecordValues(i))).ToList(),
                result
            );

        }

        [Test]
        public void ReadRecord_AmountOfProvidedHeadersNotSameAsAmountOfHeadersInFile_ThrowsExceptionAtCreation()
        {
            Assert.Catch(() =>
            {
                var csvReader = new CustomCsvReader(
                    GenerateGoodTextReader(addHeaders: true),
                    new CustomCsvReaderOptions()
                    {
                        ReadHeaders = true,
                        Headers = GenerateRecordValues(-10, magicNumber * 2)
                    });
            });
        }

        [Test]
        public void ReadRecord_ProvidingOwnHeadersWithNoHeadersInFile_ReturnsDictionary()
        {
            var csvReader = new CustomCsvReader(
                GenerateGoodTextReader(addHeaders: false, recordAmount: 10),
                new CustomCsvReaderOptions()
                {
                    ReadHeaders = false,
                    Headers = GenerateRecordValues(-10)
                });

            var result = Enumerable.Range(0, 10).Select(i => csvReader.ReadRecord()).ToList();

            Assert.AreEqual(
                Enumerable.Range(0, 10).Select(i => GenerateRecord(GenerateRecordValues(-10), GenerateRecordValues(i))).ToList(),
                result
            );
        }

        [Test]
        public void ReadRecord_ReadingMoreThanExist_ReturnsNull()
        {
            var csvReader = new CustomCsvReader(GenerateGoodTextReader(recordAmount: 4, addHeaders: true));

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


        private string[] GenerateRecordValues(int recordIndex, int fieldAmount = magicNumber)
        {
            return Enumerable.Range(0, fieldAmount).Select(index => $"{recordIndex}record_{index}field").ToArray();
        }

        private Dictionary<string, string> GenerateRecord(IList<string> headers, IList<string> values)
        {
            // Open for more elegant solutions
            return headers.Join(values, header => headers.IndexOf(header), value => values.IndexOf(value),
                    (header, value) => new KeyValuePair<string, string>(header, value))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        private TextReader GenerateNotNormalizedTextReader(int recordAmount = magicNumber, int headerAmount = magicNumber, int minFieldAmount = magicNumber / 2, int maxFieldAmount = 2 * magicNumber, bool addHeaders = false)
        {
            Random rand = new Random();

            int RandomFieldAmount()
            {
                int result;
                do
                {
                    result = rand.Next(minFieldAmount, maxFieldAmount);
                } while (result == headerAmount);

                return result;
            }

            string[][] records = Enumerable.Range(
                addHeaders ? -1 : 0,
                recordAmount + (addHeaders ? 1 : 0)
            ).Select(index => GenerateRecordValues(index, index >= 0 ? RandomFieldAmount() : headerAmount))
             .ToArray();

            var abstractCsvFile = string.Join('\n', records.Select(record => string.Join(',', record)));
            return new StringReader(abstractCsvFile);
        }

        private TextReader GenerateGoodTextReader(int recordAmount = magicNumber, int fieldAmount = magicNumber, bool addHeaders = false)
        {
            string[][] records = Enumerable.Range(addHeaders ? -1 : 0, recordAmount + (addHeaders ? 1 : 0)).Select(index => GenerateRecordValues(index, fieldAmount)).ToArray();

            var abstractCsvFile = string.Join('\n', records.Select(record => string.Join(',', record)));
            return new StringReader(abstractCsvFile);
        }


    }
}