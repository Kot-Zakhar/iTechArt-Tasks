using System;
using System.IO;

namespace CustomLogger
{
    public class FileLogger : Logger, IDisposable
    {
        protected StreamWriter fileStream;
        private static string defaultFileName = "log.txt";

        protected void Init(string fileName, bool append, string loggerName)
        {
            fileStream = new StreamWriter(fileName, append, System.Text.Encoding.Unicode);
            Name = loggerName;
        }

        public FileLogger(bool append = true)
        {
            Init(Path.Combine(Directory.GetCurrentDirectory(), defaultFileName), append, null);
        }

        public FileLogger(string fileName, bool append = true)
        {
            Init(fileName, append, null);
        }

        public FileLogger(string fileName, string loggerName, bool append = true)
        {
            Init(fileName, append, loggerName);
        }

        public void Dispose()
        {
            fileStream.Flush();
            fileStream.Close();
            fileStream.Dispose();
            fileStream = null;
        }
    }
}
