using System;
using System.IO;
using CustomLogger;
using CustomLogger.ConsoleProvider;
using CustomLogger.FileProvider;

namespace CustomLoggerUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            LoggerOutputFileProvider fileProvider = new LoggerOutputFileProvider();
            LoggerOutputFileProvider warningFileProvider = new LoggerOutputFileProvider(".\\Warnings.txt");
            ILogger log = new LoggerBuilder()
                .AddConsoleProvider(LogMessageLevel.Info)
                .AddConsoleProvider(LogMessageLevel.Error)
                .AddFileProvider(warningFileProvider, LogMessageLevel.Warning)
                .AddFileProvider(fileProvider)
                .ShowHeader()
                .SetName("Zakhar")
                .ShowName(LogMessageLevel.Warning)
                .SetTimestampFormat("G")
                .ShowTimestamp(LogMessageLevel.Error)
                .GetResult();

            log.Info("Every message has a header.");
            log.Info("Only Error messages have timestamps.");
            log.Info("This is info message, wich is printed both console and file.");
            log.Error("This is an error with timestamp, wich is printed both in console and file.");
            log.Warning("this is a warning with name, wich is printed in file only.");

            fileProvider.Dispose();
            warningFileProvider.Dispose();
            Console.WriteLine("Press F to pay respect for the great work...");
            Console.ReadKey();
        }
    }
}
