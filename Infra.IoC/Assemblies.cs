using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Infra.IoC
{
    [ContractClass(typeof(AssembliesContract))]
    public abstract class Assemblies : IEnumerable<Assembly>
    {
        public static Assemblies Referenced { get; } = new ReferencedAssemblies();

        static string LocalDirectory { get; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static Assemblies Local { get; } = new DirectoryAssemblies(LocalDirectory);
        public static Assemblies In(string directory) => new DirectoryAssemblies(directory);

        public static Assemblies Entry => new CombinedAssemblies(Assembly.GetEntryAssembly());

        public abstract IEnumerator<Assembly> GetEnumerator();
   
        IEnumerator IEnumerable.GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return GetEnumerator();
        }

        public void ForAll(Action<Assembly> action)
        {
            Contract.Requires<ArgumentNullException>(action != null);
            foreach (var assembly in this)
                action(assembly);
        }

        public static Assemblies operator+(Assemblies x, Assemblies y)
        {
            Contract.Requires<ArgumentNullException>(x != null);
            Contract.Requires<ArgumentNullException>(y != null);
            Contract.Ensures(Contract.Result<Assemblies>() != null);
            return new CombinedAssemblies(x, y);
        }

        public Assemblies AndOf<T>() =>
            new CombinedAssemblies(this, new CombinedAssemblies(typeof(T).Assembly));
    }

    [ContractClassFor(typeof(Assemblies))]
    abstract class AssembliesContract : Assemblies
    {
        public override IEnumerator<Assembly> GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator<Assembly>>() != null);
            throw new NotImplementedException();
        }
    }
}
