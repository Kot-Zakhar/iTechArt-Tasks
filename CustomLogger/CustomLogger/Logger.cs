using System;
using System.Collections.Generic;
using System.Threading;

// restricting access to the file while writing a log
// deleting file while programm in process

namespace CustomLogger
{

    public delegate void WriteToDelegate(string message);

    public class CustomLogger : ILogger
    {
        protected bool disposed = false;
        protected string name = null;
        public string Name
        {
            get
            {
                return name ?? "";
            }
            set
            {
                name = value;
            }
        }
        protected string timestampFormat = null;
        public string TimestampFormat
        {
            get
            {
                return timestampFormat ?? Thread.CurrentThread.CurrentCulture.DateTimeFormat.FullDateTimePattern;
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

        public CustomLogger()
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
            List<ILoggerOutputProvider> outputProviders = null;
            LogMessage finalMessage;

            switch (messageLevel)
            {
                case LogMessageLevel.Error:
                case LogMessageLevel.Warning:
                case LogMessageLevel.Info:
                    if (OutputProviders[(int)messageLevel].Count != 0)
                        outputProviders = OutputProviders[(int)messageLevel];
                    finalMessage = ConstructLogMessage(messageLevel, message);
                    break;
                case LogMessageLevel.All:
                default:
                    finalMessage = ConstructLogMessage(LogMessageLevel.All, message);
                    break;
            }

            foreach (ILoggerOutputProvider outputProvider in OutputProviders[(int)LogMessageLevel.All])
                outputProvider.Output(finalMessage);
            if (outputProviders != null)
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

        public virtual void Dispose()
        {
            if (!disposed)
            {
                disposed = true;
                int levelAmount = Enum.GetValues(typeof(LogMessageLevel)).Length;
                ShowHeader = null;
                ShowName = null;
                ShowTimestamp = null;
                for (int i = 0; i < levelAmount; i++)
                {
                    OutputProviders[i].ForEach(provider =>
                    {
                        if (provider is IDisposable)
                            (provider as IDisposable).Dispose();
                    });
                    OutputProviders[i] = null;
                }

            }
        }
    }
}
