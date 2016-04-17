using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    class AssemblyTypes : Types
    {
        public AssemblyTypes(Assemblies assemblies)
        {
            Assemblies = assemblies;
        }

        Assemblies Assemblies { get; }

        public override IEnumerator<Type> GetEnumerator()
        {
            return Assemblies
                .SelectMany(a => a.GetTypes())
                .GetEnumerator();
        }
    }
}
