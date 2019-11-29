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

        [Test]
        public void ReadValues_GoodStreamWithHeadersAndNoOptions_ReturnsListOfValues()
        {
            TextReader stream = FakeCsvGenerator.GenerateGoodTextReader(addHeaders: true);
            var csvReader = new CustomCsvReader(stream);

            IList<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(FakeCsvGenerator.GenerateRecordValues(0), recordValues);
        }

        [Test]
        public void ReadValues_GoodStreamAndNoHeaders_ReturnsListOfValues()
        {
            TextReader stream = FakeCsvGenerator.GenerateGoodTextReader();
            var csvReader = new CustomCsvReader(stream, new CustomCsvReaderOptions() { ReadHeaders = false });

            IList<string> recordValues = csvReader.ReadValues();

            Assert.AreEqual(FakeCsvGenerator.GenerateRecordValues(0), recordValues);
        }

        [Test]
        public void ReadRecord_GoodStreamWithHeadersAndNoOptions_ReturnsDictionary()
        {
            TextReader stream = FakeCsvGenerator.GenerateGoodTextReader(addHeaders: true);
            var csvReader = new CustomCsvReader(stream);

            IDictionary<string, string> record = csvReader.ReadRecord();

            Assert.AreEqual(FakeCsvGenerator.GenerateRecord(FakeCsvGenerator.GenerateRecordValues(-1), FakeCsvGenerator.GenerateRecordValues(0)), record);
        }

        [Test]
        public void ReadRecord_GoodStreamNoHeadersInFileOrInOptions_ThrowsException()
        {
            var csvReader = new CustomCsvReader(FakeCsvGenerator.GenerateGoodTextReader(), new CustomCsvReaderOptions() { ReadHeaders = false });
            Assert.Catch(() =>
            {
                csvReader.ReadRecord();
            });
        }

        [Test]
        public void ReadRecord_HeadersAmountNotEqualValueAmount_ThrowsException()
        {
            var csvReader = new CustomCsvReader(FakeCsvGenerator.GenerateNotNormalizedTextReader(addHeaders: true));

            Assert.Catch(() =>
            {
                var results = Enumerable.Range(0, FakeCsvGenerator.magicNumber).Select(i => csvReader.ReadRecord()).ToList();
            });
        }

        [Test]
        public void ReadRecord_ProvidingOwnHeadersInsteadHeadersInFile_ReturnsDictionary()
        {
            var csvReader = new CustomCsvReader(
                FakeCsvGenerator.GenerateGoodTextReader(addHeaders: true),
                new CustomCsvReaderOptions()
                {
                    ReadHeaders = true,
                    Headers = FakeCsvGenerator.GenerateRecordValues(-10)
                });

            var result = Enumerable.Range(0, FakeCsvGenerator.magicNumber).Select(i => csvReader.ReadRecord()).ToList();

            Assert.AreEqual(
                Enumerable.Range(0, FakeCsvGenerator.magicNumber).Select(i => FakeCsvGenerator.GenerateRecord(FakeCsvGenerator.GenerateRecordValues(-10), FakeCsvGenerator.GenerateRecordValues(i))).ToList(),
                result
            );

        }

        [Test]
        public void ReadRecord_AmountOfProvidedHeadersNotSameAsAmountOfHeadersInFile_ThrowsExceptionAtCreation()
        {
            Assert.Catch(() =>
            {
                var csvReader = new CustomCsvReader(
                    FakeCsvGenerator.GenerateGoodTextReader(addHeaders: true),
                    new CustomCsvReaderOptions()
                    {
                        ReadHeaders = true,
                        Headers = FakeCsvGenerator.GenerateRecordValues(-10, FakeCsvGenerator.magicNumber * 2)
                    });
            });
        }

        [Test]
        public void ReadRecord_ProvidingOwnHeadersWithNoHeadersInFile_ReturnsDictionary()
        {
            var csvReader = new CustomCsvReader(
                FakeCsvGenerator.GenerateGoodTextReader(addHeaders: false, recordAmount: 10),
                new CustomCsvReaderOptions()
                {
                    ReadHeaders = false,
                    Headers = FakeCsvGenerator.GenerateRecordValues(-10)
                });

            var result = Enumerable.Range(0, 10).Select(i => csvReader.ReadRecord()).ToList();

            Assert.AreEqual(
                Enumerable.Range(0, 10).Select(i => FakeCsvGenerator.GenerateRecord(FakeCsvGenerator.GenerateRecordValues(-10), FakeCsvGenerator.GenerateRecordValues(i))).ToList(),
                result
            );
        }

        [Test]
        public void ReadRecord_ReadingMoreThanExist_ReturnsNull()
        {
            var csvReader = new CustomCsvReader(FakeCsvGenerator.GenerateGoodTextReader(recordAmount: 4, addHeaders: true));

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



    }
}