﻿using System;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace DynamicProxy
{
    public abstract class DynamicProxy : DispatchProxy
    {
        protected object decorated;
        
        protected static T Create<T>(T obj)
        {
            object proxy = DispatchProxy.Create<T, DynamicProxy>();
            (proxy as DynamicProxy).decorated = obj;
            return (T)proxy;
        }

        private static new T Create<T, TProxy>() where TProxy : DynamicProxy
        {
            return DispatchProxy.Create<T, TProxy>();
        }

        protected override object Invoke(MethodInfo targetMethod, object[] args)
        {
            try
            {
                BeforeInvokeProcessing(targetMethod, args);
                DateTime startTime = DateTime.Now;
                object result = targetMethod?.Invoke(decorated, args);
                AfterInvokeProcessing(targetMethod, args, result, startTime);
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
                BeforeInvokeNotify(targetMethod, args);
            }
            catch (Exception ex)
            {
                ExceptionProcessing(ex);
            }
        }

        protected virtual void AfterInvokeProcessing(MethodInfo targetMethod, object[] args, object result, DateTime startTime)
        {
            var resultTask = result as Task;
            if (resultTask != null)
            {
                resultTask.ContinueWith(task =>
                {
                    if (task.Exception != null)
                    {
                        ExceptionNotify(task.Exception, targetMethod);
                    }
                    else
                    {
                        object result = null;
                        if (task.GetType().IsGenericType)
                        {
                            var property = task.GetType().GetTypeInfo().GetProperties()
                                .FirstOrDefault(p => p.Name == "Result");
                            if (property != null)
                            {
                                result = property.GetValue(task);
                            }
                        }
                        AfterInvokeNotify(targetMethod, args, result, DateTime.Now - startTime);
                    }
                });
            }
            else
            {
                try
                {
                    AfterInvokeNotify(targetMethod, args, result, DateTime.Now - startTime);
                }
                catch (Exception ex)
                {
                    ExceptionProcessing(ex);
                }
            }
        }

        protected virtual void ExceptionProcessing(Exception ex, MethodInfo targetMethod = null)
        {
            try
            {
                ExceptionNotify(ex, targetMethod);
            }
            catch (Exception)
            {
                //do nothing, just skip
            }
        }

        protected abstract void BeforeInvokeNotify(MethodInfo targetMethod, object[] args);
        protected abstract void AfterInvokeNotify(MethodInfo targetMethod, object[] args, object result, TimeSpan processingTime);
        protected abstract void ExceptionNotify(Exception ex, MethodInfo targetMethod = null);
    }
}
