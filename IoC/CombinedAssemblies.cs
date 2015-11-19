using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    class CombinedAssemblies : Assemblies
    {
        public CombinedAssemblies(params Assemblies[] assemblies)
        {
            Assemblies = assemblies;
        }

        Assemblies[] Assemblies { get; }

        public override IEnumerator<Assembly> GetEnumerator()
        {
            return Assemblies
                .SelectMany(a => a)
                .Distinct()
                .GetEnumerator();
        }
    }
}
