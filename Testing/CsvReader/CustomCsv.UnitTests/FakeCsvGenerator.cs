using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace CustomCsv.UnitTests
{
    public static class FakeCsvGenerator
    {
        public const int magicNumber = 4;

        public static string[] GenerateRecordValues(int recordIndex, int fieldAmount = magicNumber)
        {
            return Enumerable.Range(0, fieldAmount).Select(index => $"{recordIndex}record_{index}field").ToArray();
        }

        public static Dictionary<string, string> GenerateRecord(IList<string> headers, IList<string> values)
        {
            // Open for more elegant solutions
            return headers.Join(values, header => headers.IndexOf(header), value => values.IndexOf(value),
                    (header, value) => new KeyValuePair<string, string>(header, value))
                .ToDictionary(pair => pair.Key, pair => pair.Value);
        }

        public static TextReader GenerateNotNormalizedTextReader(int recordAmount = magicNumber, int headerAmount = magicNumber, int minFieldAmount = magicNumber / 2, int maxFieldAmount = 2 * magicNumber, bool addHeaders = false)
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

        public static TextReader GenerateGoodTextReader(int recordAmount = magicNumber, int fieldAmount = magicNumber, bool addHeaders = false)
        {
            string[][] records = Enumerable.Range(addHeaders ? -1 : 0, recordAmount + (addHeaders ? 1 : 0)).Select(index => GenerateRecordValues(index, fieldAmount)).ToArray();

            var abstractCsvFile = string.Join('\n', records.Select(record => string.Join(',', record)));
            return new StringReader(abstractCsvFile);
        }

    }
}
