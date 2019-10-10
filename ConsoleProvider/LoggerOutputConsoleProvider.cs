using System;
using CustomLogger;

namespace CustomLogger.ConsoleProvider
{
    public static class LoggerBuilderConsoleExtention
    {
        public static LoggerBuilder AddConsoleProvider(this LoggerBuilder builder, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            builder.AddOutputProvider(new LoggerOutputConsoleProvider(), messageLevel);
            return builder;
        }
    }
    public class LoggerOutputConsoleProvider : ILoggerOutputProvider
    {
        protected virtual string ProcessField(string field, string left = "", string right = "", string ifNull = "")
        {
            return field != null ? left + field + right : ifNull;
        }
        public virtual string CreateLine(LogMessage message)
        {
            return ProcessField(message.timestamp, "[", "]\t") +
                   ProcessField(message.header, "", "\t") +
                   ProcessField(message.name, "\"", "\"\t") +
                   ProcessField(message.message);
        }
        public void Output(LogMessage message)
        {
            Console.WriteLine(CreateLine(message));
        }
    }
}
