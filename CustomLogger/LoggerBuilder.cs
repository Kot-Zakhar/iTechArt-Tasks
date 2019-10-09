using System;
using System.IO;

namespace CustomLogger
{
    public interface ILoggerBuilder
    {
        void SetName(string name);
        void EnableTimestamp();
        void AddErrorStream(TextWriter errorStream);
        void AddWarningStream(TextWriter warningStream);
        void AddInfoStream(TextWriter infoStream);
        void 
        Logger GetResult();
    }
    public class LoggerBuilder : ILoggerBuilder
    {
        public void AddErrorStream(TextWriter errorStream)
        {
            throw new NotImplementedException();
        }

        public void AddInfoStream(TextWriter infoStream)
        {
            throw new NotImplementedException();
        }

        public void AddWarningStream(TextWriter warningStream)
        {
            throw new NotImplementedException();
        }

        public void EnableTimestamp()
        {
            throw new NotImplementedException();
        }

        public Logger GetResult()
        {
            throw new NotImplementedException();
        }

        public void SetName(string name)
        {
            throw new NotImplementedException();
        }
    }
}
