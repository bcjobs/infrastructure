using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mixins
{
    public class Mixin
    {
        readonly static ConcurrentDictionary<Type, Type> _map = 
            new ConcurrentDictionary<Type, Type>();

        public static Type Emit(Type mixin)
        {
            return _map.GetOrAdd(
                mixin, 
                mi => new MixinFactory(mi).Emit());
        }

        public static object Create(Type mixin, params object[] args)
        {
            return Activator.CreateInstance(Emit(mixin), args);
        }

        public static T Create<T>(params object[] args)
        {
            return (T)Create(typeof(T), args);
        }
    }
}
