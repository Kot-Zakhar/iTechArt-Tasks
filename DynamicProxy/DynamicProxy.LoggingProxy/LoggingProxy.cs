using System;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using CustomLogger;

namespace DynamicProxy.Logging
{
    public class LoggingProxy<T> : DynamicProxy<T>
    {
        protected ILogger logger;
        private static TypeEqualityComparer typeComparer = new TypeEqualityComparer();

        public LoggingProxy(ILogger logger)
        {
            this.logger = logger;
        }

        public static T CreateInstance(T obj, ILogger logger)
        {
            // bug is here:
            // The base type 'DynamicProxy.Logging.LoggingProxy`1[[CustomLogger.ILogger, CustomLogger, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null]]' must have a public parameterless constructor. (Parameter 'TProxy')
            object proxy = Create<T, LoggingProxy<T>>();
            (proxy as LoggingProxy<T>).decorated = obj;
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
            return builder
                .Append("(")
                .AppendJoin(", ",
                    args.Join(
                       targetMethod.GetParameters(),
                       (object arg) => arg.GetType(),
                       (ParameterInfo param) => param.GetType(),
                       (object arg, ParameterInfo param) => $"{param.Name}: ({arg.GetType().Name}){arg.ToString()}",
                       typeComparer
                    )
                )
                .Append(")");
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
                .Append($" with result: ({result.GetType().Name}){result}");

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


    class TypeEqualityComparer : IEqualityComparer<Type>
    {
        public bool Equals([AllowNull] Type x, [AllowNull] Type y)
        {
            if ((x == null) && (y == null))
                return true;
            else if ((x == null) || (y == null))
                return false;
            else
                return x == y || x.IsSubclassOf(y) || y.IsSubclassOf(x);            
        }

        public int GetHashCode([DisallowNull] Type obj)
        {
            return obj.GetHashCode();
        }
    }
}
