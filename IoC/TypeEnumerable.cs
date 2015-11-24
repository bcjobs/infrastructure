using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public static class TypeEnumerable
    {
        public static void ForAll(this IEnumerable<Type> types, Action<Type> action)
        {
            Contract.Requires<ArgumentNullException>(types != null);
            Contract.Requires<ArgumentNullException>(action != null);
            foreach (var type in types.Distinct())
                action(type);
        }
    }
}
