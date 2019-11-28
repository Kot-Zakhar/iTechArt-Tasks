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
            return ReadValues()?
                .Select((value, index) => new KeyValuePair<string, string>(_headers[index], value))
                .ToDictionary(keyValue => keyValue.Key, keyValue => keyValue.Value);
        }

        public IEnumerable<string> ReadValues()
        {
            return _stream?.ReadLine()?
                .Split(',')
                .Select(s => s.Trim());
        }
    }
}
