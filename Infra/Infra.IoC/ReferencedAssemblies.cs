using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    class ReferencedAssemblies : Assemblies
    {
        public override IEnumerator<Assembly> GetEnumerator()
        {
            return AppDomain.CurrentDomain
                .GetAssemblies()
                .SelectMany(a => Explore(a))
                .GetEnumerator();
        }

        IEnumerable<Assembly> Explore(Assembly assembly)
        {
            if (!assembly.IsDefined(typeof(IoCAttribute)))
                yield break;

            yield return assembly;
            foreach (var a in assembly
                .GetReferencedAssemblies()
                .Select(an => Assembly.Load(an))
                .SelectMany(ra => Explore(ra)))
                yield return a;
        }
    }
}
