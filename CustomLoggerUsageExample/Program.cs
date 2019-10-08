using System;
using CustomLogger;

namespace CustomLoggerUsageExample
{
    class Program
    {
        static void Main(string[] args)
        {
            Logger namedLogger = new Logger(Console.WriteLine, "NamedLogger");
            Logger unnamedLogger = new Logger(line => Console.WriteLine("prefix " + line + " postfix"));
            Logger noParamsLogger = new Logger();


            namedLogger.Error("some error to the namedLogger");
            unnamedLogger.Warning("some warning to unnamedLogger");
            noParamsLogger.Info("some info with no params");


            Console.WriteLine("Press F to pay respect..");
            Console.ReadKey();
        }
    }
}
