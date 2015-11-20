using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    [ContractClass(typeof(AssembliesContract))]
    public abstract class Assemblies : IEnumerable<Assembly>
    {
        static string LocalDirectory { get; } = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
        public static Assemblies Local { get; } = new DirectoryAssemblies(LocalDirectory);
        public static Assemblies In(string directory) => new DirectoryAssemblies(directory);

        public abstract IEnumerator<Assembly> GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator()
        {
            Contract.Ensures(Contract.Result<IEnumerator>() != null);
            return GetEnumerator();
        }

        public static Assemblies operator+(Assemblies x, Assemblies y)
        {
            Contract.Requires<ArgumentNullException>(x != null);
            Contract.Requires<ArgumentNullException>(y != null);
            Contract.Ensures(Contract.Result<Assemblies>() != null);
            return new CombinedAssemblies(x, y);
        }
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
