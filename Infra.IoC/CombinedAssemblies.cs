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
            Contract.Requires<ArgumentNullException>(assemblies != null);
            Contract.Ensures(Assemblies != null);
        }

        public CombinedAssemblies(params Assembly[] assemblies)
        {
            Contract.Requires<ArgumentNullException>(assemblies != null);
            Contract.Ensures(Assemblies != null);
            Assemblies = assemblies.Where(a => a != null).Distinct();
        }

        IEnumerable<Assembly> Assemblies { get; }

        public override IEnumerator<Assembly> GetEnumerator() => 
            Assemblies.GetEnumerator();
    }
}
