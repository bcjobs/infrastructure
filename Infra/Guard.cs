using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public static class Guard
    {
        public static void Assert<T>(bool condition) where T : Exception
        { 
            if (!condition)
                throw Activator.CreateInstance<T>();
        }

        public static void Assert<T>(bool condition, string message) where T : Exception
        {
            if (!condition)
                throw (T)Activator.CreateInstance(typeof(T), message);
        }
    }
}
