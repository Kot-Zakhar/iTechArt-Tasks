using System;
using System.Collections.Generic;
using System.Text;

namespace CustomCsv
{
    public class CustomCsvReaderOptions
    {
        public IList<string> Headers { get; set; } = null;
        public bool AreHeadersInStream { get; set; } = true;
    }
}
