﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Threading;

namespace RateLimit.WebApp.Filters
{
    //public class ConcurrentRequestLimitFilter : TypeFilterAttribute
    //{
    //    public ConcurrentRequestLimitFilter(params object[] requestAmount) : base(typeof(ConcurrentRequestLimitFilterImpl))
    //    {
    //        Arguments = requestAmount;
    //    }
    //}

    public class ConcurrentRequestsLimitFilterAttribute: ActionFilterAttribute
    {
        private int _requestsAmount = 0;
        public int MaxConcurrentRequestsAmount { get; private set; }
        
        public ConcurrentRequestsLimitFilterAttribute(int maxConcurrentRequestsAmount = 10)
        {
            MaxConcurrentRequestsAmount = maxConcurrentRequestsAmount;
        }

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            Log(ConsoleColor.Yellow, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId} invoke start");
            if (IsLimitReached())
            {
                Log(ConsoleColor.Red, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId} Request blocked.");
                context.Result = new StatusCodeResult(429);
            }
            else
            {
                Interlocked.Increment(ref _requestsAmount);
                Log(ConsoleColor.Green, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId}: " + _requestsAmount + " threads.");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.Canceled)
                Interlocked.Decrement(ref _requestsAmount);
            Log(ConsoleColor.Yellow, $"ConcurrentRequestsLimitMiddleware_{Thread.CurrentThread.ManagedThreadId} invoke end");
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

        public bool IsLimitReached()
        {
            return _requestsAmount >= MaxConcurrentRequestsAmount;
        }
    }
}