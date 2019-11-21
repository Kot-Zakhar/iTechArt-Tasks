using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.Logging;
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
            object log = context.HttpContext.RequestServices.GetService(typeof(ILogger<ConcurrentRequestsLimitFilterAttribute>));
            ILogger logger = log as ILogger;
            logger?.LogInformation($"{nameof(ConcurrentRequestsLimitFilterAttribute)}-{Thread.CurrentThread.ManagedThreadId} invoke start");
            if (IsLimitReached())
            {
                logger?.LogWarning($"{nameof(ConcurrentRequestsLimitFilterAttribute)}-{Thread.CurrentThread.ManagedThreadId} Request blocked.");
                context.Result = new StatusCodeResult(429);
            }
            else
            {
                Interlocked.Increment(ref _requestsAmount);
                logger?.LogInformation($"{nameof(ConcurrentRequestsLimitFilterAttribute)}-{Thread.CurrentThread.ManagedThreadId}: " + _requestsAmount + " threads.");
            }
        }

        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.Canceled)
                Interlocked.Decrement(ref _requestsAmount);
            ILogger logger = (ILogger)context.HttpContext.RequestServices.GetService(typeof(ILogger));
            logger?.LogInformation($"{nameof(ConcurrentRequestsLimitFilterAttribute)}-{Thread.CurrentThread.ManagedThreadId} invoke end");
        }

        public bool IsLimitReached()
        {
            return _requestsAmount >= MaxConcurrentRequestsAmount;
        }
    }
}
