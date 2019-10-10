using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLogger
{
    public interface ILoggerBuilder
    {
        ILoggerBuilder SetName(string name);
        ILoggerBuilder EnableTimestamp();
        ILoggerBuilder AddProvider(LogMessageLevel messageLevel);
        ILoggerBuilder AddProvider();
        ILogger GetResult();
    }
}
