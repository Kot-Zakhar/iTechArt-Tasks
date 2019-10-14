using System;
using CustomLogger;
using CustomLogger.ConsoleProvider;
using CustomLogger.FileProvider;
using DynamicProxy;
using DynamicProxy.Logging;

namespace DynamicProxy.UsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger consoleLogger = new LoggerBuilder()
                .AddConsoleProvider()
                .AddFileProvider(LogMessageLevel.Error)
                .ShowTimestamp()
                .ShowHeader()
                .SetName("wrappedConsoleLogger")
                .ShowName()
                .Build();

            ILogger wrappedLogger = LoggingProxy<ILogger>.CreateInstance(consoleLogger);

            wrappedLogger.Info("Test info message");
            wrappedLogger.Warning("Text warning");

            Console.WriteLine("Press F to exit...");
            Console.ReadKey();
        }
    }
}
