using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLogger
{
    public interface ILoggerBuilder
    {
        ILoggerBuilder Reset();
        ILoggerBuilder SetName(string name);
        ILoggerBuilder ShowName(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All);
        ILoggerBuilder ShowTimestamp(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All);
        ILoggerBuilder ShowHeader(bool show = true, LogMessageLevel messageLevel = LogMessageLevel.All);
        ILoggerBuilder AddOutputProvider(ILoggerOutputProvider outputProvider, LogMessageLevel messageLevel = LogMessageLevel.All);
        ILogger GetResult();
    }
}
