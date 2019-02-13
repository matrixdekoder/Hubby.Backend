using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Reflection;

namespace Core.Domain
{
    public static class RedirectToWhen
    {
        private static readonly MethodInfo InternalPreserveStackTraceMethod = typeof(Exception).GetMethod("InternalPreserveStackTrace", BindingFlags.Instance | BindingFlags.NonPublic);

        private static class Cache<T>
        {
            public static readonly IDictionary<Type, MethodInfo> Dict = typeof(T)
                .GetMethods(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance)
                .Where(m => m.Name == "When")
                .Where(m => m.GetParameters().Length == 1)
                .ToDictionary(m => m.GetParameters().First().ParameterType, m => m);
        }

        [DebuggerNonUserCode]
        public static void InvokeEventOptional<T, TEntity>(T instance, object @event)
        {
            var type = @event.GetType();
            if (!Cache<TEntity>.Dict.TryGetValue(type, out var info)) return;

            try
            {
                info.Invoke(instance, new[] { @event });
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                throw ex.InnerException;
            }
        }

        [DebuggerNonUserCode]
        public static void InvokeCommand<T>(T instance, object command)
        {
            var type = command.GetType();
            if (!Cache<T>.Dict.TryGetValue(type, out var info))
            {
                var s = $"Failed to locate {typeof(T).Name}.When({type.Name})";
                throw new InvalidOperationException(s);
            }
            try
            {
                info.Invoke(instance, new[] { command });
            }
            catch (TargetInvocationException ex)
            {
                if (null != InternalPreserveStackTraceMethod)
                    InternalPreserveStackTraceMethod.Invoke(ex.InnerException, new object[0]);
                throw ex.InnerException;
            }
        }
    }
}
