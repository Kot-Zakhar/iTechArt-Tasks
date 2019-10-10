using System;


namespace CustomLogger
{
    public struct LogMessage
    {
        public string timestamp;
        public string header;
        public string name;
        public string message;
    }
    public interface ILoggerOutputProvider
    {
        void Output(LogMessage message);
    }
}
