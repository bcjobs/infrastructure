using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    class SelectedTypes : Types
    {
        public SelectedTypes(Types types, Func<Type, bool> predicate)
        {
            Types = types;
            Predicate = predicate;
        }

        Types Types { get; }
        Func<Type, bool> Predicate { get; }

        public override IEnumerator<Type> GetEnumerator()
        {
            return Types
                .Where(Predicate)
                .GetEnumerator();
        }
    }
}
