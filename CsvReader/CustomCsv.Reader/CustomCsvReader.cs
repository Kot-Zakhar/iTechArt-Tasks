using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace CustomCsv
{
    public class CustomCsvReader
    {
        private readonly TextReader _stream;
        private IList<string> _headers;

        public CustomCsvReader(TextReader stream, CustomCsvReaderOptions options = null)
        {
            _stream = stream ?? throw new NullReferenceException();
            var currentOptions = options ?? new CustomCsvReaderOptions();
            if (currentOptions.AreHeadersInStream)
            {
                ParseRowAsHeaders();
                if (_headers == null)
                    throw new Exception("File is empty. Cannot read headers.");

                if (currentOptions.Headers != null)
                {
                    if (currentOptions.Headers.Count != _headers.Count)
                        throw new Exception("Provided headers amount not equal to the amount of headers in file.");
                    _headers = currentOptions.Headers;
                }
            }
            else
                _headers = currentOptions.Headers;
        }

        private void ParseRowAsHeaders()
        {
            _headers = _stream?.ReadLine()?
                .Split(',')
                .Select(s => s.Trim())
                .ToList();
        }

        public IDictionary<string, string> ReadRecord()
        {
            if (_headers == null || _headers.Count == 0)
                throw new Exception("No headers.");
            IList<string> values = ReadValues();
            if (values == null)
                return null;
            if (values.Count != _headers.Count)
                throw new Exception("Values amount not equal headers amount.");
            return values
                .Select((value, index) => new KeyValuePair<string, string>(_headers[index], value))
                .ToDictionary(keyValue => keyValue.Key, keyValue => keyValue.Value);
        }

        public IList<string> ReadValues()
        {
            return _stream?.ReadLine()?
                .Split(',')
                .Select(s => s.Trim())
                .ToList();
        }
    }
}
