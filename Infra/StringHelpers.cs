using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace System
{
    public static class StringHelpers
    {
        public static T To<T>(this string text)
        {
            if (text == null) {
                if (typeof(T).IsNullable())
                    return default(T);
                else
                    throw new InvalidCastException();
            }

            return (T)Convert.ChangeType(text, typeof(T));
        }

    }

}
