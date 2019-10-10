using System;
using System.IO;

namespace CustomLogger
{
    public class LoggerBuilder
    {
        private Logger currentLogger;
        public LoggerBuilder()
        {
            currentLogger = new Logger();
        }
        public LoggerBuilder(Logger loggerToRebuild)
        {
            currentLogger = loggerToRebuild;
        }
        public LoggerBuilder AddOutputProvider(ILoggerOutputProvider outputProvider, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.OutputProviders[(int)messageLevel].Add(outputProvider);
            return this;
        }

        public LoggerBuilder SetName(string name)
        {
            currentLogger.Name = name;
            ShowName();
            return this;
        }

        public LoggerBuilder ShowName(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.ShowName[(int)messageLevel] = show;
            return this;
        }

        public LoggerBuilder SetTimestampFormat(string format)
        {
            currentLogger.TimestampFormat = format;
            ShowTimestamp();
            return this;
        }

        public LoggerBuilder ShowTimestamp(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.ShowTimestamp[(int)messageLevel] = show;
            return this;
        }
        public LoggerBuilder ShowHeader(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.ShowHeader[(int)messageLevel] = show;
            return this;
        }

        public LoggerBuilder Reset()
        {
            currentLogger = new Logger();
            return this;
        }
        public ILogger GetResult()
        {
            Logger loggerToReturn = currentLogger;
            currentLogger = new Logger();
            return loggerToReturn;
        }
    }
}
