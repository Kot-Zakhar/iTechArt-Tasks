using System;
using System.Text;
using System.IO;

namespace CustomLogger
{
    public class FileLogger : Logger, IDisposable
    {

        protected FileStream fileStream;
        protected StreamWriter streamWriter;
        private static string defaultFileName = "log.txt";

        public FileLogger(bool append = true)
        {
            Init(Path.Combine(Directory.GetCurrentDirectory(), defaultFileName), append, null);
        }

        public FileLogger(string fullPath, bool append = true)
        {
            Init(fullPath, append, null);
        }

        public FileLogger(string fullPath, string loggerName, bool append = true)
        {
            Init(fullPath, append, loggerName);
        }

        protected void Init(string fullPath, bool append, string loggerName)
        {
            // question: can i create fileStream with FileAccess.Write and FileShare.ReadWrite?
            fileStream = new FileStream(fullPath, append ? FileMode.Append : FileMode.Create, FileAccess.Write, FileShare.Read);

            streamWriter = new StreamWriter(fileStream, Encoding.Unicode);

            SetEveryStreamTo(streamWriter);
            Name = loggerName;
        }

        public void Dispose()
        {
            streamWriter.Dispose();
            streamWriter = null;
            fileStream.Dispose();
            fileStream = null;
        }
    }
}
