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
            return this;
        }

        public LoggerBuilder ShowName(LogMessageLevel messageLevel = LogMessageLevel.All, bool show = true)
        {
            currentLogger.ShowName[(int)messageLevel] = show;
            return this;
        }

        public LoggerBuilder SetTimestampFormat(string format)
        {
            currentLogger.TimestampFormat = format;
            return this;
        }

        public LoggerBuilder ShowTimestamp(LogMessageLevel messageLevel = LogMessageLevel.All, bool show = true)
        {
            currentLogger.ShowTimestamp[(int)messageLevel] = show;
            return this;
        }
        public LoggerBuilder ShowHeader(LogMessageLevel messageLevel = LogMessageLevel.All, bool show = true)
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
