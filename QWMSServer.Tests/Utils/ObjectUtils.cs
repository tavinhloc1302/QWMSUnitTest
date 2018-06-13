using System;
using System.Reflection;

namespace QWMSServer.Tests.Utils
{
    public static class ObjectUtils
    {
        public static T GetProperty<T>(Type type, String name)
        {
            var prop = type.GetProperty(name, typeof(T));
            if (prop != null)
                return (T)prop.GetValue(null);

            var field = type.GetField(name);
            if (field != null)
                return (T)field.GetValue(null);

            throw new FieldAccessException("Object does not has field " + name);
        }

        public static T GetProperty<T>(Object obj, String name)
        {
            var objType = obj.GetType();

            var prop = objType.GetProperty(name, typeof(T));
            if (prop != null)
                return (T)prop.GetValue(obj);

            var field = objType.GetField(name);
            if (field != null)
                return (T)field.GetValue(obj);

            throw new FieldAccessException("Object does not has field " + name);
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
