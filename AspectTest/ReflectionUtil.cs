using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace AspectTest
{
    public static class ReflectionUtils
    {
        public static List<T> GetAttributes<T>(string methodName, MethodBase methodBase, Type type, bool inherit = true)
           where T : AttributeBase
        {
            var attributeList = new List<T>();

            if (methodBase.IsDefined(typeof(T), inherit))
            {
                attributeList.AddRange(methodBase.GetCustomAttributes(typeof(T), inherit).Cast<T>());
            }

            if (type.GetTypeInfo().IsDefined(typeof(T), inherit))
            {
                attributeList.AddRange(type.GetTypeInfo().GetCustomAttributes(typeof(T), inherit).Cast<T>());
            }

            if (type.GetInterfaces().SelectMany(p=>p.GetMethods()).Any(p=>p.Name==methodName))
            {
                attributeList.AddRange(type.GetInterfaces().SelectMany(p => p.GetMethod(methodName).GetCustomAttributes(typeof(T), inherit)).Cast<T>());
            }

            return attributeList;
        }
    }
}
