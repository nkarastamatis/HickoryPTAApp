using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PTAData.Entities
{

    /// <summary>
    /// Summary description for Convert
    /// </summary>
    public static class Convert
    {
        public static object Transform(this Type type, object obj)
        {
            var t = Activator.CreateInstance(type);

            var valuesByProp = obj as Dictionary<string, object>;
            if (valuesByProp == null)
                return t;

            foreach (var pair in valuesByProp)
            {
                var property = type.GetProperty(pair.Key);
                if (property != null)
                {
                    object value = null;
                    if (property.PropertyType == typeof(string))
                    {
                        value = pair.Value;
                    }
                    else if (pair.Value is Array)
                    {
                        value = Activator.CreateInstance(property.PropertyType);
                        var genericType = property.PropertyType.GetGenericArguments()[0];
                        foreach (var item in (pair.Value as object[]))
                        {

                            ((IList)value).Add(
                                genericType.Transform(item));
                        }
                    }
                    else
                    {
                        value = property.PropertyType.Transform(pair.Value);
                    }

                    property.SetValue(t, value);
                }
            }

            return t;

        }
    }
}