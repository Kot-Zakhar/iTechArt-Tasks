﻿using System;
using System.IO;

namespace CustomLogger.FileProvider
{
    public static class LoggerBuilderFileExtention
    {
        public static LoggerBuilder AddFileProvider(this LoggerBuilder builder, LogMessageLevel messageLevel = LogMessageLevel.All, string path = null, bool append = true)
        {
            LoggerOutputFileProvider fileProvider = new LoggerOutputFileProvider(path, append, messageLevel);
            builder.AddOutputProvider(fileProvider, messageLevel);
            return builder;
        }
    }
    public class LoggerOutputFileProvider : ILoggerOutputProvider, IDisposable
    {
        protected bool disposed = false;
        protected static string defaultFileName = "Logs.txt";
        protected static string defaultDirectory = Directory.GetCurrentDirectory();
        protected StreamWriter fileWriter;

        public LoggerOutputFileProvider(string path = null, bool append = true, LogMessageLevel messageLevel = LogMessageLevel.All)
        {
            var fileStream = new FileStream(
                path == null ? Path.Combine(defaultDirectory, Enum.GetName(typeof(LogMessageLevel), messageLevel) + defaultFileName) : Path.GetFullPath(path),
                append ? FileMode.Append : FileMode.Create, 
                FileAccess.Write, 
                FileShare.ReadWrite);
            fileWriter = new StreamWriter(fileStream, System.Text.Encoding.Unicode);
        }
        protected virtual string ProcessField(string field, string left = "", string right = "", string ifNull = "")
        {
            return field != null ? left + field + right : ifNull;
        }
        public virtual string CreateLine(LogMessage message)
        {
            return ProcessField(message.timestamp, "[", "]\t") +
                   ProcessField(message.header, "", "\t") +
                   ProcessField(message.name, "\"", "\"\t") +
                   ProcessField(message.message);
        }
        public void Output(LogMessage message)
        {
            lock (fileWriter)
            {
                if (fileWriter != null)
                {
                    fileWriter.WriteLine(CreateLine(message));
                    fileWriter.Flush();
                }
                else
                    throw new IOException("Stream has been closed.");
            }
        }

        public void Dispose()
        {
            if (!disposed)
            {
                fileWriter.Dispose();
                fileWriter = null;
                disposed = true;
            }
        }
    }
}
