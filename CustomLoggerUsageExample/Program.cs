using System;
using System.IO;
using CustomLogger;
using CustomLogger.ConsoleProvider;

namespace CustomLoggerUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger log = new LoggerBuilder()
                .AddConsoleProvider(LogMessageLevel.Info)
                .AddConsoleProvider(LogMessageLevel.Error)
                .SetName("Zakhar")
                .ShowTimestamp()
                .ShowHeader()
                .SetTimestampFormat("G")
                .GetResult();

            log.Info("Hello, this is info.");
            log.Error("this is an error");

            Console.WriteLine("Press F to pay respect...");
            Console.ReadKey();
        }
    }
}
