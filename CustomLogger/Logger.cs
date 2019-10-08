using System;

namespace CustomLogger
{
    public interface ILogger
    {
        void Error(string message);
        void Error(Exception ex);
        void Warning(string message);
        void Info(string message);
    }

    public delegate void WriteToDelegate(string message);

    public class Logger : ILogger
    {
        protected WriteToDelegate outputDelegate;
        // const or readonly
        protected const string ErrorHeader = "Error: ";
        protected const string InfoHeader = "Info: ";
        protected const string WarningHeader = "Warning: ";

        public string Name { get; set; }

        protected void Init(WriteToDelegate outputDelegate, string name)
        {
            this.Name = name;
            this.outputDelegate = outputDelegate;
        }

        public Logger()
        {
            Init(Console.WriteLine, null);
        }

        public Logger(WriteToDelegate outputDelegate)
        {
            Init(outputDelegate, null);
        }

        public Logger(WriteToDelegate outputDelegate, string name)
        {
            Init(outputDelegate, name);
        }

        protected void Write(string message)
        {
            if (Name != null)
                outputDelegate(String.Format("{0}: {1}", this.Name, message));
            else
                outputDelegate(message);
        }

        public void Error(string message)
        {
            this.Write(ErrorHeader + message);
        }

        public void Error(Exception ex)
        {
            this.Error(ex.Message);
        }

        public void Info(string message)
        {
            this.Write(InfoHeader + message);
        }

        public void Warning(string message)
        {
            this.Write(WarningHeader + message);
        }
    }
}
