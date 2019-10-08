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
            {
                streams[i] = stream;
            }
        }

        protected virtual void WriteToStream(messageType streamType, string message, bool showHeader = true, bool showNameIfExists = true)
        {
            string finalMessage;
            if (showHeader)
                finalMessage = $"{headers[(int)streamType]} {message}";
            else
                finalMessage = message;

            if (showNameIfExists && Name != null)
                streams[(int)streamType].WriteLine(String.Format("{0}: {1}", this.Name, finalMessage));
            else
                streams[(int)streamType].WriteLine(finalMessage);
        }

        public virtual void Error(string message)
        {
            if (streams[(int)messageType.Error] != null)
                WriteToStream(messageType.Error, message);
        }

        public virtual void Error(Exception ex)
        {
            Error(ex.Message);
        }

        public virtual void Info(string message)
        {
            if (streams[(int)messageType.Info] != null)
                WriteToStream(messageType.Info, message);
        }

        public virtual void Warning(string message)
        {
            if (streams[(int)messageType.Warning] != null)
                WriteToStream(messageType.Warning, message);
        }

    }
}
