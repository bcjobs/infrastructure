using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    class CombinedTypes : Types
    {
        public CombinedTypes(params Types[] types)
        {
            Types = types;
        }

        Types[] Types { get; }

        public override IEnumerator<Type> GetEnumerator()
        {
            return Types
                .SelectMany(t => t)
                .Distinct()
                .GetEnumerator();
        }
    }
}
