using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringHelpers
    {
        public static T To<T>(this string text)
        {
            var type = typeof(T);
            if (text == null) {
                if (type.IsNullable())
                    return default(T);
                else
                    throw new InvalidCastException();
            }

            return (T)TypeDescriptor
                .GetConverter(typeof(T))
                .ConvertFromInvariantString(text);
        }
    }

}
