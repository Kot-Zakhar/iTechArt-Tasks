using System;
using System.Threading;
using CustomLogger;
using CustomLogger.ConsoleProvider;
using CustomLogger.FileProvider;

namespace CustomLoggerUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            ILogger log = new LoggerBuilder()
                //.AddConsoleProvider(LogMessageLevel.Info)
                //.AddConsoleProvider(LogMessageLevel.Error)
                .AddFileProvider(LogMessageLevel.Warning, ".\\Warnings.txt")
                //.ShowHeader()
                .SetName("LOG1")
                .ShowName(LogMessageLevel.Warning)
                //.SetTimestampFormat("G")
                //.ShowTimestamp(LogMessageLevel.Error)
                .GetResult();

            ILogger log2 = new LoggerBuilder()
                .AddFileProvider(LogMessageLevel.Warning, ".\\Warnings.txt")
                .ShowHeader()
                .SetName("LOG2")
                .ShowName()
                .GetResult();

            Thread thread1 = new Thread(PrintingLog);
            thread1.Start(log);

            Thread thread2 = new Thread(PrintingLog);
            thread2.Start(log2);

            //log.Info("Every message has a header.");
            //log.Info("Only Error messages have timestamps.");
            //log.Info("This is info message, wich is printed both console and file.");
            //log.Error("This is an error with timestamp, wich is printed both in console and file.");
            //log.Warning("this is a warning with name, wich is printed in file only.");

            thread1.Join();
            thread2.Join();

            log.Dispose();
            Console.WriteLine("Press key...");
            Console.ReadKey();
        }

        static void PrintingLog(object log)
        {
            for (int i = 0; i < 100; i++)
                (log as ILogger).Warning(Thread.CurrentThread.ManagedThreadId.ToString());
        }
    }
}
