using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLogger
{
    public interface ILoggerOutputProvider
    {
        void ProvideOutput();
    }
}
