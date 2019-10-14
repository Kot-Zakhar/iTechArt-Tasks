using System;
using System.Collections.Generic;
using System.Reflection;

namespace DynamicProxy
{
    public abstract class DynamicProxy<T> : DispatchProxy
    {
        protected T decorated;
        //protected static T CreateInstance(T obj)
        //{
        //    object proxy = DispatchProxy.Create<T, DynamicProxy<T>>();
        //    (proxy as DynamicProxy<T>).decorated = obj;
        //    return (T)proxy;
        //}

        // should i try to hide static method of base class?
        private static new T Create<T, TProxy>() where TProxy : DynamicProxy<T>
        {
            return DispatchProxy.Create<T, TProxy>();
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            // can targetMethod be null?
            try
            {
                BeforeInvokeProcessing(targetMethod, args);
                DateTime startTime = DateTime.Now;
                object result = targetMethod?.Invoke(decorated, args);
                TimeSpan processingTime = DateTime.Now - startTime;
                AfterInvokeProcessing(targetMethod, args, result, processingTime);
                return result;
            }
            catch (Exception ex)
            {
                ExceptionProcessing(ex, targetMethod);
                throw;
            }
        }

        protected virtual void BeforeInvokeProcessing(MethodInfo targetMethod, object[] args)
        {
            try
            {
                LogBefore(targetMethod, args);
            }
            catch (Exception ex)
            {
                ExceptionProcessing(ex);
            }
        }

        protected virtual void AfterInvokeProcessing(MethodInfo targetMethod, object[] args, object result, TimeSpan processingTime)
        {
            try
            {
                LogAfter(targetMethod, args, result, processingTime);
            }
            catch (Exception ex)
            {
                ExceptionProcessing(ex);
            }
        }

        protected virtual void ExceptionProcessing(Exception ex, MethodInfo targetMethod = null)
        {
            try
            {
                LogException(ex, targetMethod);
            }
            catch (Exception)
            {
                //do nothing, just skip
            }
        }

        protected abstract void LogBefore(MethodInfo targetMethod, object[] args);
        protected abstract void LogAfter(MethodInfo targetMethod, object[] args, object result, TimeSpan processingTime);
        protected abstract void LogException(Exception ex, MethodInfo targetMethod = null);
    }
}
