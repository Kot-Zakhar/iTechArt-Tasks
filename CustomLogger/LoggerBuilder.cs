using System;
using System.IO;

namespace CustomLogger
{
    public class LoggerBuilder : ILoggerBuilder
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
        public ILoggerBuilder AddOutputProvider(ILoggerOutputProvider outputProvider, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.OutputProviders[(int)messageLevel].Add(outputProvider);
            return this;
        }

        public ILoggerBuilder SetName(string name)
        {
            currentLogger.Name = name;
            return this;
        }

        public ILoggerBuilder ShowName(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.ShowName[(int)messageLevel] = show;
            return this;
        }

        public ILoggerBuilder ShowTimestamp(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.ShowTimestamp[(int)messageLevel] = show;
            return this;
        }
        public ILoggerBuilder ShowHeader(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            currentLogger.ShowHeader[(int)messageLevel] = show;
            return this;
        }

        public ILoggerBuilder Reset()
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
