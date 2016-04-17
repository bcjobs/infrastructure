using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    class CombinedAssemblies : Assemblies
    {
        public CombinedAssemblies(params Assemblies[] assemblies) 
            : this(assemblies.SelectMany(a => a).ToArray())
        {
        }

        public CombinedAssemblies(params Assembly[] assemblies)
        {
            Assemblies = assemblies.Where(a => a != null).Distinct();
        }

        IEnumerable<Assembly> Assemblies { get; }

        public override IEnumerator<Assembly> GetEnumerator() => 
            Assemblies.GetEnumerator();
    }
}
