﻿using System;
using System.Collections.Generic;
using System.Text;

namespace CustomLogger
{
    public enum LogMessageLevel
    {
        All,
        Error,
        Warning,
        Info
    }
    public interface ILogger: IDisposable
    {
        void Log(LogMessageLevel messageLevel, string message);
        void Error(string message);
        void Error(Exception ex);
        void Warning(string message);
        void Info(string message);
    }
}
