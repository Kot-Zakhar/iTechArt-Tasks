using System;
using System.IO;

namespace CustomLogger
{

    public delegate void WriteToDelegate(string message);

    public enum messageType
    {
        Error,
        Warning,
        Info
    }

    public class Logger : ILogger
    {
        protected readonly string[] headers = { "Error:", "Warning:", "Info:" };

        protected TextWriter[] streams = new TextWriter[Enum.GetValues(typeof(messageType)).Length];

        public string Name { get; protected set; }

        protected void Init(TextWriter defaultStream, string loggerName)
        {
            Name = loggerName;
            for (var i = 0; i < Enum.GetValues(typeof(messageType)).Length; i++)
            {
                streams[i] = defaultStream;
            }
        }

        public Logger()
        {
            Init(Console.Out, null);
        }

        public Logger(TextWriter defaultOutputStream)
        {
            Init(defaultOutputStream, null);
        }

        public Logger(string loggerName)
        {
            Init(Console.Out, loggerName);
        }

        public Logger(TextWriter defaultOutputStream, string loggerName)
        {
            Init(defaultOutputStream, loggerName);
        }

        public TextWriter SetStream(messageType streamType, TextWriter stream)
        {
            TextWriter oldStream = streams[(int)streamType];
            streams[(int)streamType] = stream;
            return oldStream;
        }

        protected void WriteToStream(messageType streamType, string message, bool showHeader = true, bool showNameIfExists = true)
        {
            string finalMessage = (showHeader && headers[(int)streamType] != null) ? $"{headers[(int)streamType]} {message}" : message;
            if (showNameIfExists && Name != null)
                streams[(int)streamType].WriteLine(String.Format("{0}: {1}", this.Name, finalMessage));
            else
                streams[(int)streamType].WriteLine(message);
        }

        public void Error(string message)
        {
            if (streams[(int)messageType.Error] != null)
                WriteToStream(messageType.Error, message);
        }

        public void Error(Exception ex)
        {
            Error(ex.Message);
        }

        public void Info(string message)
        {
            if (streams[(int)messageType.Info] != null)
                WriteToStream(messageType.Info, message);
        }

        public void Warning(string message)
        {
            if (streams[(int)messageType.Warning] != null)
                WriteToStream(messageType.Warning, message);
        }

    }
}
