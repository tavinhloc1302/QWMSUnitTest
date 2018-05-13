using System;
using System.Reflection;

namespace QWMSServer.Tests.Utils
{
    public static class ObjectUtils
    {
        public static T GetProperty<T>(Object obj, String name)
        {
            var prop = obj.GetType().GetProperty(name, typeof(T));
            if (prop == null)
                return default(T);

            return (T)prop.GetValue(obj);
        }

        public static MethodInfo GetMethod(Object obj, String name)
        {
            return obj.GetType().GetMethod(name);
        }
    }
}
