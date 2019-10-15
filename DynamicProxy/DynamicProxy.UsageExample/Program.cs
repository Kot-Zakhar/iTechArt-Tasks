using System;
using System.IO;
using System.Threading.Tasks;
using System.Threading;
using CustomLogger;
using CustomLogger.ConsoleProvider;
using CustomLogger.FileProvider;
using DynamicProxy.Logging;

namespace DynamicProxy.UsageExample
{
    interface IFileStream
    {
        IAsyncResult BeginRead(byte[] array, int offset, int numBytes, AsyncCallback callback, object? state);
        IAsyncResult BeginWrite(byte[] array, int offset, int numBytes, AsyncCallback callback, object? state);
        Task CopyToAsync(Stream destination, int bufferSize, CancellationToken cancellationToken);
        ValueTask DisposeAsync();
        int EndRead(IAsyncResult asyncResult);
        void EndWrite(IAsyncResult asyncResult);
        void Flush();
        void Flush(bool flushToDisk);
        Task FlushAsync(CancellationToken cancellationToken);
        void Lock(long position, long length);
        int Read(Span<byte> buffer);
        int Read(byte[] array, int offset, int count);
        ValueTask<int> ReadAsync(Memory<byte> buffer, CancellationToken cancellationToken = default);
        Task<int> ReadAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default);
        int ReadByte();
        long Seek(long offset, SeekOrigin origin);
        void SetLength(long value);
        void Unlock(long position, long length);
        void Write(byte[] array, int offset, int count);
        void Write(ReadOnlySpan<byte> buffer);
        Task WriteAsync(byte[] buffer, int offset, int count, CancellationToken cancellationToken = default);
        ValueTask WriteAsync(ReadOnlyMemory<byte> buffer, CancellationToken cancellationToken = default);
        void WriteByte(byte value);
    }
    class NewFileStream : FileStream, IFileStream
    {
        public NewFileStream(string path, FileMode mode, FileAccess access) : base(path, mode, access) { }
    }
    class Program
    {
        static void Main(string[] args)
        {
            ILogger consoleLogger = new LoggerBuilder()
                .AddConsoleProvider(LogMessageLevel.Info, ConsoleColor.Yellow)
                .AddConsoleProvider(LogMessageLevel.Error, ConsoleColor.Red)
                .ShowHeader()
                .SetName("wrapperLogger")
                .ShowName()
                .Build();

            ILogger myLogger = new LoggerBuilder()
                .AddConsoleProvider(LogMessageLevel.All, ConsoleColor.Blue)
                .Build();

            IFileStream file = new NewFileStream(Path.GetFullPath("./exampleFile"), FileMode.Create, FileAccess.ReadWrite);

            var wrappedFileStream = LoggingProxy.Create(file, consoleLogger);

            Random rand = new Random();
            int length = 10000;
            byte[] randomValues = new byte[length];
            rand.NextBytes(randomValues);

            myLogger.Info("Writing values synchronously.");
            wrappedFileStream.Write(randomValues, 0, length);
            myLogger.Info("End of synchronous writing.");
            myLogger.Info("Writing values asynchronously.");
            var writingTask = wrappedFileStream.WriteAsync(randomValues, 0, length);
            myLogger.Info("Keep processing main thread.");
            wrappedFileStream.Seek(0, SeekOrigin.Begin);
            myLogger.Info("Async reading");
            var readingTask = wrappedFileStream.ReadAsync(randomValues, 0, length);
            myLogger.Info("Keep processing main thread.");
            writingTask.Wait(1000);
            readingTask.Wait(1000);
            myLogger.Info($"Readed {readingTask.Result}");
            Console.WriteLine("Press F to exit...");
            Console.ReadKey();
        }
    }
}
