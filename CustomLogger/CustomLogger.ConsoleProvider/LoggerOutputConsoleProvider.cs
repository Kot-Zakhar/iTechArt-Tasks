using System;

namespace CustomLogger.ConsoleProvider
{
    public static class LoggerBuilderConsoleExtention
    {
        public static LoggerBuilder AddConsoleProvider(this LoggerBuilder builder, LogMessageLevel messageLevel = LogMessageLevel.All, ConsoleColor color = ConsoleColor.Gray)
        {
            builder.AddOutputProvider(new LoggerOutputConsoleProvider(color), messageLevel);
            return builder;
        }
    }
    public class LoggerOutputConsoleProvider : ILoggerOutputProvider
    {
        private ConsoleColor color;
        public LoggerOutputConsoleProvider(ConsoleColor color)
        {
            this.color = color;
        }
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
            lock (Console.Out)
            {
                Console.ForegroundColor = this.color;
                Console.Write(ProcessField(message.timestamp, "[", "]\t"));
                Console.ResetColor();

                Console.BackgroundColor = this.color;
                Console.ForegroundColor = ConsoleColor.Black;
                Console.Write(
                    ProcessField(message.header, "", "\t") +
                    ProcessField(message.name, "\"", "\"\t")
                );
                Console.ResetColor();

                Console.ForegroundColor = this.color;
                Console.Write(ProcessField(message.message));
                Console.ResetColor();

                Console.WriteLine();
            }
        }
    }
}
