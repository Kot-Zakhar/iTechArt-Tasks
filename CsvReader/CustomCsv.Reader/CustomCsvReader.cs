using System;
using System.Linq;
using System.Collections.Generic;
using System.IO;

namespace CustomCsv
{
    public class CustomCsvReader
    {
        private readonly TextReader _stream;
        private IList<string> _headers = new List<string>();
        private readonly CustomCsvReaderOptions _options;

        public CustomCsvReader(TextReader stream, CustomCsvReaderOptions options = null)
        {
            _stream = stream ?? throw new NullReferenceException();
            _options = options ?? new CustomCsvReaderOptions();
            if (_options.AreHeadersInFile)
            {
                ParseRowAsHeaders();
            }
            else
            {
                _headers = _options.Headers;
                if (_options.AreHeadersRequired && _headers == null)
                    throw new Exception("Headers are not provided.");
            }
        }

        private void ParseRowAsHeaders()
        {
            _headers = _stream?.ReadLine()
                .Split(',')
                .Select(s => s.Trim())
                .ToList();
        }

        public IDictionary<string, string> ReadRecord()
        {
            if (_options.AreHeadersRequired && _headers.Count == 0)
                throw new Exception("No headers.");
            return ReadValues()
                .Select((value, index) => new KeyValuePair<string, string>(_headers[index], value))
                .ToDictionary(keyValue => keyValue.Key, keyValue => keyValue.Value);
        }

        public IEnumerable<string> ReadValues()
        {
            return _stream.ReadLine()
                .Split(',')
                .Select(s => s.Trim());
        }
    }
}
