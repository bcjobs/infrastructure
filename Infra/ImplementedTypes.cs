using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra
{
    public class ImplementedTypes : IEnumerable<Type>
    {
        public ImplementedTypes(object obj)
            : this(obj?.GetType())
        {
        }

        public ImplementedTypes(Type type)
        {
            _type = type;
        }

        readonly Type _type;

        public IEnumerator<Type> GetEnumerator()
        {
            if (_type == null)
                yield break;

            foreach (var i in _type.GetInterfaces())
                yield return i;

            for (Type t = _type; t != null; t = t.BaseType)
                yield return t;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
