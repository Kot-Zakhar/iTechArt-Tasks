using System;
using System.Collections.Generic;

namespace CustomLogger
{

    public delegate void WriteToDelegate(string message);

    public class Logger : ILogger
    {
        protected readonly string[] headers = { "Error:", "Warning:", "Info:" };
        
        public string Name;
        public bool[] ShowHeader;
        public bool[] ShowName;
        public bool[] ShowTimestamp;
        public List<ILoggerOutputProvider>[] OutputProviders;

        public Logger()
        {
            int levelAmount = Enum.GetValues(typeof(LogMessageLevel)).Length;
            ShowHeader = new bool[levelAmount];
            ShowName = new bool[levelAmount];
            ShowTimestamp = new bool[levelAmount];
            OutputProviders = new List<ILoggerOutputProvider>[levelAmount];
        }

        public virtual void Log(LogMessageLevel messageLevel, string message)
        {
            //if (OutputProviders[(int)messageLevel].Count != 0)
            //{
            //    foreach(ILoggerOutputProvider output in OutputProviders[(int)messageLevel])
            //    {
            //        output.Output()
            //    }
            //} 
            //else
            //{

            //}
        }

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
