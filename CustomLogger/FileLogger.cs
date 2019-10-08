using System;
using System.IO;

namespace CustomLogger
{
    public class FileLogger : Logger, IDisposable
    {
        protected StreamWriter fileStream;
        private static string defaultFileName = "log.txt";

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

        protected void Init(string fileName, bool append, string loggerName)
        {
            fileStream = new StreamWriter(fileName, append, System.Text.Encoding.Unicode);
            SetEveryStreamTo(fileStream);
            Name = loggerName;
        }

        protected override void WriteToStream(messageType streamType, string message, bool showHeader = true, bool showNameIfExists = true)
        {
            if (streams[(int)streamType] != null)
                base.WriteToStream(streamType, message, showHeader, showNameIfExists);
            else
                throw new IOException("Stream has been closed or never been created.");
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
