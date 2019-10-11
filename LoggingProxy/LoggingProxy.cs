using System;
using System.Collections.Generic;
using System.Reflection;

namespace LoggingProxy
{
    public static class LoggingProxy
    {
        public static T CreateInstance<T>(T obj)
        {
            dynamic instance = new System.Dynamic.ExpandoObject();
            Type instanceType = typeof(T);
            MethodInfo[] methods = instanceType.GetMethods();
            IDictionary<String, Object> instanceAsDictionary = instance;
            foreach (MethodInfo method in methods)
            {

            }



            return instance;
        }

    }
}
