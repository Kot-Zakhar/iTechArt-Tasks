using System;
using System.Collections.Generic;
using System.IO;

namespace CustomLogger
{

    public delegate void WriteToDelegate(string message);

    public abstract class Logger : ILogger
    {
        protected readonly string[] headers = { "Error:", "Warning:", "Info:" };
        
        // showHeader and showName should be configurable for each logLevel
        protected bool ShowHeader = true;
        protected bool ShowName = true;
        public string Name { get; protected set; }

        public Logger(ILoggerOutputProvider provider) { }

        public abstract void Log(LogMessageLevel messageLevel, string message);

        public virtual void Error(Exception ex)
        {
            Error(ex.Message);
        }

        public virtual void Error(string message)
        {
            Log(LogMessageLevel.Error, message);
        }

        public virtual void Info(string message)
        {
            Log(LogMessageLevel.Info, message);
        }

        public virtual void Warning(string message)
        {
            Log(LogMessageLevel.Warning, message);
        }

    }
}
