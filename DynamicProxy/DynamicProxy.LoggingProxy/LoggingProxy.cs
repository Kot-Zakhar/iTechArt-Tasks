﻿using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CustomLogger;
using CustomLogger.ConsoleProvider;

namespace DynamicProxy.Logging
{
    public class LoggingProxy<T> : DynamicProxy<T>
    {
        protected ILogger logger;
        
        public LoggingProxy()
        {
            this.logger = new LoggerBuilder().AddConsoleProvider().ShowHeader().ShowTimestamp().Build();
        }

        public static T CreateInstance(T obj, ILogger logger = null)
        {
            object proxy = Create<T, LoggingProxy<T>>();
            (proxy as LoggingProxy<T>).decorated = obj;
            if (logger != null)
                (proxy as LoggingProxy<T>).logger = logger;
            return (T)proxy;
        }

        protected StringBuilder BuildInfo(MethodInfo targetMethod)
        {
            return new StringBuilder()
                .Append($"{decorated.GetType().Name}.")
                .Append($"{targetMethod?.Name}");
        }

        protected StringBuilder AddArgsInfo(StringBuilder builder, MethodInfo targetMethod, object[] args)
        {
            builder.Append("(");
            var parameters = targetMethod.GetParameters();
            for (var i = 0; i < parameters.Length; i++)
            {
                if (i != 0)
                    builder.Append(", ");
                builder.Append($"{parameters[i].Name}: ({args[i].GetType().Name}) {args[i].ToString()}");
            }
            builder.Append(")");
            return builder;
        }

        protected override void LogBefore(MethodInfo targetMethod, object[] args)
        {
            var beforeMessageBuilder = BuildInfo(targetMethod);
            AddArgsInfo(beforeMessageBuilder, targetMethod, args)
                .Append(" Starting execution");

            logger.Info(beforeMessageBuilder.ToString());
        }

        protected override void LogAfter(MethodInfo targetMethod, object[] args, object result, TimeSpan processingTime)
        {
            var afterMessageBuilder = BuildInfo(targetMethod);
            AddArgsInfo(afterMessageBuilder, targetMethod, args)
                .Append(" Finishing execution")
                .Append($" in {processingTime.TotalMilliseconds} ms")
                .Append($" with result: ({result?.GetType().Name}){result}");

            logger.Info(afterMessageBuilder.ToString());
        }

        protected override void LogException(Exception ex, MethodInfo targetMethod = null)
        {
            var exceptionMessageBuilder = BuildInfo(targetMethod)
                .Append(" Throwing exception")
                .Append($": '{ex.Message}'");

            logger.Error(exceptionMessageBuilder.ToString());
        }
    }
}
