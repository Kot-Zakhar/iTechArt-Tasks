using System;
using System.IO;

namespace CustomLogger
{
    public class ConsoleLogger : Logger
    {
        public ConsoleLogger() : base()
        {
            SetStream(messageType.Error, Console.Error);
        }

        public ConsoleLogger(string loggerName) : base(loggerName)
        {
            SetStream(messageType.Error, Console.Error);
        }
    }
}
