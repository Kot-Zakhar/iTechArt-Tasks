using System;
using System.Threading.Tasks;
using System.Diagnostics;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace ProfileWebApp.Middleware
{
    public class TimeTrackingMiddleWare
    {
        private readonly RequestDelegate _next;
        private readonly ILogger _logger;
        public TimeTrackingMiddleWare(RequestDelegate next, ILogger<TimeTrackingMiddleWare> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            Stopwatch sw = new Stopwatch();
            _logger.LogTrace($"{nameof(TimeTrackingMiddleWare)} - Tracking start.");
            sw.Start();
            await _next(context);
            sw.Stop();
            _logger.LogTrace($"{nameof(TimeTrackingMiddleWare)} - Tracking stop.");
            _logger.LogInformation($"{nameof(TimeTrackingMiddleWare)} - {sw.ElapsedMilliseconds}ms");
        }
    }
}
