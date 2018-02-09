using System;
using System.Collections.Generic;
using System.Text;

namespace Core2WebApi.Core.Utills
{
    public class KeyValuePairHelper
    {
        public static bool IsKeyValuePair(object o)
        {
            Type type = o.GetType();
            if (type.IsGenericType)
            {
                return type.GetGenericTypeDefinition() != null ? type.GetGenericTypeDefinition() == typeof(KeyValuePair<,>) : false;
            }
            return false;
        }
    }
}
