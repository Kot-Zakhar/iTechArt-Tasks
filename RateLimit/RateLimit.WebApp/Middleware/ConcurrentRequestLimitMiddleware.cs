using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace RateLimit.WebApp.Middleware
{
    public class ConcurrentRequestsLimitMiddleware
    {
        private int _concurrentRequestsCount;

        private readonly RequestDelegate _next;
        private readonly int _maxAmount;

        public ConcurrentRequestsLimitMiddleware(RequestDelegate next, 
            int maxAmount)
        {
            _concurrentRequestsCount = 0;

            _next = next ?? throw new ArgumentNullException(nameof(next));
            _maxAmount = maxAmount;
        }

        private void Log(ConsoleColor c, string message)
        {
            lock (Console.Out)
            {
                ConsoleColor old = Console.ForegroundColor;
                Console.ForegroundColor = c;
                Console.WriteLine(message);
                Console.ForegroundColor = old;
            }
        }

        public async Task Invoke(HttpContext context)
        {
            Log(ConsoleColor.Yellow, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId} invoke start");
            if (IsLimitReached())
            {
                IHttpResponseFeature responseFeature = context.Features.Get<IHttpResponseFeature>();

                responseFeature.StatusCode = StatusCodes.Status429TooManyRequests;
                responseFeature.ReasonPhrase = "Concurrent request limit exceeded.";

                Log(ConsoleColor.Red, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId} Request blocked.");
            }
            else
            {
                Interlocked.Increment(ref _concurrentRequestsCount);
                Log(ConsoleColor.Green, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId}: " + _concurrentRequestsCount + " threads.");
                await _next(context);
                Interlocked.Decrement(ref _concurrentRequestsCount);
            }
            Log(ConsoleColor.Yellow, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId} invoke end");
        }

        private bool IsLimitReached()
        {
            return _concurrentRequestsCount >= _maxAmount;
        }
    }

    public static class ConcurrentRequestsLimitMiddlewareExtension
    {
        public static IApplicationBuilder UseConcurrentLimit(this IApplicationBuilder builder, int maxRequestsAmount = 10)
        {
            return builder.UseMiddleware<ConcurrentRequestsLimitMiddleware>(maxRequestsAmount);
        }
    }
}
