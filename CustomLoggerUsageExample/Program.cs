using System;
using CustomLogger;

namespace CustomLoggerUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger namedLogger = new Logger(Console.Out, "NamedLogger");
            ConsoleLogger log = new ConsoleLogger("My Console");

            namedLogger.Error("some error to the namedLogger");
            log.Info("some info");


            Console.WriteLine("Press F to pay respect..");
            Console.ReadKey();
        }
    }
}
