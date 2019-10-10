using System;


namespace CustomLogger
{
    public interface ILoggerOutputProvider
    {
        void Output(string message);
    }
}
