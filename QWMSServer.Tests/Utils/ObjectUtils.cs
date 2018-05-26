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

        public static T SetProperty<T>(Object obj, String name, T value)
        {
            var prop = obj.GetType().GetProperty(name, typeof(T));
            if (prop == null)
                throw new FieldAccessException("Object does not has field " + name);

            prop.SetValue(obj, value);
            return value;
        }

        public static MethodInfo GetMethod(Object obj, String name)
        {
            return obj.GetType().GetMethod(name);
        }
    }
}
