using System;
using System.Collections.Generic;
using System.Threading;

namespace CustomLogger
{

    public delegate void WriteToDelegate(string message);

    public class Logger : ILogger
    {
        private string name = null;
        public string Name
        {
            get
            {
                return name == null ? "" : name;
            }
            set
            {
                name = value;
            }
        }
        private string timestampFormat = null;
        public string TimestampFormat
        {
            get
            {
                return timestampFormat == null ? Thread.CurrentThread.CurrentCulture.DateTimeFormat.FullDateTimePattern : timestampFormat;
            }
            set
            {
                timestampFormat = value;
            }
        }
        public string Delimiter = " --- ";
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
            for (int i = 0; i < levelAmount; i++)
            {
                OutputProviders[i] = new List<ILoggerOutputProvider>();
            }
        }

        protected virtual string[] GetHeaders()
        {
            return Enum.GetNames(typeof(LogMessageLevel));
        }
        
        protected virtual LogMessage ConstructLogMessage(LogMessageLevel messageLevel, string message)
        {
            LogMessage result;
            result.timestamp = ShowTimestamp[(int)messageLevel] || ShowTimestamp[(int)LogMessageLevel.All] ? DateTime.Now.ToString(TimestampFormat) : null;
            result.header = ShowHeader[(int)messageLevel] || ShowHeader[(int)LogMessageLevel.All] ? GetHeaders()[(int)messageLevel] : null;
            result.name = ShowName[(int)messageLevel] || ShowName[(int)LogMessageLevel.All] ? Name : null;
            result.message = message;
            return result;
        }

        public virtual void Log(LogMessageLevel messageLevel, string message)
        {
            List<ILoggerOutputProvider> outputProviders = new List<ILoggerOutputProvider>(OutputProviders[(int)LogMessageLevel.All]);
            LogMessage finalMessage;

            switch (messageLevel)
            {
                case LogMessageLevel.Error:
                case LogMessageLevel.Warning:
                case LogMessageLevel.Info:
                    if (OutputProviders[(int)messageLevel].Count != 0)
                        outputProviders.AddRange(OutputProviders[(int)messageLevel]);
                    finalMessage = ConstructLogMessage(messageLevel, message);
                    break;
                case LogMessageLevel.All:
                default:
                    finalMessage = ConstructLogMessage(LogMessageLevel.All, message);
                    break;
            }

            foreach (ILoggerOutputProvider outputProvider in outputProviders)
                outputProvider.Output(finalMessage);
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
