using System;
using System.IO;
using CustomLogger;

namespace CustomLoggerUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            using (var fileLogger = new FileLogger())
            {
                fileLogger.Error(new Exception("hello"));
                fileLogger.Info("some info message");
            }

            using (
                var namedFileLogger = new FileLogger(
                    Path.Combine(Directory.GetCurrentDirectory(), "anotherLogFile.txt"), 
                    "Another Logger", 
                    false
                )
            ){
                namedFileLogger.Warning("some warning, i don't know");
                namedFileLogger.Error("omg!!!");
            }

            Logger namedLogger = new Logger(Console.Out, "NamedLogger");
            Logger unnamedLogger = new ConsoleLogger();
            namedLogger.Error("some error to the namedLogger");
            unnamedLogger.Info("some info");


            Console.WriteLine("Press F to pay respect..");
            Console.ReadKey();
        }
    }
}
