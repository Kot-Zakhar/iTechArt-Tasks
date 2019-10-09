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

        protected void Init(TextWriter defaultStream, string loggerName)
        {
            Name = loggerName;
            SetEveryStreamTo(defaultStream);
        }

        public TextWriter SetStream(messageType streamType, TextWriter stream)
        {
            TextWriter oldStream = streams[(int)streamType];
            streams[(int)streamType] = stream;
            return oldStream;
        }

        public void SetEveryStreamTo(TextWriter stream)
        {
            for (var i = 0; i < Enum.GetValues(typeof(messageType)).Length; i++)
                streams[i] = stream;
        }

        protected virtual void WriteToStream(messageType streamType, string message, bool showHeader = true, bool showNameIfExists = true)
        {
            if (streams[(int)messageType.Error] == null)
                throw new IOException("Stream is unreachable.");

            string finalMessage;
            if (showHeader)
                finalMessage = $"{headers[(int)streamType]} {message}";
            else
                finalMessage = message;

            TextWriter currentStream = streams[(int)streamType];
            if (showNameIfExists && Name != null)
                currentStream.WriteLine(String.Format("{0}: {1}", this.Name, finalMessage));
            else
                currentStream.WriteLine(finalMessage);
            currentStream.Flush();
        }

        public virtual void Error(Exception ex)
        {
            Error(ex.Message);
        }

        public virtual void Error(string message)
        {
            WriteToStream(messageType.Error, message);
        }

        public virtual void Info(string message)
        {
            WriteToStream(messageType.Info, message);
        }

        public virtual void Warning(string message)
        {
            WriteToStream(messageType.Warning, message);
        }

    }
}
