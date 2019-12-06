using Microsoft.AspNetCore.Builder;
using System;
using System.Collections.Generic;
using System.Text;

namespace ProfileWebApp.Middleware
{
    public static class TimeTrackingMiddlewareExtension
    {
        public static IApplicationBuilder UseTimeTrackingMiddleware(this IApplicationBuilder app)
        {
            return app.UseMiddleware<TimeTrackingMiddleWare>();
        }
    }
}
